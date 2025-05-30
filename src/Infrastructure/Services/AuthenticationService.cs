using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
       private readonly IAuthenticationRepository _authenticationRepository;
       private readonly IConfiguration _configuration;
       private readonly IPasswordHasherService _passwordHasher;
       private readonly IEmailService _emailService;
       public AuthenticationService(IAuthenticationRepository authenticationRepository, 
           IConfiguration configuration,
           IPasswordHasherService passwordHasherService,
           IEmailService emailService)
        {
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasherService;
            _emailService = emailService;            
        }

        public async Task<Result<string>> AuthenticateCredentials(CredentialForRequest credentialForRequest)
        {
            User? user = await ValidateUser(credentialForRequest);
            if (user == null)
            {
                return Result<string>.Failure($"User not found");
            }
            var claims =  GetUserClaimsAsync(user);
            var token = GenerateToken(claims);
            if (token == null) {
                return Result<string>.Failure($"Error");
            }
            return Result<string>.Success(token);            
        }


        public async Task<User?> ValidateUser(CredentialForRequest credentialForRequest)
        {
            User? user = await _authenticationRepository.GetUserByEmail(credentialForRequest.Email);
            if (user == null || user.IsAvailable == false)
            {
                return null;
            }
            var passwordValidator = _passwordHasher.VerifyPassword(user.Password,credentialForRequest.Password);
            if (!passwordValidator)
            {
                return null;
            }            
            return user;
        }
        public IEnumerable<Claim> GetUserClaimsAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            return claims;
        }
        public string? GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<Result<string>> ForgotPasswordAsync(string email)
        {
            var user = await _authenticationRepository.GetUserByEmail(email);
            if (user == null || !user.IsAvailable)
                return Result<string>.Failure("User not found or inactive");

            List<Claim> claims = GetUserClaimsAsync(user).ToList();
            claims.Add(new Claim("ResetPassword", "true"));

            var token = GenerateToken(claims);

            var link = $"http://localhost:4200/reset-password?token={token}";

            var emailDto = new EmailDto
            {
                Para = user.Email,
                Asunto = "Reset your password",
                Contenido = $"""
                <h2>Password recovery</h2>
                <p>Click the link below to reset your password. It will expire shortly:</p>
                <a href="{link}">Reset Password</a>
            """
            };
            _emailService.SendEmail(emailDto);
            return Result<string>.Success("Recovery email sent");
        }
        public async Task<Result<string>> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Authentication:Issuer"],
                ValidAudience = _configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal;
            try
            {
                principal = handler.ValidateToken(resetPassword.Token, parameters, out _);
            }
            catch
            {
                return Result<string>.Failure("The token is invalid or has expired.");
            }

            var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var resetFlag = principal.Claims.FirstOrDefault(c => c.Type == "ResetPassword")?.Value;

            if (email == null || resetFlag != "true")
                return Result<string>.Failure("Invalid token");

            var user = await _authenticationRepository.GetUserByEmail(email);
            if (user == null || !user.IsAvailable)
                return Result<string>.Failure("Usuario no válido");

            user.Password = _passwordHasher.HashPassword(resetPassword.NewPassword);
            await _authenticationRepository.UpdateAsync(user);

            return Result<string>.Success("Password changed");
        }





    }
}

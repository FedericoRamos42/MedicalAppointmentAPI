using Application.Interfaces;
using Application.Models.Request;
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
       public AuthenticationService(IAuthenticationRepository authenticationRepository, IConfiguration configuration)
        {
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;
        }

        public async Task<string?> AuthenticateCredentials(CredentialForRequest credentialForRequest)
        {
            User? user = await ValidateUser(credentialForRequest);
            if (user == null)
            {
                return null;
            }
            var claims =  GetUserClaimsAsync(user);
            var token = GenerateToken(claims);
            return token;            
        }


        public async Task<User?> ValidateUser(CredentialForRequest credentialForRequest)
        {
            User? user = await _authenticationRepository.Authenticate(credentialForRequest.Email, credentialForRequest.Password);
            if (user == null)
            {
                return null;
            }
            if (user.IsAvailable == false)
            {
                return null;
            }
            return user;
        }
        public IEnumerable<Claim> GetUserClaimsAsync(User user)
        {
            var claims = new List<Claim>();
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
                new Claim(ClaimTypes.Email, user.Email);
                new Claim(ClaimTypes.Role, user.Role.ToString());
            }
            return claims;
        }
        public string? GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

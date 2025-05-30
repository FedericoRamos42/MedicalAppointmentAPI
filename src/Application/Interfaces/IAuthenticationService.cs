using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User?> ValidateUser(CredentialForRequest credentialForRequest);
        Task<Result<string>> AuthenticateCredentials(CredentialForRequest credentialForRequest);
        IEnumerable<Claim> GetUserClaimsAsync(User user);
        string? GenerateToken(IEnumerable<Claim> claims);
        Task<Result<string>> ForgotPasswordAsync(string email);
        Task<Result<string>> ResetPasswordAsync(ResetPasswordDto resetPassword);

    }
}

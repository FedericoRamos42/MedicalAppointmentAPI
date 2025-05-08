using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User?> ValidateUser(CredentialForRequest credentialForRequest);
        Task<string> AuthenticateCredentials(CredentialForRequest credentialForRequest);
    }
}

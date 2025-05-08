using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
       private readonly IAuthenticationRepository _authenticationRepository;
       public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public async Task<string> AuthenticateCredentials(CredentialForRequest credentialForRequest)
        {
            throw new NotImplementedException();
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
    }
}

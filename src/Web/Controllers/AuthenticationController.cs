using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser([FromBody] CredentialForRequest request)
        {
            var token = await _authenticationService.AuthenticateCredentials(request);

            if (token is not null)
            {
                return (Ok(token));
            }

            return Unauthorized();
        }


    }
}

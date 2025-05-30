using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICurrentUserService _currentUserService;
        public AuthenticationController(IAuthenticationService authenticationService, ICurrentUserService currentUserService)
        {
            _authenticationService = authenticationService;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser([FromBody] CredentialForRequest request)
        {
            var result = await _authenticationService.AuthenticateCredentials(request);

            if (!result.IsSuccess)
            {
                return Unauthorized(result);
            }
            return Ok(result);

        }
        [HttpGet("FindUserClaims")]
        public IActionResult GetMyProfile()
        {
            var result = _currentUserService.GetProfile();
            if (!result.IsSuccess)
            {
                Unauthorized(result);
            }

            return Ok(result);
        }

    }
}

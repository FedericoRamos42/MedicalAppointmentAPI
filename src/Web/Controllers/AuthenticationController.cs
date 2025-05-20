using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var token = await _authenticationService.AuthenticateCredentials(request);

            if (token is not null)
            {
                return (Ok(token));
            }

            return Unauthorized();
        }
        //[Authorize]
        [HttpGet("/FindUserClaims")]
        public IActionResult GetMyProfile()
        {
            var userId = _currentUserService.GetUserId();
            if (userId is null) { return Unauthorized(); }  
            var email = _currentUserService.GetUserEmail();
            var role = _currentUserService.GetUserRole();
            return Ok(new
            {
                Id = userId,
                Email = email,
                Role = role
            });
        }

    }
}

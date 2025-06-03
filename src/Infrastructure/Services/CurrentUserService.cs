using Application.Interfaces;
using Application.Models.Response;
using Application.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Result<ProfileResponse> GetProfile()
        { 
            var id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var fullname = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var email =  _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            var role = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (id == null || email == null || role == null || fullname == null) 
            {
                return Result<ProfileResponse>.Failure("error");
            }

            var dto = new ProfileResponse
            {
                Id = id,
                FullName= fullname,
                Email = email,
                Role = role,
            };

            return Result<ProfileResponse>.Success(dto);
        }
    }
}

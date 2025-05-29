using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AvailabilityCreateRequest request)
        {
            var result = await _availabilityService.Create(request);
            return Created(string.Empty, result);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AvailabilityUpdateRequest request)
        {
            var result = await _availabilityService.Update(id, request);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var result = await _availabilityService.Delete(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] int pageIndex, [FromQuery] int pageSize = 5)
        {
            var result = await _availabilityService.GetPaginated(pageIndex, pageSize);
            return Ok(result);
        }

    }
}

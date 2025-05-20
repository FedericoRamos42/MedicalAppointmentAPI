using Application.Interfaces;
using Application.Models.Request;
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
            var availability = await _availabilityService.Create(request);
            return Ok(availability);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AvailabilityUpdateRequest request)
        {
            var availability = await _availabilityService.Update(id, request);
            return Ok(availability);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var availability = await _availabilityService.Delete(id);
            return Ok(availability);
        }

    }
}

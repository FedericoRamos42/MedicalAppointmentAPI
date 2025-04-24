using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;
        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }
        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetByDoctor(int id)
        {
            var list = await _service.GetByDoctor(id);
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ScheduleCreateRequest request)
        {
            var schedule = await _service.Create(request);
            return Ok(schedule);    
        }


    }
}

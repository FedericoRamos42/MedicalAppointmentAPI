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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ScheduleCreateRequest request)
        {
            var schedule = await _service.Create(request);
            return Ok(schedule);    
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var shedule = await _service.Delete(id);
            return Ok(shedule);
        }

        [HttpGet("/availabilities{doctorId}")]
        public async Task<IActionResult> Get([FromRoute] int doctorId, [FromQuery] DateTime date)
        {
            var list = await _service.GetByDoctorAndDate(doctorId, date);
            return Ok(list);
        }
        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetSchedule([FromRoute] int doctorId)
        {
            var schedule = await _service.GetByDoctor(doctorId);
            return Ok(schedule);
        }

        [HttpPost("{idDoctor}/availability")]
        public async Task<IActionResult> AddAvailability([FromRoute]int idDoctor,[FromBody] AvailabilityCreateRequest request )
        {
            var schedule = await _service.AddAvailability(idDoctor, request);
            return Ok(schedule);
        }

    }
}

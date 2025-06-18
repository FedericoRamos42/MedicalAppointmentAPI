using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IEmailService _emailService;
        public AppointmentController(IAppointmentService appointmentService,IEmailService emailService)
        {
            _appointmentService = appointmentService;
            _emailService = emailService;   
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _appointmentService.GetById(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentService.GetAll();
            return Ok(result);
        }
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var result = await _appointmentService.GetByPatient(patientId);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var result = await _appointmentService.GetByDoctor(doctorId);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("status/{id}")]
        public async Task<IActionResult> GetByStatus(int id,[FromQuery] AppointmentStatus status)
        {
            var result = await _appointmentService.GetByStatus(id, status);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateRequest request)
        {
            var result = await _appointmentService.Create(request);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Created(string.Empty, result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
            var result = await _appointmentService.Delete(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPut("canceled/{id}")]
        public async Task<IActionResult> Cancel (int id)
        {
            var result = await _appointmentService.Cancel(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("availabilities{doctorId}")]
        public async Task<IActionResult> Get([FromRoute] int doctorId, [FromQuery] DateTime date)
        {
            var result = await _appointmentService.GetAppointmentAvailabilited(doctorId, date);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] int pageIndex, [FromQuery] int pageSize = 5)
        {
            var result = await _appointmentService.GetPaginated(pageIndex, pageSize);
            return Ok(result);
        }       
    }
}

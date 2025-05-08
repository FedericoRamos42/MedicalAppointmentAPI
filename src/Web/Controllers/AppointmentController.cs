using Application.Interfaces;
using Application.Models.Request;
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
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var appointment = await _appointmentService.GetById(id);
            return Ok(appointment);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAll();
            return Ok(appointments);
        }
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var appointments = await _appointmentService.GetByPatient(patientId);
            return Ok(appointments);
        }
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var appointments = await _appointmentService.GetByDoctor(doctorId);
            return Ok(appointments);
        }
        [HttpGet("/status/{id}")]
        public async Task<IActionResult> GetByStatus(int id,[FromQuery] AppointmentStatus status)
        {
            var appointments = await _appointmentService.GetByStatus(id, status);
            return Ok(appointments);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateRequest request)
        {
            var appointment = await _appointmentService.Create(request);
            return Ok(appointment);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
            var appointment = await _appointmentService.Delete(id);
            return Ok(appointment);
        }

    }
}

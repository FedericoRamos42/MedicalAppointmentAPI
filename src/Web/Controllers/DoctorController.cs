using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = await _doctorService.GetById(id);
            return Ok(doctor);
        }
        [HttpGet("withAvailabilities/{id}")]
        public async Task<IActionResult> GetWithAvailabilities(int id)
        {
            var doctor = await _doctorService.GetWithAvailabilities(id);
            return Ok(doctor);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAll();
            return Ok(doctors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoctorUpdateRequest request)
        {
            var doctor = await _doctorService.Update(id, request);
            return Ok(doctor);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorService.Delete(id);
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DoctorCreateRequest request)
        {
            var doctor = await _doctorService.Create(request);
            return Ok(doctor);
        }
    }
}

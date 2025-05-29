using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IMedicalHistoryService _service;
        public MedicalHistoryController(IMedicalHistoryService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var medicalHistory = await _service.GetById(id);
            return Ok(medicalHistory);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAll();
            return Ok(list);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var medicalHistory = await _service.Delete(id);
            return Ok(medicalHistory);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalHistoryCreateRequest request)
        {
            var medicalHistory = await _service.Create(request);
            return Ok(medicalHistory);
        }
    }
}

using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
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
            var result = await _doctorService.GetById(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("withAvailabilities/{id}")]
        public async Task<IActionResult> GetWithAvailabilities(int id)
        {
            var result = await _doctorService.GetWithAvailabilities(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _doctorService.GetAll();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoctorUpdateRequest request)
        {
            var result = await _doctorService.Update(id, request);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorService.Delete(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DoctorCreateRequest request)
        {
            var result = await _doctorService.Create(request);
            return Created(string.Empty, result);
        }
        //[HttpGet("paginated")]
        //public async Task<IActionResult> GetPaginated([FromQuery] int pageIndex, [FromQuery] int pageSize = 5)
        //{
        //    var result = await _doctorService.GetPaginated(pageIndex, pageSize);
        //    return Ok(result);
        //}
        [HttpGet("GetFilteredDoctors")]
        public async Task<IActionResult> GetFilteredDoctors([FromQuery] int pageIndex,
                                                            [FromQuery] int pageSize = 5,                                                            
                                                            [FromQuery] DoctorFilterDto? filter = null,
                                                            [FromQuery] string? orderBy = "Name")
        {
            var result = await _doctorService.GetFilteredPaginatedAsync(pageIndex, pageSize, filter, orderBy);
            return Ok(result);
        }

    }
}

using Application.Interfaces;
using Application.Models.Request;
using Application.Result;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        public AdminController(IAdminService service) 
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            
            var result = await _service.GetById(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var result = await _service.GetAll();  
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody] AdminUpdateRequest request)
        {
            var result = await _service.Update(id,request);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _service.Delete(Id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AdminCreateRequest request)
        {
            var result = await _service.Create(request);
            return Ok(result);
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] int pageIndex, [FromQuery] int pageSize = 5)
        {
            var result = await _service.GetPaginated(pageIndex, pageSize);
            return Ok(result);
        }

    }
}

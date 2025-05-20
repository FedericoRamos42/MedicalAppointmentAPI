using Application.Interfaces;
using Application.Models.Request;
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
            var admin = await _service.GetById(id);
            return Ok(admin);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var list = await _service.GetAll();   
            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody] AdminUpdateRequest request)
        {
            var admin = await _service.Update(id,request);
            return Ok(admin);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var admin = await _service.Delete(Id);
            return Ok(admin);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AdminCreateRequest request)
        {
            var admin = await _service.Create(request);
            return Ok(admin);
        }

    }
}

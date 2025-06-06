using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("Admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var result = await _dashboardService.GetAdminDashboard();
            return Ok(result);
        }
        [HttpGet("Doctor/{id}")]
        public async Task<IActionResult> GetDoctorDashboard(int id)
        {
            var result = await _dashboardService.GetDoctorDashboard(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("Patient/{id}")]
        public async Task<IActionResult> GetPatientDashboard(int id)
        {
            var result = await _dashboardService.GetPatientDashboard(id);
            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


    }
}

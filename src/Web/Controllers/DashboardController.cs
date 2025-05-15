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
            var dashboard = await _dashboardService.GetAdminDashboard();
            return Ok(dashboard);
        }
    }
}

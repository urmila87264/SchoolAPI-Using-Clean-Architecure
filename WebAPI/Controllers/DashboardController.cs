using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")] // Only Admin can access this
        public IActionResult GetAdminDashboard()
        {
            return Ok(new { Message = "Welcome to Admin Dashboard!" });
        }

        [HttpGet("teacher")]
        [Authorize(Roles = "Teacher")] // Only User can access this
        public IActionResult GetUserDashboard()
        {
            return Ok(new { Message = "Welcome to User Dashboard!" });
        }
    }
}

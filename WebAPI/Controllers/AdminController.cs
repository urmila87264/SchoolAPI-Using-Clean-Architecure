using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        [HttpGet("dashboard")]
        public IActionResult GetAdminDashboard() {
            return Ok(new { Message = "Welcome to Admin Dashboard!" });
        }
    }
}

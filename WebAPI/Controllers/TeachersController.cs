using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")] // Only Teachers can access this controller
    public class TeachersController : ControllerBase
    {
        [HttpGet("dashboard")]
        public IActionResult GetTeacherDashboard()
        {
            return Ok(new { Message = "Welcome to Teacher Dashboard!" });
        }
    }
}

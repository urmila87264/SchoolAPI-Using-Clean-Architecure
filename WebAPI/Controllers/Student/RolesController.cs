using Appliction.Interfaces.Student;
using Appliction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole() {
            var roles = await _rolesService.GetAllRolesAsync();
            if (roles == null || roles.Count == 0)
            {
                return NotFound("No roles found.");
            }
            return Ok(roles);
        }
    }
}

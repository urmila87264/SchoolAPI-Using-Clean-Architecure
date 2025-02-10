using Appliction.Interfaces;
using Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthenticationController(ILoginService loginService)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            // ✅ Validate request object
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { Message = "Email and password are required." });
            }

            try
            {
                // ✅ Check user credentials
                var user = await _loginService.GetUserByUsernameAsync(request.Email, request.Password);

                if (user == null)
                {
                    return Unauthorized(new { Message = "Invalid email or password." });
                }

                return Ok(new
                {
                    Message = "Login successful",
                    User = new
                    {
                        user.Email,
                        user.Role // ✅ Avoid exposing sensitive data
                    }
                });
            }
            catch (Exception ex)
            {
                // ✅ Log error (Use ILogger in production)
                return StatusCode(500, new
                {
                    Message = "An error occurred while processing the request.",
                    Details = ex.Message
                });
            }
        }
    }
}

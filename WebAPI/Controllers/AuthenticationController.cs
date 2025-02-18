using Appliction.Interfaces;
using Domain.Authentication;
using Infrastructure.JWT;
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
        private readonly JWTToken _jwtToken;

        public AuthenticationController(ILoginService loginService, JWTToken jwtToken)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            _jwtToken = jwtToken ?? throw new ArgumentNullException(nameof(jwtToken));
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

                // ✅ Generate JWT token
                var token = _jwtToken.GenerateJwtToken(user);

                return Ok(new
                {
                    Message = "Login successful",
                    Token = token,
                    User = new
                    {
                        Email = user.Email,
                        Role = user.RoleId,
                        Password=user.Password,

                    }
                });
            }
            catch (Exception ex)
            {
                // ✅ Log error (Consider using ILogger)
                return StatusCode(500, new
                {
                    Message = "An error occurred while processing the request.",
                    Error = ex.Message
                });
            }
        }
    }
}

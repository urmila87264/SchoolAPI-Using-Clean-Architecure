using Appliction.Interfaces;
using Domain.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/Logins")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginsController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("login")]
        //[Route("Login")]
        public IActionResult Login([FromBody] LoginRequest request) {
            var user =  _loginService.GetUserByUsernameAsync(request.Email, request.Password);

            if (user == null)
            {
                return BadRequest(new { Message = "Invalid email or password." });
            }
            return Ok(new
            {
                Message = "Login successful",
                // Token = token,
             
            });

        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

using Appliction.Interfaces;
using Domain.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private  readonly IUserService _userService;
        //  private readonly ILoginService _loginService;
        //public AuthController(IUserService userService, ILoginService loginService)
        //{
        //    _userService = userService;
        //    _loginService = loginService;

        //}

        public AuthController(IUserService userService)
        {
            _userService = userService;
          

        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] Registration user)
        {
            try
            {
                // ✅ 1. Validate Incoming Request
                if (user == null)
                {
                    return BadRequest(new { Message = "Invalid request. User data is required." });
                }

                //if (!user.IsValid()) // ✅ Custom validation from SignUp model
                //{
                //    return BadRequest(new { Message = "Validation failed. Please check the provided details." });
                //}

                // ✅ 2. Call SignUp Service
                var result = await _userService.SignUpAsync(user);

                // ✅ 3. Handle Response
                if (result)
                {
                    return Ok(new { Message = "User registered successfully." });
                }

                return BadRequest(new { Message = "User registration failed. Email might already exist." });
            }
            catch (Exception ex)
            {
                // ✅ 4. Log the Exception (Assuming you have Serilog or ILogger)
                //ILogger.LogError(ex, "Error during user registration.");

                // ✅ 5. Return Generic Error Response
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }


    }
}

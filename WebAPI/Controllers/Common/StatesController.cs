using Appliction.Common.Response;
using Appliction.Interfaces.Common;
using Appliction.Common.Response;
using Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        public readonly IstateService _stateService;
        public StatesController(IstateService istateService)
        {
            _stateService = istateService;
        }
        [HttpGet("GetAllState")]
        public async Task<IActionResult> GetAllState()
        {
            var response = await _stateService.GetAllStatesAsync();
          
           // BaseResponse<State> baseResponse = null;
            if (response==null)
            {
                return NotFound(response);
            }

            return Ok(response);

        }
    }
}

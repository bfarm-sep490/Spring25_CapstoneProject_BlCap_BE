using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels.Auth;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthencationController : ControllerBase
    {
        public IAuthencationService _authencationService;
        public AuthencationController(IAuthencationService authencationService)
        {
            _authencationService = authencationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<IBusinessResult>> SignIn(LoginForm form)
        {
            try
            {
                var result = await _authencationService.SignIn(form.Email, form.Password);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IBusinessResult>> GetAccountInfobyId([FromRoute] int id)
        {
            try
            {
                var result = await _authencationService.GetAccountInfoById(id);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> ChangePassword(int id, AccountChangePassword model)
        {
            try
            {
                var rs = await _authencationService.ChangePassword(id, model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet()]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _authencationService.GetAllAccount();
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("{farmer_id}/device-token")]
        public async Task<ActionResult<IBusinessResult>> CreateDeviceToken([FromRoute]int farmer_id, [FromBody]string token)
        {
            try
            {
                var result = await _authencationService.AddFarmerDevice(farmer_id,token);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{farmer_id}/device-token")]
        public async Task<ActionResult<IBusinessResult>> GetDeviceTokensByFarmerId([FromRoute] int farmer_id)
        {
            try
            {
                var result = await _authencationService.GetAllDeviceTokensbyFarmerId(farmer_id);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.APIs.RequestModels;
using Spring25.BlCapstone.BE.Services.Base;
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

        [HttpPost("owner/login")]
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
        [HttpPost("farmer/login")]
        public async Task<ActionResult<IBusinessResult>> SignInForFarmer(LoginForm form)
        {
            try
            {
                var result = await _authencationService.SignInForFarmer(form.Email, form.Password);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

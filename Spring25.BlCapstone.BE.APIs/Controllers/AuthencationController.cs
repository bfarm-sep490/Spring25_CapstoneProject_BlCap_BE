using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        [Authorize]
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

        [HttpGet]
        [Authorize]
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

        [HttpPost("password-forgotten")]
        [Authorize]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                var result = await _authencationService.ForgotPassword(email);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
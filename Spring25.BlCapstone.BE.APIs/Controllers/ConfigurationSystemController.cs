using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Config;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/configuration-systems")]
    [ApiController]
    [Authorize]
    public class ConfigurationSystemController : ControllerBase
    {
        public IConfigurationSystemService _configurationSystemService;
        public ConfigurationSystemController(IConfigurationSystemService configurationSystemService)
        {
            _configurationSystemService = configurationSystemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConfig()
        {
            try
            {
                var rs = await _configurationSystemService.GetConfig();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(ConfigSystemCreate model)
        {
            try
            {
                var rs = await _configurationSystemService.CreateNewConfig(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

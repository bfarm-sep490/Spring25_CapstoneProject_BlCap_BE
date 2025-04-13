using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Config;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/configuration-systems")]
    [ApiController]
    public class ConfigurationSystemController : ControllerBase
    {
        public IConfigurationSystemService _configurationSystemService;
        public ConfigurationSystemController(IConfigurationSystemService configurationSystemService)
        {
            _configurationSystemService = configurationSystemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? status)
        {
            try
            {
                var rs = await _configurationSystemService.GetAll(status);
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
        
        [HttpPut("{id}/status")]
        public async Task<IActionResult> Switch(int id)
        {
            try
            {
                var rs = await _configurationSystemService.SwitchStatus(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _driverService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rs = await _driverService.GetById(id);
            return Ok(rs);
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> SwitchStatus(int id)
        {
            var rs = await _driverService.SwitchStatus(id);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _driverService.RemoveDriver(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateFarmer model)
        {
            var rs = await _driverService.CreateDriver(model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateFarmer model)
        {
            var rs = await _driverService.UpdateDriver(id, model);
            return Ok(rs);
        }
    }
}

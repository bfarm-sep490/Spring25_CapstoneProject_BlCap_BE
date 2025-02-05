using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Device;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        public IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rs = await _deviceService.GetAll();
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rs = await _deviceService.GetById(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDevice model)
        {
            var rs = await _deviceService.CreateDevice(model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDevice model)
        {
            var rs = await _deviceService.UpdateDevice(id, model);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _deviceService.RemoveDevice(id);
            return Ok(rs);
        }
    }
}

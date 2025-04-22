using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/farmers")]
    [ApiController]
    public class FarmerController : ControllerBase
    {
        private IFarmerService _service;

        public FarmerController(IFarmerService farmerService)
        {
            _service = farmerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
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
            var rs = await _service.GetById(id);
            return Ok(rs);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SwitchStatus(int id)
        {
            var rs = await _service.SwitchStatus(id);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _service.RemoveFarmer(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFarmer model)
        {
            var rs = await _service.CreateFarmer(model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateFarmer model)
        {
            var rs = await _service.UpdateFarmer(id, model);
            return Ok(rs);
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _service.UploadImage(image);
            return Ok(rs);
        }

        [HttpPost("{id}/device-token")]
        public async Task<IActionResult> AddTokenDevice(int id, [Required] string tokenDevice)
        {
            try
            {
                var rs = await _service.AddFarmerTokenDevice(id, tokenDevice);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/device-token")]
        public async Task<IActionResult> GetListTokens(int id)
        {
            try
            {
                var rs = await _service.GetAllDeviceTokensByFarmerId(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/device-token")]
        public async Task<IActionResult> RemoveToken(int id)
        {
            try
            {
                var rs = await _service.RemoveDeviceTokenByFarmerId(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("{id}/calendar")]
        public async Task<IActionResult> GetCalendarByFarmerId(int id, DateTime? start_date, DateTime? end_date)
        {
            try
            {
                var rs = await _service.GetFarmerCalendar(id, start_date, end_date);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/notifications")]
        public async Task<IActionResult> GetListNotifications(int id)
        {
            try
            {
                var rs = await _service.GetListNotifications(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("notification-read/{notification_id}")]
        public async Task<IActionResult> MarkAsRead(int notification_id)
        {
            try
            {
                var rs = await _service.MarkAsRead(notification_id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/notifications-read")]
        public async Task<IActionResult> MarkAllAsRead(int id)
        {
            try
            {
                var rs = await _service.MarkAllAsRead(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

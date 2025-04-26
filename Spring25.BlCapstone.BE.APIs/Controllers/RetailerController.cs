using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Retailer;
using Spring25.BlCapstone.BE.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/retailers")]
    [ApiController]
    public class RetailerController : ControllerBase
    {
        private IRetailerService _retailerService;
        public RetailerController(IRetailerService retailerService)
        {
            _retailerService = retailerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _retailerService.GetAll();
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
            var rs = await _retailerService.GetById(id);
            return Ok(rs);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SwitchStatus(int id)
        {
            var rs = await _retailerService.SwitchStatus(id);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _retailerService.RemoveRetailer(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRetailer model)
        {
            var rs = await _retailerService.CreateRetailer(model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateRetailer model)
        {
            var rs = await _retailerService.UpdateRetailer(id, model);
            return Ok(rs);
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _retailerService.UploadImage(image);
            return Ok(rs);
        }

        [HttpGet("{id}/notifications")]
        public async Task<IActionResult> GetListNotifications(int id)
        {
            try
            {
                var rs = await _retailerService.GetListNotifications(id);
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
                var rs = await _retailerService.MarkAsRead(notification_id);
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
                var rs = await _retailerService.MarkAllAsRead(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

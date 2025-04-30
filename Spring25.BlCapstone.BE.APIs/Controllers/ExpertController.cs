using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/experts")]
    [ApiController]
    [Authorize]
    public class ExpertController : ControllerBase
    {
        private IExpertService _expertService;
        public ExpertController(IExpertService expertService)
        {
            _expertService = expertService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _expertService.GetAll();
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
            var rs = await _expertService.GetById(id);
            return Ok(rs);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SwitchStatus(int id)
        {
            var rs = await _expertService.SwitchStatus(id);
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var rs = await _expertService.RemoveExpert(id);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFarmer model)
        {
            var rs = await _expertService.CreateExpert(model);
            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateFarmer model)
        {
            var rs = await _expertService.UpdateExpert(id, model);
            return Ok(rs);
        }

        [HttpPost("images/upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> image)
        {
            var rs = await _expertService.UploadImage(image);
            return Ok(rs);
        }

        [HttpGet("{id}/notifications")]
        public async Task<IActionResult> GetListNotifications(int id)
        {
            try
            {
                var rs = await _expertService.GetListNotifications(id);
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
                var rs = await _expertService.MarkAsRead(notification_id);
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
                var rs = await _expertService.MarkAllAsRead(id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

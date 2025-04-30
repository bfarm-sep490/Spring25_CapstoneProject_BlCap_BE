using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/owners")]
    [ApiController]
    [Authorize]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> GetListNotifications()
        {
            try
            {
                var rs = await _ownerService.GetListNotifications();
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
                var rs = await _ownerService.MarkAsRead(notification_id);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("notifications-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            try
            {
                var rs = await _ownerService.MarkAllAsRead();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public IAblyService _ablyService;
        public NotificationController(IAblyService ablyService)
        {
            _ablyService = ablyService;
        }

        [HttpPost]
        public async Task<ActionResult<IBusinessResult>> SendNoti(string title, string body)
        {
            var rs = await _ablyService.SendNotification(title, body);
            return Ok(rs);
        }

        [HttpPost("topic")]
        public async Task<ActionResult<IBusinessResult>> SendNotiTpc(string title, string body, string topic)
        {
            var rs = await _ablyService.SendMessageWithTopic(title, body, topic);
            return Ok(rs);
        }
    }
}

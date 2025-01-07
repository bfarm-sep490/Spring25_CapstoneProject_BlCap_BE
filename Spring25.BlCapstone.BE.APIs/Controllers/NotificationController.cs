using Microsoft.AspNetCore.Mvc;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.Services;

namespace Spring25.BlCapstone.BE.APIs.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public IFCMService _fcmService;
        public NotificationController(IFCMService fcmService)
        {
            _fcmService = fcmService;
        }

        [HttpPost]
        public async Task<ActionResult<IBusinessResult>> SendNoti(string title, string body, string dvToken)
        {
            var rs = await _fcmService.SendMessageToDevice(title, body, dvToken);
            return Ok(rs);
        }

        [HttpPost("topic")]
        public async Task<ActionResult<IBusinessResult>> SendNotiTpc(string title, string body, string topic)
        {
            var rs = await _fcmService.SendMessageWithTopic(title, body, topic);
            return Ok(rs);
        }
    }
}

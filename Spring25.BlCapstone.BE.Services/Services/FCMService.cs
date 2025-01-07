using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IFCMService
    {
        Task<IBusinessResult> SendMessageToDevice(string title, string body, string dvToken);
        Task<IBusinessResult> SendMessageWithTopic(string title, string body, string topic);
    }

    public class FCMService : IFCMService
    {
        private readonly FCMHelper _fcm;
        public FCMService()
        {
            _fcm ??= new FCMHelper();
        }
        public async Task<IBusinessResult> SendMessageToDevice(string title, string body, string dvToken)
        {
            try
            {
                var res = await _fcm.SendMessageToDevice(title, body, dvToken);
                return new BusinessResult
                {
                    Status = 200,
                    Message = "Send Message Success!",
                    Data = res
                };
            }
            catch(Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = "Have bug?",
                    Data = null
                };
            }
        }

        public async Task<IBusinessResult> SendMessageWithTopic(string title, string body, string topic)
        {
            try
            {
                var res = await _fcm.SendMessageWithTopic(title, body, topic);
                return new BusinessResult
                {
                    Status = 200,
                    Message = "Send Message Success!",
                    Data = res
                };
            }
            catch(Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = "Have bug?",
                    Data = null
                };
            }
        }
    }
}

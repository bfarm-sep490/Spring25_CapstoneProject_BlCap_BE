using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Notification
{
    public class NotificationDeviceRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public object? Data { get; set; }
    }
}

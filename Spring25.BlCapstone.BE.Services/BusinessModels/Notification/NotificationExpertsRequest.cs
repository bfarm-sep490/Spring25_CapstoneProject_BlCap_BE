using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Notification
{
    public class NotificationExpertsRequest
    {
        public List<string>? Emails { get; set; } = new List<string>();
        public string Url { get; set; } 
    }
}

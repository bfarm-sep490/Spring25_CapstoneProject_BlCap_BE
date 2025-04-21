using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Notification
{
    public class NotificationExpertsRequest
    {
        public List<Infor>? Infors { get; set; }
        public string Url { get; set; } 
    }

    public class Infor
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}

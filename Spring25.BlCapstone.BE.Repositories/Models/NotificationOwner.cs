using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class NotificationOwner
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string? Message { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

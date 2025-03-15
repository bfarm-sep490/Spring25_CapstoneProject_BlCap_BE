using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class NotificationExpert
    {
        [Key]
        public int Id { get; set; }
        public int ExpertId { get; set; }
        public string? Message { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedDate { get; set; }

        public Expert Expert { get; set; }
    }
}

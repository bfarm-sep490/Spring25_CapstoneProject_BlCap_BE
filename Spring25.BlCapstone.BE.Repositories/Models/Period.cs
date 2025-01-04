using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Period
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Duration { get; set; }
        public double CompletedTrackQuantity { get; set; }
        public string Status { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int PlanId { get; set; }

        public Plan Plan { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}

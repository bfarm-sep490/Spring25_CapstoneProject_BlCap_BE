using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Period
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public string PeriodName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public Plan Plan { get; set; }
        public ICollection<ProductionTask> ProductionTasks { get; set; }
        public ICollection<HarvestingTask> HarvestingTasks { get; set; }
        public ICollection<PackagingTask> PackagingTasks { get; set; }
        public ICollection<InspectingTask> InspectingTasks { get; set; }
    }
}

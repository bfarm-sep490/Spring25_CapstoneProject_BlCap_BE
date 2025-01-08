using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime PlanDate { get; set; }
        public int PeriodId { get; set; }
        public string ReportContent { get; set; }
        public string Status { get; set; }
        public DateTime ActionDate { get; set; }
        public int FarmerId { get; set; }

        public Period Period { get; set; }
        public Farmer Farmer { get; set; }
        public ICollection<ImageReport> ImageReports { get; set; }
        public ICollection<TaskFertilizer> TaskFertilizers { get; set; }
        public ICollection<TaskPesticide> TaskPesticides { get; set; }
    }
}

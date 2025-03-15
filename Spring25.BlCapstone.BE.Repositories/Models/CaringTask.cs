using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class CaringTask
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int? ProblemId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string TaskType { get; set; }
        public string? ResultContent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public Plan Plan { get; set; }
        public Problem Problem { get; set; }
        public ICollection<CaringImage> CaringImages { get; set; }
        public ICollection<CaringPesticide> CaringPesticides { get; set; }
        public ICollection<CaringFertilizer> CaringFertilizers { get; set; }
        public ICollection<CaringItem> CaringItems { get; set; }
        public ICollection<FarmerCaringTask> FarmerCaringTasks { get; set; }
    }
}
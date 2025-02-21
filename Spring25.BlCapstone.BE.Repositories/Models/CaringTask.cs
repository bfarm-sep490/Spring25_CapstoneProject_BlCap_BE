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
        public int? FarmerId { get; set; }
        public int? ProblemId { get; set; }
        public string TaskName { get; set; }
        public string? ResultContent { get; set; }
        public string TaskType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsAvailable { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Plan Plan { get; set; }
        public Farmer Farmer { get; set; }
        public Problem Problem { get; set; }
        public ICollection<CaringImage> CaringImages { get; set; }
        public ICollection<CaringPesticide> CaringPesticides { get; set; }
        public ICollection<CaringFertilizer> CaringFertilizers { get; set; }
        public ICollection<CaringItem> CaringItems { get; set; }
    }
}

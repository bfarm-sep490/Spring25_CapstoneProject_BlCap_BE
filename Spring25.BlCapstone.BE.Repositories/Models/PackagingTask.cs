using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class PackagingTask
    {
        [Key]
        public int Id { get; set; }
        public int? FarmerId { get; set; }
        public int PlanId { get; set; }
        public string TaskName { get; set; }
        public string PackedUnit { get; set; }
        public int PackedQuantity { get; set; }
        public string Description { get; set; }
        public string? ResultContent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Priority { get; set; }

        public Farmer Farmer { get; set; }
        public Plan Plan { get; set; }
        public ICollection<PackagingImage> PackagingImages { get; set; }
        public ICollection<PackagingItem> PackagingItems { get; set; }
    }
}

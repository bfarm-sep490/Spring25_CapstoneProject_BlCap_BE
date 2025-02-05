using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class PackagingTask
    {
        [Key]
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public int FarmerId { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public DateTime PackagingDate { get; set; }
        public string PackagedUnit { get; set; }
        public float PackagedQuantity { get; set; }
        public string Description { get; set; }
        public string ResultContent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsAvailable { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Period Period { get; set; }
        public Farmer Farmer { get; set; }
        public ICollection<PackagingImage> PackagingImages { get; set; }
        public ICollection<PackagingItem> PackagingItems { get; set; }
    }
}

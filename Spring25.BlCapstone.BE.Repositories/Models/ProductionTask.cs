using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class ProductionTask
    {
        [Key]
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public int YieldId { get; set; }
        public int FarmerId { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsAvailable { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Period Period { get; set; }
        public Yield Yield { get; set; }
        public Farmer Farmer { get; set; }
        public ICollection<ProductionImage> ProductionImages { get; set; }
        public ICollection<ProductionPesticide> ProductionPesticides { get; set; }
        public ICollection<ProductionFertilizer> ProductionFertilizers { get; set; }
        public ICollection<ProductionItem> ProductionItems { get; set; }
    }
}

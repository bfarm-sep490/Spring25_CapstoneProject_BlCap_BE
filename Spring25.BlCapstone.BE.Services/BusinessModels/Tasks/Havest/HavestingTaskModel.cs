using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Havest
{
    public class HavestingTaskModel
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int YieldId { get; set; }
        public int? FarmerId { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public string Description { get; set; }
        public string? ResultContent { get; set; }
        public DateTime? HarvestDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public float? HarvestedQuantity { get; set; }
        public string? HarvestedUnit { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsAvailable { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<HavestingImageModel> havestImages { get; set; }
        public List<HavestingItemModel> havestingItems { get; set; }
    }
}

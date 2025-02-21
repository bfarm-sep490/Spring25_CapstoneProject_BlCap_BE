using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect
{
    public class InspectingFormModel
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int? InspectorId { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ResultContent { get; set; }
        public float BrixPoint { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Moisture { get; set; }
        public string ShellColor { get; set; }
        public string TestGTKitColor { get; set; }
        public int InspectingQuantity { get; set; }
        public string Unit { get; set; }
        public float? IssuePercent { get; set; }
        public bool CanHarvest { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<InspectingImageModel> InspectingImages { get; set; }
        public List<InspectingItemModel> InspectingItems { get; set; }
    }
}

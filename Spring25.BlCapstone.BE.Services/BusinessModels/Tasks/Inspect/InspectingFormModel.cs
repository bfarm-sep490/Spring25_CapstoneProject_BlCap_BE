using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect
{
    public class InspectingFormModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("plan_id")]
        public int PlanId { get; set; }

        [JsonPropertyName("inspector_id")]
        public int? InspectorId { get; set; }

        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }

        [JsonPropertyName("task_type")]
        public string TaskType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }

        [JsonPropertyName("brix_point")]
        public float BrixPoint { get; set; }

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public float Humidity { get; set; }

        [JsonPropertyName("moisture")]
        public float Moisture { get; set; }

        [JsonPropertyName("shell_color")]
        public string ShellColor { get; set; }

        [JsonPropertyName("test_gt_kit_color")]
        public string TestGTKitColor { get; set; }

        [JsonPropertyName("inspecting_quantity")]
        public int InspectingQuantity { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("issue_percent")]
        public float? IssuePercent { get; set; }

        [JsonPropertyName("can_harvest")]
        public bool CanHarvest { get; set; }

        [JsonPropertyName("completed_date")]
        public DateTime CompletedDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("inspecting_images")]
        public List<InspectingImageModel> InspectingImages { get; set; }

        [JsonPropertyName("inspecting_items")]
        public List<InspectingItemModel> InspectingItems { get; set; }
    }
}

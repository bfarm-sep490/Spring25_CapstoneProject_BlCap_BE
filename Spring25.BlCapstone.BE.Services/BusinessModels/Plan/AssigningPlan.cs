using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plan
{
    public class AssigningPlan
    {
        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }
        [JsonPropertyName("yield_id")]
        public int? YieldId { get; set; }
        [JsonPropertyName("plan_name")]
        public string? PlanName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("estimated_product")]
        public float? EstimatedProduct { get; set; }
        [JsonPropertyName("estimated_unit")]
        public string? EstimatedUnit { get; set; }
        [JsonPropertyName("caring_tasks")]
        public List<AssignCaringTask>? AssignCaringTasks { get; set; }
        [JsonPropertyName("harvesting_tasks")]
        public List<AssignHarvestingTask>? AssignHarvestingTasks { get; set; }
        [JsonPropertyName("inspecting_forms")]
        public List<AssignInspectingForm>? AssignInspectingTasks { get; set; }
        [JsonPropertyName("packaging_tasks")]
        public List<AssignPackagingTask>? AssignPackagingTasks { get; set; }
    }

    public class AssignCaringTask
    {
        [JsonPropertyName("task_id")]
        public int Id { get; set; }
        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class AssignHarvestingTask
    {
        [JsonPropertyName("task_id")]
        public int Id { get; set; }
        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class AssignInspectingForm
    {
        [JsonPropertyName("task_id")]
        public int Id { get; set; }
        [JsonPropertyName("inspector_id")]
        public int? InspectorId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class AssignPackagingTask
    {
        [JsonPropertyName("task_id")]
        public int Id { get; set; }
        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}

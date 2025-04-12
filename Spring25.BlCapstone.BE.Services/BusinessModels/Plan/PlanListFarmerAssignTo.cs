using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plan
{
    public class PlanListFarmerAssignTo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }
        [JsonPropertyName("plant_name")]
        public string PlantName { get; set; }
        [JsonPropertyName("yield_id")]
        public int? YieldId { get; set; }
        [JsonPropertyName("yield_name")]
        public string? YieldName { get; set; }
        [JsonPropertyName("expert_id")]
        public int ExpertId { get; set; }
        [JsonPropertyName("expert_name")]
        public string Name { get; set; }
        [JsonPropertyName("plan_name")]
        public string? PlanName { get; set; }
        [JsonPropertyName("is_active_in_plan")]
        public bool Status { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonPropertyName("status")]
        public string PlanStatus { get; set; }
        [JsonPropertyName("estimated_product")]
        public float? EstimatedProduct { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }
        [JsonPropertyName("is_approved")]
        public bool IsApproved { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plan
{
    public class UpdatePlan
    {
        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }
        [JsonPropertyName("yield_id")]
        public int? YieldId { get; set; }
        [JsonPropertyName("expert_id")]
        public int ExpertId { get; set; }
        [JsonPropertyName("plan_name")]
        public string? PlanName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonPropertyName("estimated_product")]
        public float? EstimatedProduct { get; set; }
        [JsonPropertyName("estimated_unit")]
        public string? EstimatedUnit { get; set; }
        [JsonPropertyName("update_by")]
        public string? UpdateBy { get; set; }
    }
}

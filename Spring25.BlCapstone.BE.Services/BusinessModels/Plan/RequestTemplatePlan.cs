using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plan
{
    public class RequestTemplatePlan
    {
        [JsonPropertyName("orders")]
        public List<OrderPlanRequest>? Orders { get; set; } = null;
        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }
        [JsonPropertyName("yield_id")]
        public int YieldId { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("estimated_product")]
        public int EstimatedProduct { get; set; }
        [JsonPropertyName("seed_quantity")]
        public int SeedQuantity { get; set; }
        [JsonPropertyName("expert_id")]
        public int ExpertId { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
    }
    public class OrderPlanRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}

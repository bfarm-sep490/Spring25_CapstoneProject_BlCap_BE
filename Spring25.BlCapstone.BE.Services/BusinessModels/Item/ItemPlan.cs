using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Item
{
    public class ItemPlan
    {
        [JsonPropertyName("caring-items-in-plan")]
        public List<CaringItemPlan> CaringItemPlans { get; set; }
        [JsonPropertyName("harvesting-items-in-plan")]
        public List<HarvestingItemPlan> HarvestingItemPlans { get; set; }
        [JsonPropertyName("inspecting-items-in-plan")]
        public List<InspectingItemPlan> InspectingItemPlans { get; set; }
    }

    public class CaringItemPlan
    {
        [JsonPropertyName("item_id")]
        public int Id { get; set; }
        [JsonPropertyName("estimated_quantity")]
        public int EstimatedQuantity { get; set; }
        [JsonPropertyName("inuse_quantity")]
        public int InUseQuantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class HarvestingItemPlan
    {
        [JsonPropertyName("item_id")]
        public int Id { get; set; }
        [JsonPropertyName("estimated_quantity")]
        public int EstimatedQuantity { get; set; }
        [JsonPropertyName("inuse_quantity")]
        public int InUseQuantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class InspectingItemPlan
    {
        [JsonPropertyName("item_id")]
        public int Id { get; set; }
        [JsonPropertyName("estimated_quantity")]
        public int EstimatedQuantity { get; set; }
        [JsonPropertyName("inuse_quantity")]
        public int InUseQuantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}

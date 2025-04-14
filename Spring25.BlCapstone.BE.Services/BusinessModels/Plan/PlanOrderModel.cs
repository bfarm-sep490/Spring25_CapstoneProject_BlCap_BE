using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plan
{
    public class PlanOrderModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("orders_plan")]
        public List<OrPl>? OrderPlas { get; set; }
    }

    public class OrPl
    {
        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }
        [JsonPropertyName("order_plan_quantity")]
        public float Quantity { get; set; }
        [JsonPropertyName("retailer_id")]
        public int RetailerId { get; set; }
        [JsonPropertyName("retailer_name")]
        public string RetailerName { get; set; }
        [JsonPropertyName("pre_order_quantity")]
        public float PreOrderQuantity { get; set; }
        [JsonPropertyName("estimated_pickup_date")]
        public DateTime EstimatedPickupDate { get; set; }
    }
}

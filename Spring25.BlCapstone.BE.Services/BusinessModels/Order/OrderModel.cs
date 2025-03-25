using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Order
{
    public class OrderModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("retailer_id")]
        public int RetailerId { get; set; }
        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }
        [JsonPropertyName("plan_id")]
        public int? PlanId { get; set; }
        [JsonPropertyName("packaging_type_id")]
        public int PackagingTypeId { get; set; }
        [JsonPropertyName("deposit_price")]
        public float DepositPrice { get; set; }
        [JsonPropertyName("total_price")]
        public float? TotalPrice { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("preorder_quantity")]
        public float PreOrderQuantity { get; set; }
        [JsonPropertyName("estimate_pick_up_date")]
        public DateTime EstimatedPickupDate { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("transactions")]
        public List<TransactionOrder>? transactionOrders { get; set; }
    }

    public class TransactionOrder
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}

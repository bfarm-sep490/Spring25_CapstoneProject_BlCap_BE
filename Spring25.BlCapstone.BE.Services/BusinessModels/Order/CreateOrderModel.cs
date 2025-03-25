using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Order
{
    public class CreateOrderModel
    {
        [JsonPropertyName("retailer_id")]
        public int RetailerId { get; set; }
        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }
        [JsonPropertyName("packaging_type_id")]
        public int PackagingTypeId { get; set; }
        [JsonPropertyName("deposit_price")]
        public float DepositPrice { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("preorder_quantity")]
        public float EstimatedQuantity { get; set; }
        [JsonPropertyName("estimate_pick_up_date")]
        public DateTime EstimatedPickupDate { get; set; }
    }
}

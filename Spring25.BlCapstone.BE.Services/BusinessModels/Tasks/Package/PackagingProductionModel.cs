using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package
{
    public class PackagingProductionModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("plan_id")]
        public int PlanId { get; set; }
        [JsonPropertyName("packaging_type_id")]
        public int? PackagingTypeId { get; set; }
        [JsonPropertyName("plan_name")]
        public string? PlanName { get; set; }
        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }
        [JsonPropertyName("plant_name")]
        public string PlantName { get; set; }
        [JsonPropertyName("packaging_date")]
        public DateTime? CompleteDate { get; set; }
        [JsonPropertyName("expired_date")]
        public DateTime? ProductExpiredDate { get; set; }
        [JsonPropertyName("quantity_per_pack")]
        public float QuantityPerPack { get; set; }
        [JsonPropertyName("pack_quantity")]
        public int PackQuantity { get; set; }
        [JsonPropertyName("total_packs")]
        public int TotalPacks { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("evaluated_result")]
        public string EvaluatedResult { get; set; }
        [JsonPropertyName("orders")]
        public List<OrPro> OrderProducts { get; set; }
    }

    public class OrPro
    {
        [JsonPropertyName("order_id")]
        public int Id { get; set; }
        [JsonPropertyName("retailer_id")]
        public int RetailerId { get; set; }
        [JsonPropertyName("retailer_name")]
        public string RetailerName { get; set; }
        [JsonPropertyName("quantity_of_packs")]
        public int QuantityOfPacks { get; set; }
    }
}

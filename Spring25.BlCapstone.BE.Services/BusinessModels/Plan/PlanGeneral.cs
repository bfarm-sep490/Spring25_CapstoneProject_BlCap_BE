using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plan
{
    public class PlanGeneral
    {
        [JsonPropertyName("plan_id")]
        public int Id { get; set; }
        [JsonPropertyName("plan_name")]
        public string? PlanName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("plant_information")]
        public PlantInformation PlantInformation { get; set; }
        [JsonPropertyName("yield_information")]
        public YieldInformation? YieldInformation { get; set; }
        [JsonPropertyName("expert_information")]
        public ExpertInformation ExpertInformation { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonPropertyName("complete_date")]
        public DateTime? CompleteDate { get; set; }
        [JsonPropertyName("estimated_product")]
        public float? EstimatedProduct { get; set; }
        [JsonPropertyName("qr_code")]
        public string? QRCode { get; set; }
        [JsonPropertyName("seed_quantity")]
        public int? SeedQuantity { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }

    public class PlantInformation
    {
        [JsonPropertyName("plant_id")]
        public int Id { get; set; }
        [JsonPropertyName("plant_name")]
        public string PlantName { get; set; }
        [JsonPropertyName("plant_image")]
        public string ImageUrl { get; set; }
    }

    public class YieldInformation
    {
        [JsonPropertyName("yield_id")]
        public int Id { get; set; }
        [JsonPropertyName("yield_name")]
        public string YieldName { get; set; }
        [JsonPropertyName("area_unit")]
        public string AreaUnit { get; set; }
        [JsonPropertyName("area")]
        public double Area { get; set; }
    }

    public class ExpertInformation
    {
        [JsonPropertyName("expert_id")]
        public int Id { get; set; }
        [JsonPropertyName("expert_name")]
        public string Name { get; set; }
    }
}

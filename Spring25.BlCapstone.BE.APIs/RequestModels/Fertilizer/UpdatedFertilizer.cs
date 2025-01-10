using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer
{
    public class UpdatedFertilizer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("nutrient_content")]  
        public string NutrientContent { get; set; }

        [JsonPropertyName("storage_conditions")]
        public string StorageConditions { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("expired_date")]
        public DateTime ExpiredDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("farm_owner_id")]
        public int FarmOwnerId { get; set; }
    }
}

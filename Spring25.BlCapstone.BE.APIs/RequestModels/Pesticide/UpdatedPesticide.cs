using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide
{
    public class UpdatedPesticide
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("available_quantity")]
        public float AvailableQuantity { get; set; }
        [JsonPropertyName("total_quantity")]
        public float TotalQuantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}

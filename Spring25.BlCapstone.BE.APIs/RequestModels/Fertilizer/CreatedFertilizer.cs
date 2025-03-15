using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer
{
    public class CreatedFertilizer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("quantity")]
        public float Quantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}

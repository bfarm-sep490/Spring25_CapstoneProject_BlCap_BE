using System.Text.Json.Serialization;

namespace Spring25.BlCapstone.BE.APIs.RequestModels.Yield
{
    public class UpdatedYield
    {
        [JsonPropertyName("yield_name")]
        public string YieldName { get; set; }
        [JsonPropertyName("area_unit")]
        public string AreaUnit { get; set; }
        [JsonPropertyName("area")]
        public double Area { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("size")]
        public string Size { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}

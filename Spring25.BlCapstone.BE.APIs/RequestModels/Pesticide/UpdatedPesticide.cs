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

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("purpose")]
        public string Purpose { get; set; }

        [JsonPropertyName("safety_instructions")]
        public string SafetyInstructions { get; set; }

        [JsonPropertyName("re_entry_period")]
        public string ReEntryPeriod { get; set; }

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

using System.Text.Json.Serialization;

namespace Spring25.BlCapstone.BE.APIs.RequestModels.Plan
{
    public class AddFarmerToPlan
    {
        [JsonPropertyName("farmer_id")]
        public int FarmerId { get; set; }
    }
}

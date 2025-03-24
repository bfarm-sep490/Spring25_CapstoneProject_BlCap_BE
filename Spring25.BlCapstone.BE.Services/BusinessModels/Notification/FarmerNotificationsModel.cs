using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Notification
{
    public class FarmerNotificationsModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("farmer_id")]
        public int FarmerId { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}

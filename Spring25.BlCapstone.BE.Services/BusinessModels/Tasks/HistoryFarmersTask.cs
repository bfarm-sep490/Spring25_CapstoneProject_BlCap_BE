using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks
{
    public class HistoryFarmersTask
    {
        [JsonPropertyName("farmer_id")]
        public int FarmerId { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("expired_date")]
        public DateTime ExpiredDate { get; set; }
    }
}

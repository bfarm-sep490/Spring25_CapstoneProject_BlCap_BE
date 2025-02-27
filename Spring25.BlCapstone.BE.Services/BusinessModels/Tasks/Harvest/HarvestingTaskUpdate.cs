using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest
{
    public class HarvestingTaskUpdate
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("result-content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("list_of_image_urls")]
        public List<string> Images { get; set; }
        [JsonPropertyName("complete_date")]
        public DateTime? CompleteDate { get; set; }
        [JsonPropertyName("harvested_quantity")]
        public float? HarvestedQuantity { get; set; }
        [JsonPropertyName("harvested_unit")]
        public string? HarvestedUnit { get; set; }
    }
}

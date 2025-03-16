using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest
{
    public class HarvestingTaskReport
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("list_of_image_urls")]
        public List<string> Images { get; set; }
        [JsonPropertyName("harvested_quantity")]
        public float? HarvestedQuantity { get; set; }
        [JsonPropertyName("product_expired_date")]
        public DateTime? ProductExpiredDate { get; set; }
        [JsonPropertyName("fail_quantity")]
        public float? FailQuantity { get; set; }
        [JsonPropertyName("report_by")]
        public string? UpdatedBy { get; set; }
    }
}

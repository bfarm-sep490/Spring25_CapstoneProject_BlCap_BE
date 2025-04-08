using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package
{
    public class PackagingReport
    {
        [JsonPropertyName("harvesting_task_id")]
        public int HarvestingTaskId { get; set; }
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("packaged_item_count")]
        public int PackagedItemCount { get; set; }
        [JsonPropertyName("total_packaged_weight")]
        public float TotalPackagedWeight { get; set; }
        [JsonPropertyName("report_by")]
        public string? UpdatedBy { get; set; }
        [JsonPropertyName("list_of_image_urls")]
        public List<string>? Images { get; set; }
    }
}
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
        [JsonPropertyName("packed_unit")]
        public string? PackedUnit { get; set; }
        [JsonPropertyName("packed_quantity")]
        public int? PackedQuantity { get; set; }
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("complete_date")]
        public DateTime? CompleteDate { get; set; }
        [JsonPropertyName("list_of_image_urls")]
        public List<string> Images { get; set; }
    }
}

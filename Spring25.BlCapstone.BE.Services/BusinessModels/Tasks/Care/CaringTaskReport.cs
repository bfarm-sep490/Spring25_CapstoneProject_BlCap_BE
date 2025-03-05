using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care
{
    public class CaringTaskReport
    {
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("complete_date")]
        public DateTime? CompleteDate { get; set; }
        [JsonPropertyName("is_complete")]
        public bool IsCompleted { get; set; }
        [JsonPropertyName("list_of_image_urls")]
        public List<string> Images { get; set; }
    }
}

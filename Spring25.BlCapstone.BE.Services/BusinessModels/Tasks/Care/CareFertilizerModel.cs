using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care
{
    public class CareFertilizerModel
    {
        [JsonPropertyName("fertilizer_id")]
        public int FertilizerId { get; set; }
        [JsonPropertyName("task_id")]
        public int TaskId { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
        [JsonPropertyName("quantity")]
        public float Quantity { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Havest
{
    public class HarvestingTaskModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("plan_id")]
        public int PlanId { get; set; }

        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("farmer_name")]
        public string? FarmerName { get; set; }

        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }

        [JsonPropertyName("task_type")]
        public string TaskType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }

        [JsonPropertyName("harvest_date")]
        public DateTime? HarvestDate { get; set; }

        [JsonPropertyName("complete_date")]
        public DateTime? CompleteDate { get; set; }

        [JsonPropertyName("harvested_quantity")]
        public float? HarvestedQuantity { get; set; }

        [JsonPropertyName("harvested_unit")]
        public string? HarvestedUnit { get; set; }

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        [JsonPropertyName("is_available")]
        public bool IsAvailable { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("harvest_images")]
        public List<HarvestingImageModel> HarvestImages { get; set; }

        [JsonPropertyName("harvesting_items")]
        public List<HarvestingItemModel> HarvestingItems { get; set; }
    }

}

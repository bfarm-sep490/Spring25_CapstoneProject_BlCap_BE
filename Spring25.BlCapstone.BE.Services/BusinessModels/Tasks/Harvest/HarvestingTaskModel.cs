using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
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
        [JsonPropertyName("farmer_information")]
        public List<FarmerInfor> FarmerInfor { get; set; }
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("complete_date")]
        public DateTime? CompleteDate { get; set; }
        [JsonPropertyName("harvested_quantity")]
        public float? HarvestedQuantity { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("product_expired_date")]
        public DateTime? ProductExpiredDate { get; set; }
        [JsonPropertyName("fail_quantity")]
        public float? FailQuantity { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }
        [JsonPropertyName("harvest_images")]
        public List<HarvestingImageModel> HarvestImages { get; set; }
        [JsonPropertyName("harvesting_items")]
        public List<HarvestingItemModel> HarvestingItems { get; set; }
    }
}

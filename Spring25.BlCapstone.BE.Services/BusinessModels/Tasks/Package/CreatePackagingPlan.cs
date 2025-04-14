using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package
{
    public class CreatePackagingPlan
    {
        [JsonPropertyName("plan_id")]
        public int PlanId { get; set; }
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("order_id")]
        public int? OrderId { get; set; }
        [JsonPropertyName("packaging_type_id")]
        public int? PackagingTypeId { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("items")]
        public List<PackageItem>? Items { get; set; } = new List<PackageItem>();
    }

    public class PackageItem
    {
        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care
{
    public class CreateCaringPlan
    {
        [JsonPropertyName("plan_id")]
        public int PlanId { get; set; }
        [JsonPropertyName("problem_id")]
        public int? ProblemId { get; set; }
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("task_type")]
        public string TaskType { get; set; }
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
        [JsonPropertyName("fertilizers")]
        public List<FerCare>? Fertilizers { get; set; } = new List<FerCare>();
        [JsonPropertyName("pesticides")]
        public List<PesCare>? Pesticides { get; set; } = new List<PesCare>();
        [JsonPropertyName("items")]
        public List<ItemCare>? Items { get; set; } = new List<ItemCare>();
    }

    public class FerCare
    {
        [JsonPropertyName("fertilizer_id")]
        public int FertilizerId { get; set; }
        [JsonPropertyName("quantity")]
        public float Quantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class PesCare
    {
        [JsonPropertyName("pesticide_id")]
        public int PesticideId { get; set; }
        [JsonPropertyName("quantity")]
        public float Quantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class ItemCare
    {
        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}

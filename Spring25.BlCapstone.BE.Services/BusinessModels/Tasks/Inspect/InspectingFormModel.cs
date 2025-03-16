using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect
{
    public class InspectingFormModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("plan_id")]
        public int PlanId { get; set; }

        [JsonPropertyName("inspector_id")]
        public int? InspectorId { get; set; }
        [JsonPropertyName("inspector_name")]
        public string? InspectorName { get; set; }

        [JsonPropertyName("task_name")]
        public string FormName { get; set; }
        [JsonPropertyName("task_type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("number_of_sample")]
        public int? NumberOfSample { get; set; }
        [JsonPropertyName("sample_weight")]
        public float? SampleWeight { get; set; }
        [JsonPropertyName("can_harvest")]
        public bool CanHarvest { get; set; }

        [JsonPropertyName("complete_date")]
        public DateTime? CompleteDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }
        [JsonPropertyName("inspecting_results")]
        public InspectingResultLess InspectingResults { get; set; }
    }

    public class InspectingResultLess
    {
        [JsonPropertyName("result_id")]
        public int Id { get; set; }
        [JsonPropertyName("evaluated_result")]
        public string EvaluatedResult { get; set; }
        [JsonPropertyName("result_images")]
        public List<InspectingImageModel> InspectingImageModels { get; set; }
    }
}

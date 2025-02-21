using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care
{
    public class CaringTaskModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("plan_id")]

        public int PlanId { get; set; }
        [JsonPropertyName("yield_id")]

        public int YieldId { get; set; }
        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("problem_id")]

        public int? ProblemId { get; set; }
        [JsonPropertyName("task_name")]

        public string TaskName { get; set; }
        [JsonPropertyName("result_content")]

        public string? ResultContent { get; set; }
        [JsonPropertyName("task_type")]
        public string TaskType { get; set; }
        [JsonPropertyName("start_date")]

        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]

        public DateTime EndDate { get; set; }
        [JsonPropertyName("is_completed")]

        public bool IsCompleted { get; set; }
        [JsonPropertyName("is_available")]

        public bool IsAvailable { get; set; }
        [JsonPropertyName("priority")]
        public int Priority { get; set; }
        [JsonPropertyName("status")]

        public string Status { get; set; }
        [JsonPropertyName("create_at")]

        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("update_at")]

        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("care_images")]
        public List<CaringImageModel> CareImages { get; set; }
        [JsonPropertyName("care_pesticides")]
        public List<CarePesticideModel> CarePesticides {  get; set; }
        [JsonPropertyName("care_fertilizers")]
        public List<CareFertilizerModel> CareFertilizers { get; set; }
    }
}

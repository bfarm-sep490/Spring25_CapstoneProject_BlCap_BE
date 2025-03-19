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
        [JsonPropertyName("plan_name")]
        public string? PlanName { get; set; }
        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("farmer_information")]
        public List<FarmerInfor> FarmerInfor { get; set; }
        [JsonPropertyName("problem_id")]
        public int? ProblemId { get; set; }
        [JsonPropertyName("problem_name")]
        public string ProblemName { get; set; }
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("task_type")]
        public string TaskType { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
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
        [JsonPropertyName("care_images")]
        public List<CaringImageModel> CareImages { get; set; }
        [JsonPropertyName("care_pesticides")]
        public List<CarePesticideModel> CarePesticides {  get; set; }
        [JsonPropertyName("care_fertilizers")]
        public List<CareFertilizerModel> CareFertilizers { get; set; }
        [JsonPropertyName("care_items")]
        public List<CareItemsModel> CareItems { get; set; }
    }

    public class CareItemsModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }
        [JsonPropertyName("item_name")]
        public string Name { get; set; }
        [JsonPropertyName("task_id")]
        public int TaskId { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class FarmerInfor
    {
        [JsonPropertyName("farmer_id")]
        public int? FarmerId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("farmer_name")]
        public string Name { get; set; }
    }
}

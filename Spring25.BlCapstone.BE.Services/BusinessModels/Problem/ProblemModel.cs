using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Problem
{
    public class ProblemModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("plan_id")]
        public int PlanId { get; set; }
        [JsonPropertyName("plan_name")]
        public string PlanName { get; set; }
        [JsonPropertyName("farmer_id")]
        public int FarmerId { get; set; }
        [JsonPropertyName("farmer_name")]
        public string FarmerName { get; set; }
        [JsonPropertyName("problem_name")]
        public string ProblemName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("problem_images")]
        public List<Images> Images { get; set; }
    }

    public class Images
    {
        [JsonPropertyName("image_id")]
        public int Id { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}

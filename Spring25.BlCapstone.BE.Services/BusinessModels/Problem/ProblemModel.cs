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
        [JsonPropertyName("problem_name")]
        public string ProblemName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("problem_type")]
        public string ProblemType { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
        [JsonPropertyName("problem_images")]
        public List<Images> Images { get; set; }
        [JsonPropertyName("issues")]
        public List<ProblemIssues> ProblemIssues { get; set; }
    }

    public class Images
    {
        [JsonPropertyName("image_id")]
        public int Id { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class ProblemIssues
    {
        [JsonPropertyName("issue_id")]
        public int Id { get; set; }
        [JsonPropertyName("issue_name")]
        public string IssueName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }
}

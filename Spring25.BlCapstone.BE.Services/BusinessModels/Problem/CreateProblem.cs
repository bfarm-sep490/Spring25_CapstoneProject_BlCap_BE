using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Problem
{
    public class CreateProblem
    {
        [JsonPropertyName("issue_id")]
        public int? IssueId { get; set; }
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
        [JsonPropertyName("list_of_images")]
        public List<string>? ImageUrl { get; set; }
    }
}

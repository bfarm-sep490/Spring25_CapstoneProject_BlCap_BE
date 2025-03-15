using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Problem
{
    public class ProblemPlan
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("problem_name")]
        public string ProblemName { get; set; }
        [JsonPropertyName("farmer_id")]
        public int FarmerId { get; set; }
        [JsonPropertyName("farmer_name")]
        public string Name { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
    }
}

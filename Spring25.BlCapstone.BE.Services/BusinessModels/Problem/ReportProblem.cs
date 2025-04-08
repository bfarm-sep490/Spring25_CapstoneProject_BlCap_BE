using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Problem
{
    public class ReportProblem
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("result_content")]
        public string? ResultContent { get; set; }
    }
}

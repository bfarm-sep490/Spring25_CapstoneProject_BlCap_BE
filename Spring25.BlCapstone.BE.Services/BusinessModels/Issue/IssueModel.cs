using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Issue
{
    public class IssueModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("issue_name")]
        public string IssueName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }
}

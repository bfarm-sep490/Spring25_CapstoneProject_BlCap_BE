using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Farmer
{
    public class FarmerModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("avatar_image")]
        public string Avatar { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
        [JsonPropertyName("list_farmer_specializations")]
        public List<Special> FarmerSpecials { get; set; }
        [JsonPropertyName("complete-tasks")]
        public int CompletedTasks { get; set; }
        [JsonPropertyName("incomplete-tasks")]
        public int IncompleteTasks { get; set; }
        [JsonPropertyName("performance-score")]
        public double? PerformanceScore { get; set; }
    }

    public class Special
    {
        [JsonPropertyName("special_id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

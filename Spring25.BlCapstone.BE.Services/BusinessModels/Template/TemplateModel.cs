using Spring25.BlCapstone.BE.Repositories.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Template
{
    public class TemplateModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("season_type")]
        public string SeasonType { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("estimated_per_one")]
        public float EstimatedPerOne { get; set; }

        [JsonPropertyName("duration_days")]
        public int DurationDays { get; set; }
        [JsonPropertyName("plant_template")]
        public PlanTemplate? Template { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest
{
    public class UpdateHarvestingTask
    {
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }
        [JsonPropertyName("items")]
        public List<HarvestItem>? Items { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect
{
    public class InspectingImageModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("result_id")]
        public int ResultId { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}

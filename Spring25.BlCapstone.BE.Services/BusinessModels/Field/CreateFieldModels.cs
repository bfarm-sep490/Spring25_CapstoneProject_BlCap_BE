﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Fields
{
    public class CreateFieldModels
    {
        [JsonPropertyName("area")]
        public double Area { get; set; }
        [JsonPropertyName("farm_owner_id")]
        public int FarmOwnerId { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("wide")]
        public double Wide { get; set; }
        [JsonPropertyName("long")]
        public double Long { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("image_fields")]
        public List<IFormFile>? ImageFields { get; set; }
    }
}

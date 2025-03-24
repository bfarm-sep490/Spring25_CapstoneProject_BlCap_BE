﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package
{
    public class PackagingProductionModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("plan_id")]
        public int PlanId { get; set; }
        [JsonPropertyName("plan_name")]
        public string? PlanName { get; set; }
        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }
        [JsonPropertyName("plant_name")]
        public string PlantName { get; set; }
        [JsonPropertyName("packaging_date")]
        public DateTime? CompleteDate { get; set; }
        [JsonPropertyName("expired_date")]
        public DateTime? ProductExpiredDate { get; set; }
        [JsonPropertyName("packaging_quantity")]
        public int? PackedQuantity { get; set; }
        [JsonPropertyName("packaging_unit")]
        public string? PackedUnit { get; set; }
        //[JsonPropertyName("available_packaging_quantity")]
        //public float? AvailableHarvestingQuantity { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}

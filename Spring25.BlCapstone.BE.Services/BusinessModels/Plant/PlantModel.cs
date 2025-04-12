using Spring25.BlCapstone.BE.Services.BusinessModels.Yield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plant
{
    public class PlantModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("plant_name")]
        public string PlantName { get; set; }
        [JsonPropertyName("quantity")]
        public float Quantity { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("base_price")]
        public float BasePrice { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }
        [JsonPropertyName("delta_one")]
        public float DeltaOne { get; set; }
        [JsonPropertyName("delta_two")]
        public float DeltaTwo { get; set; }
        [JsonPropertyName("delta_three")]
        public float DeltaThree { get; set; }
        [JsonPropertyName("preservation_day")]
        public int PreservationDay { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}

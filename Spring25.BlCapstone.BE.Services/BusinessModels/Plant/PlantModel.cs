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
        [JsonPropertyName("estimated_per_one")]
        public float EstimatedPerOne { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        #region
        /*       [JsonPropertyName("is_available")]
                 public bool IsAvailable { get; set; }
                 [JsonPropertyName("unit")]
                 public string Unit { get; set; }
                 [JsonPropertyName("min_temp")]
                 public double MinTemp { get; set; }
                 [JsonPropertyName("max_temp")]
                 public double MaxTemp { get; set; }
                 [JsonPropertyName("min_humid")]
                 public double MinHumid { get; set; }
                 [JsonPropertyName("max_humid")]
                 public double MaxHumid { get; set; }
                 [JsonPropertyName("min_moisture")]
                 public double MinMoisture { get; set; }
                 [JsonPropertyName("max_moisture")]
                 public double MaxMoisture { get; set; }
                 [JsonPropertyName("min_fertilizer")]
                 public double MinFertilizer { get; set; }
                 [JsonPropertyName("max_fertilizer")]
                 public double MaxFertilizer { get; set; }
                 [JsonPropertyName("fertilizer_unit")]
                 public string FertilizerUnit { get; set; }
                 [JsonPropertyName("min_pesticide")]
                 public double MinPesticide { get; set; }
                 [JsonPropertyName("max_pesticide")]
                 public double MaxPesticide { get; set; }
                 [JsonPropertyName("pesticide_unit")]
                 public string PesticideUnit { get; set; }
                 [JsonPropertyName("min_brix_point")]
                 public double MinBrixPoint { get; set; }
                 [JsonPropertyName("max_brix_point")]
                 public double MaxBrixPoint { get; set; } 
                 [JsonPropertyName("gt_test_kit_color")]
                 public string GTTestKitColor { get; set; }
                 [JsonPropertyName("image_url")]
                 public string ImageUrl { get; set; }
         */
        #endregion
    }
}

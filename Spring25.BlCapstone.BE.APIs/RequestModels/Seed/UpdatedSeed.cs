using System.Text.Json.Serialization;

namespace Spring25.BlCapstone.BE.APIs.RequestModels.Seed
{
    public class UpdatedSeed
    {
        [JsonPropertyName("seed_name")]
        public string SeedName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("is_available")]
        public bool IsAvailable { get; set; }
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
    }
}

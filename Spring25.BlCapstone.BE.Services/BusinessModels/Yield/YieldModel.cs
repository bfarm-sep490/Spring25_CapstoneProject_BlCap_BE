using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Yield
{
    public class YieldModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("yield_name")]
        public string YieldName { get; set; }

        [JsonPropertyName("area_unit")]
        public string AreaUnit { get; set; }

        [JsonPropertyName("area")]
        public double Area { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plant
{
    public class PlantYieldModel
    {
        [JsonPropertyName("yield_id")]
        public int YieldId { get; set; }
        [JsonPropertyName("plant_id")]

        public int PlantId { get; set; }
    }
}

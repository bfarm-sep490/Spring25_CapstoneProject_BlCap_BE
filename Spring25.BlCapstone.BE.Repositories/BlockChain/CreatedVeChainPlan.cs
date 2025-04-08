using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.BlockChain
{
    public class CreatedVeChainPlan
    {
        [JsonPropertyName("_planId")]
        public int PlanId { get; set; }

        [JsonPropertyName("_plantId")]
        public int PlantId { get; set; }

        [JsonPropertyName("_yieldId")]
        public int YieldId { get; set; }

        [JsonPropertyName("_expertId")]
        public int ExpertId { get; set; }

        [JsonPropertyName("_planName")]
        public string PlanName { get; set; }

        [JsonPropertyName("_startDate")]
        public string StartDate { get; set; }

        [JsonPropertyName("_endDate")]
        public string EndDate { get; set; }

        [JsonPropertyName("_estimatedProduct")]
        public double EstimatedProduct { get; set; }

        [JsonPropertyName("_estimatedUnit")]
        public int EstimatedUnit { get; set; }

        [JsonPropertyName("_status")]
        public string Status { get; set; }
    }

}

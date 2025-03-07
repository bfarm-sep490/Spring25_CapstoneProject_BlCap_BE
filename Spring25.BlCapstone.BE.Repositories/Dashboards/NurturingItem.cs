using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Dashboards
{
    public class NurturingItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("estimate_quantity")]
        public float EstimateQuantity {  get; set; }
        [JsonPropertyName("used_quantity")]
        public float UsedQuantity {  get; set; }
        [JsonPropertyName("unit")]
        public string Unit {  get; set; }
    }
}

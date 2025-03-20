using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Dashboards
{
    public class CaringType
    {
        [JsonIgnore()]
        public string Type { get; set; }
        [JsonPropertyName("ongoing_quantity")]
        public int OnGoingQuantity { get; set; }
        [JsonPropertyName("pending_quantity")]
        public int PendingQuantity { get; set; }
        [JsonPropertyName("incomplete_quantity")]
        public int InCompleteQuantity { get; set; }
        [JsonPropertyName("cancel_quantity")]
        public int CancelQuantity { get; set; }
        [JsonPropertyName("complete_quantity")]
        public int CompleteQuantity { get; set; }
    }
    public class TypeTasksStatus
    {
        [JsonPropertyName("watering")]
        public CaringType Watering { get; set; } = new CaringType();
        [JsonPropertyName("nuturing")]
        public CaringType Nuturing {get; set; } = new CaringType();
        [JsonPropertyName("planting")]
        public CaringType Planting { get; set; } = new CaringType();
        [JsonPropertyName("fertilizer")]
        public CaringType Fertilizer { get; set; } = new CaringType();
        [JsonPropertyName("pesticide")]
        public CaringType Pesticide { get; set; }= new CaringType();
        [JsonPropertyName("weeding")]
        public CaringType Weeding { get; set; } = new CaringType();
        [JsonPropertyName("pruning")]
        public CaringType Pruning { get; set; } = new CaringType();
    }
}

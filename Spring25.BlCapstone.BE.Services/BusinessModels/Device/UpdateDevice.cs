using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Device
{
    public class UpdateDevice
    {
        [JsonPropertyName("yield_id")]
        public int YieldId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("device_code")]
        public string DeviceCode { get; set; }
        [JsonPropertyName("installation_date")]
        public DateTime InstallationDate { get; set; }
        [JsonPropertyName("updated_by")]
        public string UpdatedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Farmer
{
    public class FarmerModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("avatar_image")]
        public string AvatarImage { get; set; }

        [JsonPropertyName("ci_number")]
        public string CINumber { get; set; }

        [JsonPropertyName("ci_before_image")]
        public string CIBeforeImage { get; set; }

        [JsonPropertyName("ci_after_image")]
        public string CIAfterImage { get; set; }

        [JsonPropertyName("device_address")]
        public string DeviceAddress { get; set; }

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}

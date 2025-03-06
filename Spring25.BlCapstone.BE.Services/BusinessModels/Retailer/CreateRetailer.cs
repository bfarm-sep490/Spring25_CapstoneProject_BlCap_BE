using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Retailer
{
    public class CreateRetailer
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("dob")]
        public DateTime? DOB { get; set; }
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("avatar_url")]
        public string? Avatar { get; set; }
        [JsonPropertyName("long_&_lat")]
        public string? LongxLat { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
    }
}

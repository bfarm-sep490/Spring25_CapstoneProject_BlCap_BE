using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Auth
{
    public class DeviceTokenModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}

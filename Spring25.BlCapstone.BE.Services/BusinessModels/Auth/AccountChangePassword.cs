using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Auth
{
    public class AccountChangePassword
    {
        [JsonPropertyName("old_password")]
        public string OldPassword { get; set; }
        [JsonPropertyName("new_password")]
        public string NewPassword { get; set; }
    }
}

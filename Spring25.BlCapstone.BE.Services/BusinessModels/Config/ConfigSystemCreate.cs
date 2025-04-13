using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Config
{
    public class ConfigSystemCreate
    {
        [JsonPropertyName("deposit_percent")]
        public float DepositPercent { get; set; }
    }
}

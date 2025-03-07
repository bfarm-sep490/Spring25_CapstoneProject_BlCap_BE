using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Dashboards
{
    public class HavestedTask
    {
       
        [JsonPropertyName("havested-value")]
        public float HavestedValue { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}

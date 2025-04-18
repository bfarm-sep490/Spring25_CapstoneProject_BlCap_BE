using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Transaction
{
    public class DashboardTransactions
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("price_per_day")]
        public float PricePerDay { get; set; }
    }
}

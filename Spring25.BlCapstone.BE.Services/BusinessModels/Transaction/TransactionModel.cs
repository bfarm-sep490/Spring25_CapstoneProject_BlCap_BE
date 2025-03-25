using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Transaction
{
    public class TransactionModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }
        [JsonPropertyName("order_code")]
        public long OrderCode { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("payment_date")]
        public DateTime? PaymentDate { get; set; }
    }
}

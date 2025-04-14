using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Order
{
    public class BatchModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }
        [JsonPropertyName("quantity")]
        public float Quantity { get; set; }
        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}

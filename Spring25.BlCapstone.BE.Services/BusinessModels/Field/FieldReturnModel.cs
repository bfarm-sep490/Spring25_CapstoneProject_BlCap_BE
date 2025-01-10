using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Fields
{
    public class FieldReturnModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("area")]
        public double Area { get; set; }
        [JsonPropertyName("farm_owner_id")]
        public int FarmOwnerId { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("wide")]
        public double Wide { get; set; }
        [JsonPropertyName("long")]
        public double Long { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("updated_by")]
        public string UpdatedBy { get; set; }
        [JsonPropertyName("image_of_fields")]
        public List<ImagesOfFields> ImageOfField { get; set; }
    }

    public class ImagesOfFields
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}

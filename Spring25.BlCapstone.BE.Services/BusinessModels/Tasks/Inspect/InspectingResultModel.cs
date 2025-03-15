using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect
{
    public class InspectingResultModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("arsen")]
        public float Arsen { get; set; }
        [JsonPropertyName("plumbum")]
        public float Plumbum { get; set; }
        [JsonPropertyName("cadmi")]
        public float Cadmi { get; set; }
        [JsonPropertyName("hydrargyrum")]
        public float Hydrargyrum { get; set; }
        [JsonPropertyName("salmonella")]
        public float Salmonella { get; set; }
        [JsonPropertyName("coliforms")]
        public float Coliforms { get; set; }
        [JsonPropertyName("ecoli")]
        public float Ecoli { get; set; }
        [JsonPropertyName("glyphosate_glufosinate")]
        public float Glyphosate_Glufosinate { get; set; }
        [JsonPropertyName("sulfur_dioxide")]
        public float SulfurDioxide { get; set; }
        [JsonPropertyName("methyl_bromide")]
        public float MethylBromide { get; set; }
        [JsonPropertyName("hydrogen_phosphide")]
        public float HydrogenPhosphide { get; set; }
        [JsonPropertyName("dithiocarbamate")]
        public float Dithiocarbamate { get; set; }
        [JsonPropertyName("nitrat")]
        public float Nitrat { get; set; }
        [JsonPropertyName("nano3_kno3")]
        public float NaNO3_KNO3 { get; set; }
        [JsonPropertyName("chlorate")]
        public float Chlorate { get; set; }
        [JsonPropertyName("perchlorate")]
        public float Perchlorate { get; set; }
        [JsonPropertyName("evaluated_result")]
        public string EvaluatedResult { get; set; }
        [JsonPropertyName("inspect_images")]
        public List<InspectingImageModel> InspectingImageModels { get; set; }
    }
}

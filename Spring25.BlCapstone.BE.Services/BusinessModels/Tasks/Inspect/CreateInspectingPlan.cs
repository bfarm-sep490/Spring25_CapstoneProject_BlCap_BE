using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect
{
    public class CreateInspectingPlan
    {
        [JsonPropertyName("plan_id")]
        public int PlanId { get; set; }
        [JsonPropertyName("task_name")]
        public string FormName { get; set; }
        [JsonPropertyName("task_type")]
        public string FormType { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
    }
}

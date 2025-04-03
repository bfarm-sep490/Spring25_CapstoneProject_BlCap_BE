using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks
{
    public class SuggestTasksModel
    {
        [JsonPropertyName("plan_id")]
        public int PlanId {  get; set; }
        [JsonPropertyName("create_havesting_plans")]
        public List<CreateHarvestingPlan> CreateHarvestingPlans { get;set; } = new List<CreateHarvestingPlan>();
        [JsonPropertyName("create_caring_plans")]
        public List<CreateCaringPlan> CreateCaringPlans { get; set; } = new List<CreateCaringPlan> { };
        [JsonPropertyName("create_packaging_plans")]
        public List<CreatePackagingPlan> CreatePackagingPlans { get; set; } = new List<CreatePackagingPlan> { };
        [JsonPropertyName("create_inspecting_plans")]
        public List<CreateInspectingPlan> CreateInspectingPlans { get; set; } = new List<CreateInspectingPlan> { };

    }
}

using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Plan
{
    public class CreatePlanTemplate
    {
        [JsonPropertyName("orders")]
        public List<PO>? Orders { get; set; } = new List<PO>();
        [JsonPropertyName("plant_id")]
        public int PlantId { get; set; }
        [JsonPropertyName("yield_id")]
        public int? YieldId { get; set; }
        [JsonPropertyName("expert_id")]
        public int ExpertId { get; set; }
        [JsonPropertyName("plan_name")]
        public string? PlanName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("season_name")]
        public string? SeasonName { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonPropertyName("estimated_product")]
        public float? EstimatedProduct { get; set; }
        [JsonPropertyName("seed_quantity")]
        public int? SeedQuantity { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("caring_tasks")]
        public List<PlanCare>? PlanCaringTasks { get; set; } = new List<PlanCare>();
        [JsonPropertyName("harvesting_tasks")]
        public List<PlanHar>? PlanHarvestingTasks { get; set; } = new List<PlanHar>();
        [JsonPropertyName("inspecting_forms")]
        public List<PlanForm>? PlanInspectingForms { get; set; } = new List<PlanForm>();
        [JsonPropertyName("packaging_tasks")]
        public List<PlanPack>? PlanPackagingTasks { get; set; } = new List<PlanPack>();
    }

    public class PlanCare
    {
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("task_type")]
        public string TaskType { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("fertilizers")]
        public List<FerCare>? Fertilizers { get; set; } = new List<FerCare>();
        [JsonPropertyName("pesticides")]
        public List<PesCare>? Pesticides { get; set; } = new List<PesCare>();
        [JsonPropertyName("items")]
        public List<ItemCare>? Items { get; set; } = new List<ItemCare>();
    }
    
    public class PlanHar
    {
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("items")]
        public List<HarvestItem>? Items { get; set; } = new List<HarvestItem>();
    }
    
    public class PlanForm
    {
        [JsonPropertyName("task_name")]
        public string FormName { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
    }
    
    public class PlanPack
    {
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }
        [JsonPropertyName("packaging_type_id")]
        public int? PackagingTypeId { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("total_package_weight")]
        public float TotalPackagedWeight { get; set; }

        [JsonPropertyName("items")]
        public List<PackageItem>? Items { get; set; } = new List<PackageItem>();
    }
    public class OrdPlan
    {
        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }
        [JsonPropertyName("quantity")]
        public float Quantity { get; set; }
    }
}

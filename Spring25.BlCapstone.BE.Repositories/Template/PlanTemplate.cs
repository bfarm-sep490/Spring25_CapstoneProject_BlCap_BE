using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Template
{
    using System.Text.Json.Serialization;

    public class PlanTemplate
    {
        [JsonPropertyName("season_type")]
        public string SeasonType { get; set; }

        [JsonPropertyName("sample_quantity")]
        public float SampleQuantity { get; set; }

        [JsonPropertyName("caring_tasks")]
        public List<CaringTaskTemplate> CaringTasks { get; set; }

        [JsonPropertyName("inspecting_tasks")]
        public List<InspectingTaskTemplate> InspectingTasks { get; set; }

        [JsonPropertyName("harvesting_task_tempates")]
        public List<HarvestingTaskTempate> HarvestingTaskTempates { get; set; }
    }

    public class HarvestingTaskTempate
    {
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("start_in")]
        public float StartIn { get; set; }

        [JsonPropertyName("end_in")]
        public float EndIn { get; set; }

        [JsonPropertyName("items")]
        public List<ItemTemplate>? Items { get; set; } = new List<ItemTemplate>();
    }

    public class PesticideTemplate
    {
        [JsonPropertyName("pesticide_id")]
        public int PesticideId { get; set; }

        [JsonPropertyName("quantity")]
        public float Quantity { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class FertilizerTemplate
    {
        [JsonPropertyName("fertilizer_id")]
        public int FertilizerId { get; set; }

        [JsonPropertyName("quantity")]
        public float Quantity { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class ItemTemplate
    {
        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class InspectingTaskTemplate
    {
        [JsonPropertyName("form_name")]
        public string FormName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("start_in")]
        public float StartIn { get; set; }

        [JsonPropertyName("end_in")]
        public float EndIn { get; set; }
    }

    public class CaringTaskTemplate
    {
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("task_type")]
        public string TaskType { get; set; }

        [JsonPropertyName("start_in")]
        public float StartIn { get; set; }

        [JsonPropertyName("end_in")]
        public float EndIn { get; set; }

        [JsonPropertyName("fertilizers")]
        public List<FertilizerTemplate>? Fertilizers { get; set; } = new List<FertilizerTemplate>();

        [JsonPropertyName("pesticides")]
        public List<PesticideTemplate>? Pesticides { get; set; } = new List<PesticideTemplate>();

        [JsonPropertyName("items")]
        public List<ItemTemplate>? Items { get; set; } = new List<ItemTemplate>();
    }


}

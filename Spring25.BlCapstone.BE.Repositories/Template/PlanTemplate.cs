using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Template
{
    public class PlanTemplate
    {
        public string SeasonType { get; set;}
        public float SampleQuantity {  get; set;}
        public List<CaringTaskTemplate> CaringTasks { get; set;}
        public List<InspectingTaskTemplate> InspectingTasks { get; set;}
        public List<HarvestingTaskTempate> HarvestingTaskTempates { get; set;}
    }

    public class HarvestingTaskTempate
    {
        public string TaskName { get; set; }
        public string? Description { get; set; }
        public float StartIn { get; set; }
        public float EndIn { get; set; }
        public List<ItemTemplate>? Items { get; set; } = new List<ItemTemplate>();
    }

    public class PesticideTemplate
    {
        public int PesticideId { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
    }

    public class FertilizerTemplate
    {
        public int FertilizerId { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }

    }
    public class ItemTemplate
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
    public class InspectingTaskTemplate
    {
        public string FormName { get; set; }
        public string? Description { get; set; }
        public float StartIn { get; set; }
        public float EndIn { get; set; }
    }

    public class CaringTaskTemplate
    {
        public string TaskName { get; set; }
        public string? Description { get; set; }
        public string TaskType { get; set; }
        public float StartIn { get; set; }
        public float EndIn { get; set; }
        public List<FertilizerTemplate>? Fertilizers { get; set; } = new List<FertilizerTemplate>();
        public List<PesticideTemplate>? Pesticides { get; set; } = new List<PesticideTemplate>();
        public List<ItemTemplate>? Items { get; set; } = new List<ItemTemplate>();
    }

}

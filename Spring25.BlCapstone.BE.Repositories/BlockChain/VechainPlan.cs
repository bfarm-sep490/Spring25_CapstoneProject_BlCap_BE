using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.BlockChain
{
    public class VechainResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public VechainPlan Data { get; set; }
    }
    public class VechainPlan
    {
        public PlanDetail Plan { get; set; }
        public List<TaskMilestone> TaskMilestones { get; set; }
        public List<InspectionMilestone> InspectionMilestones { get; set; }
    }

    public class PlanDetail
    {
        public int PlanId { get; set; }
        public int PlantId { get; set; }
        public int YieldId { get; set; }
        public int ExpertId { get; set; }
        public string PlanName { get; set; }
        public long StartDate { get; set; }
        public long EndDate { get; set; }
        public double EstimatedProduct { get; set; }
        public string EstimatedUnit { get; set; }
        public string Status { get; set; }
    }

    public class TaskMilestone
    {
        public int TaskId { get; set; }
        public string TaskType { get; set; }
        public long Timestamp { get; set; }
        public string Status { get; set; }
        public string Data { get; set; }
        //public DataTask Data { get; set; } = new DataTask();
    }

    public class InspectionMilestone
    {
        public int InspectionId { get; set; }
        public long Timestamp { get; set; }
        public int InspectionType { get; set; }
        public string Data { get; set; }
        //public DataInspect Data { get; set; } = new DataInspect();
    }
    public class VeChainFarmer
    {
        public int Id { get; set; }
        public string Name { get; set; } // Name
    }

    public class DataInspect
    {
        public VeChainFarmer Inspector { get; set; }
        public float Arsen { get; set; }
        public float Plumbum { get; set; }
        public float Cadmi { get; set; }
        public float Hydrargyrum { get; set; }
        public float Salmonella { get; set; }
        public float Coliforms { get; set; }
        public float Ecoli { get; set; }
        public float Glyphosate_Glufosinate { get; set; }
        public float SulfurDioxide { get; set; }
        public float MethylBromide { get; set; }
        public float HydrogenPhosphide { get; set; }
        public float Dithiocarbamate { get; set; }
        public float Nitrat { get; set; }
        public float NaNO3_KNO3 { get; set; }
        public float Chlorate { get; set; }
        public float Perchlorate { get; set; }
        public string ResultContent { get; set; }
        public string Timestamp { get; set; } 
    }

    public class DataTask
    {
        public string Description { get; set; } // Description
        public VeChainFarmer Farmer { get; set; } // Farmer
        public List<VeChainItem> Fertilizers { get; set; } = new List<VeChainItem>(); // Fertilizers
        public List<VeChainItem> Pesticides { get; set; } = new List<VeChainItem>();  // Pesticides
        public List<VeChainItem> Items { get; set; } = new List<VeChainItem>();  // Item
        public string Timestamp { get; set; } // Timestamp
    }

    public class VeChainItem
    {
        public int Id { get; set; }
        public string Name { get; set; } // Name
        public float Quantity { get; set; }    // Quantity
        public string Unit { get; set; } // Unit
    }
}

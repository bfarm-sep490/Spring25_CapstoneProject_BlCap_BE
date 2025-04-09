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
        public string Id { get; set; }
        public string N { get; set; } // Name
    }
    public class DataInspect
    {
        public string D { get; set; }          // Description
        public VeChainFarmer F { get; set; }          // Farmer
        public List<VeChainItem> Ft { get; set; }     // Fertilizers
        public List<VeChainItem> P { get; set; }      // Pesticides
        public List<VeChainItem> I { get; set; }      // Item
        public string T { get; set; }            // Timestamp
    }
    public class DataTask
    {
        public string D { get; set; }// Description
        public Farmer F { get; set; }// Farmer
        public List<VeChainItem> Ft { get; set; }// Fertilizers
        public List<VeChainItem> P { get; set; } // Pesticides
        public List<VeChainItem> I { get; set; }  // Item
        public string T { get; set; } // Timestamp
    }
    public class VeChainItem
    {
        public string Id { get; set; }
        public string N { get; set; } // Name
        public int Q { get; set; }    // Quantity
        public string U { get; set; } // Unit
    }
}

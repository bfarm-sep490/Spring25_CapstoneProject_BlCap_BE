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
        public VeChainPlan Plan { get; set; }
        public List<TaskMilestone> TaskMilestones { get; set; }
        public List<InspectionMilestone> InspectionMilestones { get; set; }
    }

    public class VeChainPlan
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
        public string DataHash { get; set; }
    }

    public class InspectionMilestone
    {

    }
}

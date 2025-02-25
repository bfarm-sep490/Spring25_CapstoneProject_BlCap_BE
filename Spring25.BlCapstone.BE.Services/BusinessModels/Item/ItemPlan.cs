using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Item
{
    public class ItemPlan
    {
        public List<CaringItemPlan> CaringItemPlans { get; set; }
        public List<HarvestingItemPlan> HarvestingItemPlans { get; set; }
        public List<InspectingItemPlan> InspectingItemPlans { get; set; }
    }

    public class CaringItemPlan
    {
        public int Id { get; set; }
        public int EstimatedQuantity { get; set; }
        public int InUseQuantity { get; set; }
        public string Unit { get; set; }
    }

    public class HarvestingItemPlan
    {
        public int Id { get; set; }
        public int EstimatedQuantity { get; set; }
        public int InUseQuantity { get; set; }
        public string Unit { get; set; }
    }

    public class InspectingItemPlan
    {
        public int Id { get; set; }
        public int EstimatedQuantity { get; set; }
        public int InUseQuantity { get; set; }
        public string Unit { get; set; }
    }
}

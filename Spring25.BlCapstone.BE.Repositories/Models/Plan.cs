using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        public int SeedId { get; set; }
        public int YieldId { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public float EstimatedProduct { get; set; }
        public string EstimatedUnit { get; set; }
        public int AvailablePackagingQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsApproved { get; set; }

        public Seed Seed { get; set; }
        public Yield Yield { get; set; }
        public ICollection<PackedProduct> PackedProducts { get; set; }
        public ICollection<ExpertPermission> ExpertPermissions { get; set; }
        public ICollection<FarmerPermission> FarmerPermissions { get; set; }
        public ICollection<YieldPlan> YieldPlans { get; set; }
        public ICollection<Period> Periods { get; set; }
    }
}

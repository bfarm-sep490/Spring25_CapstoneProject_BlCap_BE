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
        public int PlantId { get; set; }
        public int? YieldId {  get; set; }
        public int ExpertId { get; set; }
        public string? PlanName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string Status { get; set; }
        public float? EstimatedProduct { get; set; }
        public string? EstimatedUnit { get; set; }
        public string? QRCode { get; set; }
        public int? SeedQuantity { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public Plant Plant { get; set; }
        public Expert Expert { get; set; }
        public Yield Yield { get; set; }  
        public ICollection<Order> Orders { get; set; }
        public ICollection<FarmerPermission> FarmerPermissions { get; set; }
        public ICollection<Problem> Problems { get; set; }
        public ICollection<CaringTask> CaringTasks { get; set; }
        public ICollection<HarvestingTask> HarvestingTasks { get; set; }
        public ICollection<InspectingForm> InspectingForms { get; set; }
        public ICollection<PackagingTask> PackagingTasks { get; set; }
        public ICollection<PlanTransaction> PlanTransactions { get; set; }
    }
}

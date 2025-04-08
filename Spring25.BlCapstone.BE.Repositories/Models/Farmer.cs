using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Farmer
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime? DOB { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }

        public Account Account { get; set; }
        public FarmerPerformance FarmerPerformance { get; set; }
        public ICollection<FarmerPermission> FarmerPermissions { get; set; }
        public ICollection<NotificationFarmer> NotificationFarmers { get; set; }
        public ICollection<Problem> Problems { get; set; }
        public ICollection<FarmerCaringTask> FarmerCaringTasks { get; set; }
        public ICollection<FarmerHarvestingTask> FarmerHarvestingTasks { get; set; }
        public ICollection<FarmerPackagingTask> FarmerPackagingTasks { get; set; }
        public ICollection<FarmerSpecialization> FarmerSpecializations { get; set; }
    }
}

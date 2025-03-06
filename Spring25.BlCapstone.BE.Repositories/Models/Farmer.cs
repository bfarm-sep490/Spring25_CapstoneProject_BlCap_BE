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
        public string? Status { get; set; }
        public string? Avatar { get; set; }

        public Account Account { get; set; }
        public ICollection<FarmerPermission> FarmerPermissions { get; set; }
        public ICollection<CaringTask> CaringTasks { get; set; }
        public ICollection<HarvestingTask> HarvestingTasks { get; set; }
        public ICollection<PackagingTask> PackagingTasks { get; set; }
    }
}

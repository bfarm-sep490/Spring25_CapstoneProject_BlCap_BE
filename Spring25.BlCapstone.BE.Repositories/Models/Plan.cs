using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        public int FieldId { get; set; }
        public int SeedId { get; set; }
        public string PlanName { get; set; }
        public double Area { get; set; }
        public string FieldType { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public double TotalHarvestedQuantity { get; set; }
        public DateTime? EndDate { get; set; }
        public double Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public Field Field { get; set; }
        public Seed Seed { get; set; }
        public ICollection<Period> Periods { get; set; }
    }
}

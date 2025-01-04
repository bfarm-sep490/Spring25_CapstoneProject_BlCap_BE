using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Pesticide
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Purpose { get; set; }
        public string SafetyInstructions { get; set; }
        public string ReEntryPeriod {  get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }
        public DateOnly ExpriredDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int FarmOrnerId {  get; set; }

        public FarmOwner Owner { get; set; }
        public ICollection<TaskPesticide> TaskPesticides { get; set; }
    }
}

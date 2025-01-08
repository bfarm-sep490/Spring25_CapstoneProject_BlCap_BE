using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide
{
    public class PesticideModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Purpose { get; set; }
        public string SafetyInstructions { get; set; }
        public string ReEntryPeriod { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateOnly ExpiredDate { get; set; }
        public string Status { get; set; }
        // public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int FarmOwnerId { get; set; }
        // public FarmOwner Owner { get; set; }
    }
}

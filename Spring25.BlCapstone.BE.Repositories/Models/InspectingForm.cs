using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class InspectingForm
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int? InspectorId { get; set; }
        public string FormName { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? NumberOfSample { get; set; }
        public float? SampleWeight { get; set; }
        public DateTime? CompleteDate { get; set; }
        public bool CanHarvest { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        
        public Plan Plan { get; set; }
        public Inspector Inspector { get; set; }
        public InspectingResult InspectingResult { get; set; }
    }
}

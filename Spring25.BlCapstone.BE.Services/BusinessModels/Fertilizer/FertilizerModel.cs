using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer
{
    public class FertilizerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string NutrientContent { get; set; }
        public string StorageConditions { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }
        public DateOnly ExpriredDate { get; set; }
        public string Status { get; set; }
        // public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int FarmOrnerId { get; set; }
        // public FarmOwner Owner { get; set; }
    }
}

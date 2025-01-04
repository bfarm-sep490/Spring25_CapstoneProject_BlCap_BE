using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Fields
{
    public class FieldReturnModel
    {
        public int Id { get; set; }
        public double Area { get; set; }
        public int FarmOwnerId { get; set; }
        public string Description { get; set; }
        public double Wide { get; set; }
        public double Long { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public List<ImagesOfFields> ImageOfField { get; set; }
    }

    public class ImagesOfFields
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
    }
}

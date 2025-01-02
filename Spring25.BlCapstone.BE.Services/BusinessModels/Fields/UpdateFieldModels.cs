using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Fields
{
    public class UpdateFieldModels
    {
        public double Area { get; set; }
        public int FarmOwnerId { get; set; }
        public string Description { get; set; }
        public double Wide { get; set; }
        public double Long { get; set; }
        public string Type { get; set; }
        public string UpdatedBy { get; set; }
        public List<IFormFile>? ImageFields { get; set; }
    }
}

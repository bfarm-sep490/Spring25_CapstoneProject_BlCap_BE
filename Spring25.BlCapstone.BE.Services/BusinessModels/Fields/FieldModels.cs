using Microsoft.AspNetCore.Http;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Fields
{
    public class FieldModels
    {
        public double Area { get; set; }
        public int FarmOwnerId { get; set; }
        public string Description { get; set; }
        public double Wide { get; set; }
        public double Long { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public List<IFormFile>? ImageFields { get; set; }
    }
}

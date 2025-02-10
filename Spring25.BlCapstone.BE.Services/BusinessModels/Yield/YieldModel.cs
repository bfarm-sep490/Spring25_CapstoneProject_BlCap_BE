using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Yield
{
    public class YieldModel
    {
        public int Id { get; set; }
        public string YieldName { get; set; }
        public string AreaUnit { get; set; }
        public double Area { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }
        public string Size { get; set; }
    }
}

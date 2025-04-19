using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks
{
    public class FilterRequestNotCaring
    {
        public int? plan_id { get; set; }
        public int? farmer_id { get; set; }
        public List<string>? status { get; set; }
        public int? page_number { get; set; }
        public int? page_size { get; set; }
    }
}

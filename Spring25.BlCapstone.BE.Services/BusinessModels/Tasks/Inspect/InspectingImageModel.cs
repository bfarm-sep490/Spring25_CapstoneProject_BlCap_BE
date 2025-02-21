using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect
{
    public class InspectingImageModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Url { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class InspectingResult
    {
        [Key]
        public int Id { get; set; }
        public int FormId { get; set; }
        public float Arsen { get; set; }
        public float Plumbum { get; set; }
        public float Cadmi { get; set; }
        public float Hydrargyrum { get; set; }
        public float Salmonella { get; set; }
        public float Coliforms { get; set; }
        public float Ecoli { get; set; }
        public float Glyphosate_Glufosinate { get; set; }
        public float SulfurDioxide { get; set; }
        public float MethylBromide { get; set; }
        public float HydrogenPhosphide { get; set; }
        public float Dithiocarbamate { get; set; }
        public float Nitrat { get; set; }
        public float NaNO3_KNO3 { get; set; }
        public float Chlorate { get; set; }
        public float Perchlorate { get; set; }
        public string EvaluatedResult { get; set; }

        public InspectingForm InspectingForm { get; set; }
        public ICollection<InspectingImage> InspectingImages { get; set; }
    }
}

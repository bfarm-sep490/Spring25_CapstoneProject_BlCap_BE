using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public string IssueName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public Problem Problem { get; set; }
        public ICollection<SampleSolution> SampleSolutions { get; set; }
    }
}

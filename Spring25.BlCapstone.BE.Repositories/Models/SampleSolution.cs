using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class SampleSolution
    {
        [Key]
        public int Id { get; set; }
        public int IssueId { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Issue Issue { get; set; }
    }
}

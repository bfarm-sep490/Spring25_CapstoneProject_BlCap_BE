using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class ImageReport
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
        public int TaskId { get; set; }

        public Task Task { get; set; }
    }
}

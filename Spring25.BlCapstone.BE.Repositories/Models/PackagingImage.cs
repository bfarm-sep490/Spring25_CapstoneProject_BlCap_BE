using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class PackagingImage
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Url { get; set; }

        public PackagingTask PackagingTask { get; set; }
    }
}

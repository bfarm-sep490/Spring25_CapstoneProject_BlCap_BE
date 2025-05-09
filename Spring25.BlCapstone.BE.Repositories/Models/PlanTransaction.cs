﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class PlanTransaction
    {
        [Key]
        [ForeignKey("Plan")]
        public int Id { get; set; }
        public string UrlAddress { get; set; }

        public Plan Plan { get; set; }
    }
}

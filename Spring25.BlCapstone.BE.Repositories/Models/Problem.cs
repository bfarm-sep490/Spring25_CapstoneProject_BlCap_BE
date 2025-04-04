﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class Problem
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int FarmerId { get; set; }
        public string ProblemName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string? ResultContent { get; set; }
        public DateTime CreatedDate { get; set; }

        public Plan Plan { get; set; }
        public Farmer Farmer { get; set; }
        public ICollection<ProblemImage> ProblemImages { get; set; }
        public ICollection<CaringTask> CaringTasks { get; set; }
    }
}

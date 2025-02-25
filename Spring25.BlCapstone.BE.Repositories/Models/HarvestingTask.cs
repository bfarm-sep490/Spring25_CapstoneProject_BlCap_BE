﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class HarvestingTask
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int? FarmerId { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public string Description { get; set; }
        public string? ResultContent { get; set; }
        public DateTime? HarvestDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public float? HarvestedQuantity { get; set; }
        public string? HarvestedUnit { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsAvailable { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Plan Plan { get; set; }
        public Farmer Farmer { get; set; }
        public ICollection<HarvestingImage> HarvestingImages { get; set; }
        public ICollection<HarvestingItem> HarvestingItems { get; set; }
    }
}

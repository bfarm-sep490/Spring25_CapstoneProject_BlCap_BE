﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Models
{
    public class FarmerPermission
    {
        public int FarmerId { get; set; }
        public int PlanId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Farmer Farmer { get; set; }
        public Plan Plan { get; set; }
    }
}

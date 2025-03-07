﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Dashboards
{
    public class AdminData
    {
        [JsonPropertyName("date")]
        public DateTime Date {  get; set; }
        [JsonPropertyName("value")]
        public double Value {  get; set; }
    }
}

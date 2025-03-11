using Newtonsoft.Json;
using Spring25.BlCapstone.BE.Repositories.Dashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.BusinessModels.Dashboard
{
    public class TasksDashboardModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("caring_task")]
        public TasksDashboard CaringTask { get; set; }
        [JsonPropertyName("harvesting_task")]

        public TasksDashboard HarvestingTask { get; set; }
        [JsonPropertyName("packaging_task")]

        public TasksDashboard PackagingTask { get; set; }
       
    }
}

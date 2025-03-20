using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Dashboards
{
    public class StatusTask
    {
        [JsonPropertyName("ongoing_quantity")]
        public int OnGoingQuantity { get; set; }
        [JsonPropertyName("pending_quantity")]
        public int PendingQuantity { get; set; }
        [JsonPropertyName("incomplete_quantity")]
        public int InCompleteQuantity { get; set; }
        [JsonPropertyName("cancel_quantity")]
        public int CancelQuantity {  get; set; }
        [JsonPropertyName("complete_quantity")]
        public int CompleteQuantity {  get; set; }
        [JsonPropertyName("last_create_date")]
        public DateTime LastCreateDate {  get; set; }
    }
    public class StatusDashboard
    {
        [JsonPropertyName("caring_tasks")]
        public StatusTask CaringTasks {get;set;} = new StatusTask();
        [JsonPropertyName("harvesting_tasks")]
        public StatusTask HarvestingTasks { get;set;} = new StatusTask();
        [JsonPropertyName("packaging_tasks")]
        public StatusTask PackagingTasks { get;set;}= new StatusTask();
    }
}

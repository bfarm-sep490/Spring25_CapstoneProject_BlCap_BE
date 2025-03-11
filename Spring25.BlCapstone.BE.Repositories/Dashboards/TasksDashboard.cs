using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Dashboards
{
    public class TasksDashboard
    {
        [JsonPropertyName("total_tasks")]
        public int TotalTasks { get; set; }
        [JsonPropertyName("complete_tasks")]

        public int CompleteTasks { get; set; }
        [JsonPropertyName("cancel_tasks")]

        public int CancelTasks {  get; set; }
        [JsonPropertyName("ongoing_tasks")]

        public int OnGoingTasks {  get; set; }
        [JsonPropertyName("incomplete_tasks")]

        public int IncompleteTasks {  get; set; }
        [JsonPropertyName("pending_tasks")]

        public int PendingTasks { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Spring25.BlCapstone.BE.APIs.RequestModels.CaringTask
{
    public class GetCaringTaskRequest
    {
        public int? plan_id { get; set; }
        public int? farmer_id { get; set; }
        public int? problem_id { get; set; }
        public List<string>? status_list { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Spring25.BlCapstone.BE.APIs.RequestModels.CaringTask
{
    public class TaskReasonReplace
    {
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }
}

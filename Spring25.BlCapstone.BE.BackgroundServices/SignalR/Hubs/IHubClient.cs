using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.BackgroundServices.SignalR.Hubs
{   
    public class MessageInstance {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime dateTime { get; set; }
        public string Url { get; set; }
        public string Image {  get; set; }
        public MessageInstance(string? id, string? title, string? message, string? url, string? image, DateTime? dateTime)
        {   
            this.dateTime=dateTime ?? DateTime.Now;
            this.Id = id;
            this.Title = title;
            this.Message = message;
            this.Url = url;
            this.Image = image;
        }
    }
    public interface IHubClient
    {
        Task BroadcastMessage(MessageInstance msg);
    }
}

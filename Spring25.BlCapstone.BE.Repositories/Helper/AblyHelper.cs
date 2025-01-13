using IO.Ably;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Helper
{
    public class AblyHelper
    {
        private readonly AblyRest _ablyClient;
        private readonly string _channelNotification = "notifications";

        public AblyHelper()
        {
            var ablyConfig = Environment.GetEnvironmentVariable("ABLY_API_KEY");
            if (string.IsNullOrEmpty(ablyConfig))
            {
                throw new Exception("Ably configuration is missing...");
            }

            _ablyClient = new AblyRest(ablyConfig);
        }

        public async Task<string> SendNotificationAsync(string title, string body)
        {
            try
            {
                var channel = _ablyClient.Channels.Get(_channelNotification);
                await channel.PublishAsync("Notification", new
                {
                    title = title,
                    body = body
                });

                return "Push notifications successfully!";
            } 
            catch (AblyException ex)
            {
                return $"Push notifications failed: {ex.Message}";
            }
        }

        public async Task<string> SendMessageWithTopic(string title, string body, string topic)
        {
            try
            {
                var channel = _ablyClient.Channels.Get(topic);
                var message = new Message
                {
                    Name = topic,
                    Data = new
                    {
                        title = title,
                        body = body
                    }
                };
                await channel.PublishAsync(message);

                return "Push notifications successfully!";
            }
            catch (AblyException ex)
            {
                return $"Push notifications failed: {ex.Message}";
            }
        }
    }
}

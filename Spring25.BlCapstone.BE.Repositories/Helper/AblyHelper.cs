using FirebaseAdmin.Messaging;
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
        private readonly string _channelName = "notifications";

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
                var channel = _ablyClient.Channels.Get(_channelName);
                await channel.PublishAsync("notification", new
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
                await channel.PublishAsync(topic, new
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
    }
}

using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using IO.Ably;
using IO.Ably.Push;
using Newtonsoft.Json.Linq;
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
                    Title = title,
                    Body = body
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

        public async Task<string> SendMessageToDevice(string title, string body, string tokenDevice)
        {
            try
            {
                var notification = new JObject
                {
                    ["notification"] = new JObject
                    {
                        { "title", title },
                        { "body", body }
                    }
                };

                var recipient = new JObject
                {
                    { "deviceId", tokenDevice },
                };

                await _ablyClient.Push.Admin.PublishAsync(recipient, notification);
                return "Push notifications successfully!";
            }
            catch (Exception ex)
            {
                return $"Push notifications failed: {ex.Message}";
            }
        }
    }
}

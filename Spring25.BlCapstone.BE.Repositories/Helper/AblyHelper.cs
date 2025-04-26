using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using IO.Ably;
using IO.Ably.Push;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Helper
{
    public static class AblyHelper
    {
        private static readonly AblyRealtime _ablyClient;
        private static readonly RedisManagement _redisManagement;
        private static readonly string _channelNotification = "notifications";

        static AblyHelper()
        {
            var ablyConfig = Environment.GetEnvironmentVariable("ABLY_API_KEY");
            if (string.IsNullOrEmpty(ablyConfig))
            {
                throw new Exception("Ably configuration is missing...");
            }

            _redisManagement = new RedisManagement();
            _ablyClient = new AblyRealtime(ablyConfig);
        }

        public static async Task<string> SendNotificationAsync(string title, string body)
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

        public static async Task<string> SendMessageWithChanel(string title, string body, string chanel)
        {
            try
            {
                var channel = _ablyClient.Channels.Get(chanel);
                var message = new Message
                {
                    Name = chanel,
                    Data = new
                    {
                        Title = title,
                        Body = body
                    }
                };
                await channel.PublishAsync("Notification", message);

                return "Push notifications successfully!";
            }
            catch (AblyException ex)
            {
                return $"Push notifications failed: {ex.Message}";
            }
        }

        public static async Task<string> RegisterTokenDevice(string deviceToken, string role)
        {
            try
            {
                JObject recipient = new JObject(
                    new JProperty("transportType", "fcm"),
                    new JProperty("registrationToken", deviceToken)
                );

                string id = GenerateRandomId(role);
                var deviceDetails = new DeviceDetails()
                {
                    Id = id,
                    Platform = "android",
                    FormFactor = "phone",
                    Metadata = new JObject(),
                    Push = new DeviceDetails.PushData()
                    {
                        Recipient = recipient,
                        ErrorReason = null
                    }
                };

                var devcieRegistration = await _ablyClient.Push.Admin.DeviceRegistrations.SaveAsync(deviceDetails);
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GenerateRandomId(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException("Role cannot be null or empty", nameof(role));
            }

            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string randomPart = new Random().Next(1000, 9999).ToString();

            return $"{role.ToUpper()}-{timestamp}-{randomPart}";
        }

        private static string GetDeviceTokensByFarmerId(int id)
        {
            var key = $"farmer-{id}";
            try
            {
                if (_redisManagement.IsConnected == false) return null;
                string productListJson = _redisManagement.GetData(key);
                if (productListJson == null || productListJson == "[]")
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<DeviceTokenModel>(productListJson);
                return result.Token;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<string> SendMessageToDevice(string title, string body, int farmerId, object? data = null)
        {
            try
            {
                string deviceId = GetDeviceTokensByFarmerId(farmerId);
                JObject payload = new JObject
                {
                    ["notification"] = new JObject
                    {
                        { "title", title },
                        { "body", body }
                    }
                };

                JObject recipient = new JObject
                {
                    { "deviceId", deviceId }
                };

                await _ablyClient.Push.Admin.PublishAsync(recipient, payload);
                return "Push notifications successfully!";
            }
            catch (Exception ex)
            {
                return $"Push notifications failed: {ex.Message}";
            }
        }

        public static async Task<string> RemoveTokenDevice(string deviceId)
        {
            try
            {
                await _ablyClient.Push.Admin.DeviceRegistrations.RemoveAsync(deviceId);

                return "Remove token device successfully !";
            }
            catch (Exception ex)
            {
                return $"Remove failed: {ex.Message}";
            }
        }
    }
}

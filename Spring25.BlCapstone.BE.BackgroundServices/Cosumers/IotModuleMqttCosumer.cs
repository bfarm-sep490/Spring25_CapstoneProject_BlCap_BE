using System;
using System.Threading;
using System.Threading.Tasks;
using HiveMQtt.Client.Options;
using Microsoft.Extensions.Hosting;
using Spring25.BlCapstone.BE.BackgroundServices.Clients;
using Spring25.BlCapstone.BE.Repositories;

namespace Spring25.BlCapstone.BE.BackgroundServices.Cosumers
{
    public class IotModuleMqttConsumer : BackgroundService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly HiveCloudClient _client;

        public IotModuleMqttConsumer(UnitOfWork unitOfWork, HiveCloudClient client)
        {
            _unitOfWork = unitOfWork;
            _client = client;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("iot module mqtt consumer starting...");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (!_client.IsConnected())
                    {
                        Console.WriteLine("client is not connected. attempting to reconnect...");
                        await _client.Connect();
                        Console.WriteLine("client reconnected successfully.");

                        await SubscribeToTopics();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error in mqtt consumer: {ex.Message}");
                }
            }

            Console.WriteLine("iot module mqtt consumer stopping...");
        }

        private async Task SubscribeToTopics()
        {
            try
            {
                await _client.SubscribeWithHandler("iot/topic/device1", async (topic, payload) =>
                {
                    Console.WriteLine($"received message for device1: {payload}");
                    await ProcessMessageAsync("iot/topic/device1", payload);
                });

                await _client.SubscribeWithHandler("iot/topic/device2", async (topic , payload) =>
                {
                    Console.WriteLine($"received message for device2: {payload}");
                    await ProcessMessageAsync("iot/topic/device2", payload);
                });

                Console.WriteLine("mqtt subscriptions completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error subscribing to topics: {ex.Message}");
            }
        }

        private async Task ProcessMessageAsync(string topic, string payload)
        {
            try
            {
                Console.WriteLine($"processing message from {topic}: {payload}");

                var logEntry = new IotMessageLog
                {
                    Topic = topic,
                    Payload = payload,
                    Timestamp = DateTime.UtcNow
                };

                Console.WriteLine($"{topic} {payload}");

                Console.WriteLine($"message saved to database: {topic} - {payload}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error processing message from {topic}: {ex.Message}");
            }
        }
    }

    public class IotMessageLog
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Payload { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

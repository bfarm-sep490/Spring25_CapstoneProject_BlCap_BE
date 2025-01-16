using HiveMQtt.Client;
using HiveMQtt.Client.Options;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.BackgroundServices.Clients
{
    public class HiveCloudClient
    {
        private readonly string _host = "broker.hivemq.com";
        private readonly int _port = 1883;
        private readonly HiveMQClient _client;

        private readonly ConcurrentDictionary<string, Func<string,string, Task>> _topicHandlers = new();

        public HiveCloudClient()
        {
            var options = new HiveMQClientOptions
            {
                Host = _host,
                Port = _port
            };

            _client = new HiveMQClient(options);

            _client.OnMessageReceived += async (sender, args) =>
            {
                string topic = args.PublishMessage.Topic;
                string payload = Encoding.UTF8.GetString(args.PublishMessage.Payload);

                if (_topicHandlers.TryGetValue(topic, out var handler))
                {
                    try
                    {
                        await handler(topic,payload); 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"error handling message for topic '{topic}': {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"unhandled topic: {topic} - message: {payload}");
                }
            };
        }

        public async Task Connect()
        {
            try
            {
                await _client.ConnectAsync();
                Console.WriteLine("connected to hivemq broker.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error connecting to broker: {ex.Message}");
            }
        }

        public async Task Disconnect()
        {
            try
            {
                await _client.DisconnectAsync();
                Console.WriteLine("disconnected from hivemq broker.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error disconnecting from broker: {ex.Message}");
            }
        }

        public async Task SubscribeWithHandler(string topic, Func<string,string, Task> handler)
        {
            try
            {
                await _client.SubscribeAsync(topic);
                _topicHandlers[topic] = handler;
                Console.WriteLine($"subscribed to topic: {topic}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error subscribing to topic '{topic}': {ex.Message}");
            }
        }

        public async Task PublishToTopic(string topic, string payload)
        {
            try
            {
                await _client.PublishAsync(topic, payload).ConfigureAwait(false);
                Console.WriteLine($"message published to topic: {topic} - payload: {payload}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error publishing to topic '{topic}': {ex.Message}");
            }
        }

        public bool IsConnected()
        {
            try
            {
                return _client.IsConnected();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error checking connection status: {ex.Message}");
                return false;
            }
        }
    }
}

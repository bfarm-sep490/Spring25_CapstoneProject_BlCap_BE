using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Redis
{
    public class RedisManagement
    {
        private readonly StackExchange.Redis.IDatabase _cache;
        private readonly ConnectionMultiplexer _redis;
        public bool IsConnected = false;
        public RedisManagement()
        {
            try
            {
                string redisUrl = GetEnvironmentVariable("REDIS_URL");
                string redisPort = GetEnvironmentVariable("REDIS_PORT");
                string redisUser = GetEnvironmentVariable("REDIS_USER");
                string redisPassword = GetEnvironmentVariable("REDIS_PASSWORD");

                string connectionString = $"{redisUser}:{redisPassword}@{redisUrl}:{redisPort}";

                _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    EndPoints = { $"{redisUrl}:{redisPort}" },
                    User = redisUser,
                    Password = redisPassword,
                  
                    AbortOnConnectFail = false
                });       
                _cache = _redis.GetDatabase();
                IsConnected = true;
            }
            catch (Exception ex) {
                _redis = null;
                IsConnected = false;
            }
       
        }

        public RedisManagement(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _cache = _redis.GetDatabase();
        }

        public void SetData(string key, string value)
        {
            _cache.StringSet(key, value, TimeSpan.FromDays(2));
        }

        public string GetData(string key)
        {
            return _cache.StringGet(key);
        }

        public void DeleteData(string key)
        {
            _cache.KeyDelete(key);
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }
        public async Task AddProductToListAsync(string key, object product)
        {
            var products = await GetProductsFromListAsync(key);
            products.Add(product);
            var productJson = JsonConvert.SerializeObject(products);
            await _cache.StringSetAsync(key, productJson);
        }

        public async Task<List<object>> GetProductsFromListAsync(string key)
        {
            var productList = new List<object>();
            var productJson = await _cache.StringGetAsync(key);

            if (!string.IsNullOrEmpty(productJson))
            {
                productList = JsonConvert.DeserializeObject<List<object>>(productJson);
            }
            return productList;
        }

        public async Task PublishAsync(string channel, string message)
        {
            var sub = _redis.GetSubscriber();
            await sub.PublishAsync(channel, message);
        }

        private string GetEnvironmentVariable(string key)
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"'{key}' configuartion is missing...");
            }
            return value;
        }
    }
}

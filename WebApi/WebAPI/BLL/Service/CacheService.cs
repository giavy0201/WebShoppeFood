using BLL.IService;
using BLL.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BLL.Service
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        public static List<string> keys { get; set; }
        public CacheService(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
            keys = new List<string>();
        }
        public async Task<T> GetCachedData<T>(string key)
        {
            try
            {
                var jsonData = _cache.GetString(key);
                if (jsonData == null)
                    return default(T);
                return JsonSerializer.Deserialize<T>(jsonData);
            }
            catch (Exception ec)
            {
                throw ec;
            }
        }
        public async Task SetCachedData<T>(string key, T data)
        {
            try
            {
                var random = new Random();
                var randomSeconds = random.Next(1, 60);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(ValueTime.LongTime + randomSeconds)
                };
                var options_json = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };
                var jsonData = JsonSerializer.Serialize(data, options_json);
                _cache.SetString(key, jsonData, options);
                if (!keys.Contains(key))
                {
                    keys.Add(key);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetCachedData<T>(string key, T data, int timeSeconds)
        {
            try
            {
                var random = new Random();
                var randomSeconds = random.Next(1, 60);
                var timeCache = timeSeconds + randomSeconds;
                var options = new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(timeCache),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1800 + randomSeconds)
                };
                var options_json = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };
                var jsonData = JsonSerializer.Serialize(data, options_json);
                _cache.SetString(key, jsonData, options);
                if (!keys.Contains(key))
                {
                    keys.Add(key);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveCache(string key)
        {
            try
            {
                _cache.Remove(key);
                keys.Remove(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

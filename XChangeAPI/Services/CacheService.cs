using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;
using XChangeAPI.Services.Interfaces;

namespace XChangeAPI.Services
{
    public class CacheService : ICacheService
    {
        IDistributedCache _cacheDB;
        public CacheService(IDistributedCache cacheDB)
        {
            _cacheDB = cacheDB;
        }

        public async Task<T> GetData<T>(string key)
        {
            var value = await _cacheDB.GetStringAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public async Task SetData<T>(string key, T value, DateTimeOffset expiration)
        {
            var expiryTime = expiration.DateTime.Subtract(DateTime.Now);
            await _cacheDB.SetStringAsync(key, JsonSerializer.Serialize(value), new DistributedCacheEntryOptions() { AbsoluteExpiration = expiration });
        }

        public async Task RemoveData(string key)
        {
            await _cacheDB.RemoveAsync(key);
        }

    }
}

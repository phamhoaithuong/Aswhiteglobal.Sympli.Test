using Microsoft.Extensions.Caching.Distributed;
using Sympli.Core.Interfaces;
using System.Text.Json;

namespace Sympli.Infrastructure.Services
{
    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;

        public CacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetDataAsync<T>(string key, CancellationToken token = default)
        {
            var value = await _cache.GetStringAsync(key, token);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default;
        }

        public async Task SetDataAsync<T>(string key, T value, int minutes, CancellationToken token = default)
        {
            var options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(minutes));

            var data = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, data, options, token);
        }
    }
}

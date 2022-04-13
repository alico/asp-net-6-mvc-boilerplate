using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Service
{
    public class CacheService : ICacheService
    {
        public readonly IMemoryCache _cache;
        public readonly IDistributedCache _distributedCache;

        public CacheService(IMemoryCache cache, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _cache = cache;
        }

        public async Task<T> Get<T>(string cacheKey, TimeSpan memCacheExpireDate, Task<T> func, bool purge = false) where T : class
        {
            var value = _cache.Get<T>(cacheKey);

            if (value is null || purge)
            {
                value = await func;
                using (var entry = _cache.CreateEntry(cacheKey))
                {
                    entry.Value = value;
                    entry.AbsoluteExpirationRelativeToNow = memCacheExpireDate;
                    entry.SlidingExpiration = null;
                }
            }

            return value;
        }

        public async Task<T> Get<T>(string cacheKey, TimeSpan distributedCacheExpireDate, TimeSpan memCacheExpireDate, Task<T> func, bool purge = false) where T : class
        {
            var value = _cache.Get<T>(cacheKey);
            var expireDate = DateTime.Now.Add(distributedCacheExpireDate);

            if (value is null || purge)
            {
                value = await _distributedCache.GetAsync<T>(cacheKey);
                if (value is null || purge)
                {
                    value = await func;
                    await _distributedCache.SetAsync(cacheKey, value, expireDate);
                }

                using (var entry = _cache.CreateEntry(cacheKey))
                {
                    entry.Value = value;
                    entry.AbsoluteExpirationRelativeToNow = memCacheExpireDate;
                    entry.SlidingExpiration = null;
                }
            }

            return value;
        }
    }
}

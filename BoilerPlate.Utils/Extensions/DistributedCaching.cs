using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BoilerPlate.Utils
{
    public static class DistributedCaching
    {
        public static async Task SetAsync<T>(this IDistributedCache distributedCache, string key, T value, DateTime absoluteExpiration, TimeSpan? slidingExpiration = null, CancellationToken token = default(CancellationToken))
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = slidingExpiration
            };

            await distributedCache.SetAsync(key, value.ToByteArray(), cacheOptions, token);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken token = default(CancellationToken)) where T : class
        {
            var result = await distributedCache.GetAsync(key, token);
            return result.FromByteArray<T>();
        }
    }
}

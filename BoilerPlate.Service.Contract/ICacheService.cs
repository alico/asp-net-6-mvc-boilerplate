using System;
using System.Threading.Tasks;

namespace BoilerPlate.Service.Contract
{
    public interface ICacheService
    {
        Task<T> Get<T>(string cacheKey, TimeSpan memCacheExpireDate, Task<T> func, bool purge = false) where T : class;
        Task<T> Get<T>(string cacheKey, TimeSpan distributedCacheExpireDate, TimeSpan memCacheExpireDate, Task<T> func, bool purge = false) where T : class;
    }
}

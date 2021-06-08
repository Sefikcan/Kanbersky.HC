using Kanbersky.HC.Core.Caching.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kanbersky.HC.Core.Caching.Concrete.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="distributedCache"></param>
        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="duration"></param>
        public void Add(string key, object data, double duration)
        {
            var serializedItems = JsonSerializer.Serialize(data);
            var byteItems = Encoding.UTF8.GetBytes(serializedItems);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(duration), //Cache süresi
                //AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5), // Redis’de ilgili keye ait cache’in mutlaka yani işlem yapılsın ya da yapılmasın 5sn sonunda düşeceğinin belirlenmesidir.
                //SlidingExpiration = TimeSpan.FromSeconds(10) //Redis’de ilgili key ile belli bir zaman mesela bu örnekte 5sn içinde hiçbir işlem yapılmaz ise, cache’in düşeceğinin belirlenmesidir.
            };

            _distributedCache.Set(key, byteItems, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public async Task<T> AddAsync<T>(string key, T data, double duration)
        {
            var serializedItems = JsonSerializer.Serialize(data);
            var byteItems = Encoding.UTF8.GetBytes(serializedItems);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(duration), //Cache süresi
                //AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5), // Redis’de ilgili keye ait cache’in mutlaka yani işlem yapılsın ya da yapılmasın 5sn sonunda düşeceğinin belirlenmesidir.
                //SlidingExpiration = TimeSpan.FromSeconds(10) //Redis’de ilgili key ile belli bir zaman mesela bu örnekte 5sn içinde hiçbir işlem yapılmaz ise, cache’in düşeceğinin belirlenmesidir.
            };

            await _distributedCache.SetAsync(key, byteItems, options);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            var getCacheResponse = _distributedCache.Get(key);
            if (getCacheResponse != null)
            {
                var objectString = Encoding.UTF8.GetString(getCacheResponse);
                return JsonSerializer.Deserialize<T>(objectString);
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            var getCacheResponse = _distributedCache.Get(key);
            if (getCacheResponse != null)
            {
                var objectString = Encoding.UTF8.GetString(getCacheResponse);
                return JsonSerializer.Deserialize<object>(objectString);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            var getCacheResponse = await _distributedCache.GetAsync(key);
            if (getCacheResponse != null)
            {
                var objectString = Encoding.UTF8.GetString(getCacheResponse);
                return JsonSerializer.Deserialize<T>(objectString);
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<object> GetAsync(string key)
        {
            var getCacheResponse = await _distributedCache.GetAsync(key);
            if (getCacheResponse != null)
            {
                var objectString = Encoding.UTF8.GetString(getCacheResponse);
                return JsonSerializer.Deserialize<object>(objectString);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExists(string key)
        {
            return _distributedCache.Get(key) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> IsExistsAsync(string key)
        {
            return await _distributedCache.GetAsync(key) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }
}

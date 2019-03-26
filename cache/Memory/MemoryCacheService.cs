using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace cache.Memory
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCacheService : ICacheService
    {

        public bool Exists(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        public bool Set(string key, object cachedObject, int? secondsTimeOut)
        {
            if (this.Exists(key))
            {
                this.Remove(key);
            }

            return MemoryCache.Default.Add(key, cachedObject, DateTime.Now.AddSeconds(secondsTimeOut.Value));
        }

        public object Get(string key)
        {
            return MemoryCache.Default.Get(key);
        }

        public T Get<T>(string key)
        {
            return (T)MemoryCache.Default.Get(key);
        }

        public bool Remove(string key)
        {
            MemoryCache.Default.Remove(key);

            return true;
        }

        public bool RemoveStartWithKey(string prefixKey)
        {
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                if (cacheKey.ToString().StartsWith(prefixKey))
                    MemoryCache.Default.Remove(cacheKey);
            }

            return true;
        }

        public bool RemoveAll()
        {
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                MemoryCache.Default.Remove(cacheKey);
            }

            return true;
        }
    }
}

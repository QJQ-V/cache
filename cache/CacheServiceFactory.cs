using cache.Memory;
using cache.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cache
{
    public class CacheServiceFactory
    {
        public static ICacheService CreateCacheService(string cacheType)
        {
            if (String.IsNullOrEmpty(cacheType))
            {
                return new MemoryCacheService();
            }

            switch (cacheType)
            {
                case "redis":
                    return new RedisCacheService();
                case "default":
                    return new MemoryCacheService();
                default:
                    return new MemoryCacheService();
            }
        }
    }
}

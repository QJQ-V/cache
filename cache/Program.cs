using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cache
{
    class Program
    {
        static void Main(string[] args)
        {
            var cacheService = CacheServiceFactory.CreateCacheService("memory");

            string key = "test_1";
            cacheService.Exists(key);
            cacheService.Get(key);
        }
    }
}

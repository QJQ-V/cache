using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cache
{
    public interface ICacheService
    {
        /// <summary>
        /// 判断缓存Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 设置缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheObject"></param>
        /// <param name="secondsTimeout"></param>
        /// <returns></returns>
        bool Set(string key, object cacheObject, int? secondsTimeout);

        /// <summary>
        /// 根据KEY获取缓存并转换为泛型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 根据KEY获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 根据KEY删除缓存对象
        /// </summary>
        /// <param name="key"></param>
        bool Remove(string key);

        /// <summary>
        /// 根据KEY删除带前缀的缓存对象
        /// </summary>
        /// <param name="prefixKey"></param>
        bool RemoveStartWithKey(string prefixKey);

        /// <summary>
        /// 删除所有缓存对象
        /// </summary>
        bool RemoveAll();
    }
}

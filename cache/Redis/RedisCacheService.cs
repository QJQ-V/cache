using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cache.Redis
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        private ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(connectionString);
        private IDatabase _db;
        private const string connectionString = "";

        public RedisCacheService()
        {
            this._db = _redis.GetDatabase();
        }

        public bool Exists(string key)
        {
            return _db.KeyExists(key);
        }

        public bool Set(string key, object cachedObject, int? secondsTimeOut)
        {
            TimeSpan? tp = null;
            if (secondsTimeOut != null)
            {
                tp = TimeSpan.FromSeconds(secondsTimeOut.Value);
            }

            string data = null;

            if (cachedObject != null)
            {
                if (cachedObject.GetType() == typeof(string) || cachedObject.GetType().IsValueType)
                {
                    data = cachedObject.ToString();
                }
                else {
                    data = Newtonsoft.Json.JsonConvert.SerializeObject(cachedObject);
                }
            }

            return _db.StringSet(key, data, tp);
        }

        public T Get<T>(string key)
        {
            var value = this._db.StringGet(key);
            if (value.IsNull)
            {
                return default;
            }

            if (typeof(T) == typeof(string) || typeof(T).IsValueType)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
            }
        }

        public object Get(string key)
        {
           return this._db.StringGet(key);
        }

        public bool Remove(string key)
        {
            return _db.KeyDelete(key);
        }
        public bool RemoveStartWithKey(string prefixKey)
        {
            var listServer = this._redis.GetEndPoints().ToList();
            List<RedisKey> listKeys = new List<RedisKey>();
            listServer.ForEach(r =>
            {
                var server = this._redis.GetServer(r);
                var keys = server.Keys(this._db.Database, prefixKey);
                listKeys.AddRange(keys);
            });

            listKeys.ForEach(o => this._db.KeyDelete(o));

            return true;
        }

        public bool RemoveAll()
        {
            var listServer = this._redis.GetEndPoints().ToList();
            List<RedisKey> listKeys = new List<RedisKey>();
            listServer.ForEach(o =>
            {
                var server = this._redis.GetServer(o);
                server.FlushDatabase(this._db.Database);
            });

            return true;
        }
    }
}

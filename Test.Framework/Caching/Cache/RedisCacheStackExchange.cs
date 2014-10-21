using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.Configuration;
using Test.Framework.Extensibility;
using StackExchange.Redis;

namespace Test.Framework.Caching
{
    public class RedisCacheStackExchange : ICache
    {
        private StackExchange.Redis.IDatabase Database;
        private string connectionName;
        public IWebConfiguration Configuration { get; set; }
        public RedisCacheStackExchange()
        {
            if (Configuration == null)
            {
                this.Configuration = Container.Resolve<IWebConfiguration>();
            }
            var redisConfig = this.Configuration.GetSection<RedisConfigSection>("redis");
            if (redisConfig == null) return;
            var cacheStrings = redisConfig.GetCacheStrings();
            var cacheString = cacheStrings.FirstOrDefault();
            this.connectionName = cacheString.Name;
            DefaultRedisActivator.InitializeStackExchange();
            this.Database = Container.Resolve<StackExchange.Redis.IDatabase>();
        }
        public int Count
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public List<string> CachedKeys
        {
            get { throw new NotImplementedException(); }
        }

        public void Set<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public bool TryGet<T>(string key, out T value)
        {
            throw new NotImplementedException();
        }
    }
}

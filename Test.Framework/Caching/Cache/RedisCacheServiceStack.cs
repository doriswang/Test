using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using Test.Framework.Extensibility;

namespace Test.Framework.Caching
{
    public sealed class RedisCacheServiceStack : ICache
    {
        public RedisCacheServiceStack()
        {
            DefaultRedisActivator.InitializeServiceStack();
        }

        public int Count
        {
            get 
            {
                return 0;
            }
        }

        public T Get<T>(string key)
        {
            using (var client = Container.Resolve<IRedisClientsManager>("Test").GetClient())
            {
                return client.Get<T>(key);
            }
        }

        public void Remove(string key)
        {
            using (var client = Container.Resolve<IRedisClientsManager>("Test").GetClient())
            {
                client.Remove(key);
            }
        }

        public bool Contains(string key)
        {
            using (var client = Container.Resolve<IRedisClientsManager>("Test").GetClient())
            {
                return client.ContainsKey(key);
            }
        }

        public List<string> CachedKeys
        {
            get
            {
                using (var client = Container.Resolve<IRedisClientsManager>("Test").GetClient())
                {
                    return client.GetAllKeys();
                }
            }
        }

        public void Set<T>(string key, T value)
        {
            using (var client = Container.Resolve<IRedisClientsManager>("Test").GetClient())
            {
                client.Add<T>(key, value);
            }
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            using (var client = Container.Resolve<IRedisClientsManager>("Test").GetClient())
            {
                client.Add<T>(key, value, absoluteExpiration);
            }
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            using (var client = Container.Resolve<IRedisClientsManager>("Test").GetClient())
            {
                client.Add<T>(key, value, slidingExpiration);
            }
        }

        public bool TryGet<T>(string key, out T value)
        {
            using (var client = Container.Resolve<IRedisClientsManager>("Test").GetClient())
            {
                value = default(T);
                object cached = new object();
                if (!Contains(key)) return false;
                value = Get<T>(key);
                return true;
            }
        }
    }
}

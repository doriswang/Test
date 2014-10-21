using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.Extensibility;
using ServiceStack.Caching;
using ServiceStack.Redis;

namespace Test.Framework.Caching
{
    public static class CacheRegister
    {
        public static void Register(IList<string> connectionNames, CacheType cacheType)
        {
            switch (cacheType)
            {
                case CacheType.InProcess:
                    InProcessRegister(connectionNames);
                    break;
                case CacheType.Redis:
                    RedisRegister(connectionNames);
                    break;
                case CacheType.Memcache:
                    MemcacheRegister(connectionNames);
                    break;
                default:
                    InProcessRegister(connectionNames);
                    break;
            }
        }

        public static void InProcessRegister(IList<string> connectionNames)
        { 
            
        }

        public static void RedisRegister(IList<string> connectionNames)
        {
            foreach (var connectionName in connectionNames)
            {
                Container.RegisterInstance<IRedisClientsManager, BasicRedisClientManager>(connectionName, new BasicRedisClientManager(connectionName), ObjectLifeSpans.Transient);
            }
        }

        public static void MemcacheRegister(IList<string> connectionNames)
        { 
            
        }
    }
}

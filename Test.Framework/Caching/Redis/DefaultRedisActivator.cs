using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using Test.Framework.Configuration;
using Test.Framework.Extensibility;

namespace Test.Framework.Caching
{
    public static class DefaultRedisActivator
    {
        public static void InitializeServiceStack()
        {
            IWebConfiguration configuration = Container.Resolve<IWebConfiguration>();
            IRedisClientFactory factory = Container.Resolve<IRedisClientFactory>();
            if (factory != null)
            {
                var redisConfig = configuration.GetSection<RedisConfigSection>("redis");
                if (redisConfig == null) return;
                var cacheStrings = redisConfig.GetCacheStrings();
                foreach (var cacheString in cacheStrings)
                {
                    factory.CreateBasicClient(cacheString.Name, cacheString.Server, cacheString.Port, cacheString.Password);
                }
            }
        }

        public static void InitializeStackExchange()
        {
            IWebConfiguration configuration = Container.Resolve<IWebConfiguration>();
            IRedisClientFactory factory = Container.Resolve<IRedisClientFactory>();
            if (factory != null)
            {
                var redisConfig = configuration.GetSection<RedisConfigSection>("redis");
                if (redisConfig == null) return;
                var cacheStrings = redisConfig.GetCacheStrings();
                foreach (var cacheString in cacheStrings)
                {
                    factory.CreateClient(cacheString.Name, cacheString.Server, cacheString.Port, cacheString.DbValue, cacheString.Password);
                }
            } 
        }
    }
}

using Test.Framework.Caching;
using Test.Framework.Configuration;
using Test.Framework.Extensibility;

namespace Test.Framework
{
    public static class Framework
    {
        public static void Initialize()
        {
            Container.InitializeWith(new LightInjectTypeResolver());
            Container.Register<IWebConfiguration, WebConfiguration>(ObjectLifeSpans.Singleton);
            Container.Register<IRedisClientFactory, RedisClientFactory>(ObjectLifeSpans.Singleton);
            Container.Register<ICache, RedisCacheServiceStack>(ObjectLifeSpans.Singleton);
        }
    }
}

using Couchbase.Configuration;
using Enyim.Caching;

namespace Test.Framework.Caching
{
    public interface ICouchbaseClientFactory
    {
        IMemcachedClient Create(ICouchbaseClientConfiguration config);
    }
}

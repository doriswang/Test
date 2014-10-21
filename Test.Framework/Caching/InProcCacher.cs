using System;
using System.Collections.Generic;
using Test.Framework.Extensibility;
using Test.Framework.Validation;

namespace Test.Framework.Caching
{
    public static class InProcCacher
    {
        private static ICache Cache
        {
            get
            {
                return Container.Resolve<ICache>("HttpRuntimeCache");
            }
        }

        public static bool Contains(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return Cache.Contains(key);
        }

        public static bool TryGet<T>(string key, out T value)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return Cache.TryGet<T>(key, out value);
        }

        public static T Get<T>(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return Cache.Get<T>(key);
        }

        public static void Set<T>(string key, T value)
        {
            Check.Argument.IsNotEmpty(key, "key");

            RemoveIfExists(key);

            Cache.Set(key, value);
        }

        public static void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            Check.Argument.IsNotEmpty(key, "key");
            Check.Argument.IsNotInPast(absoluteExpiration, "absoluteExpiration");

            RemoveIfExists(key);

            Cache.Set(key, value, absoluteExpiration);
        }

        public static void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            Check.Argument.IsNotEmpty(key, "key");
            Check.Argument.IsNotNegativeOrZero(slidingExpiration, "slidingExpiration");

            RemoveIfExists(key);

            Cache.Set(key, value, slidingExpiration);
        }

        public static void Remove(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            Cache.Remove(key);
        }

        internal static void RemoveIfExists(string key)
        {
            if (Cache.Contains(key))
            {
                Cache.Remove(key);
            }
        }

        public static List<string> GetKeys()
        {
            return Cache.CachedKeys;
        }
    }
}

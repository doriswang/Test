using System;
using System.Collections.Generic;
using Test.Framework.Extensibility;
using Test.Framework.Validation;

namespace Test.Framework.Caching
{
    public static class OutProcCacher
    {
        private static ICache InternalCache
        {
            get
            {
                return Container.Resolve<ICache>();
            }
        }

        public static bool Contains(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return InternalCache.Contains(key);
        }

        public static bool TryGet<T>(string key, out T value)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return InternalCache.TryGet<T>(key, out value);
        }

        public static T Get<T>(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return InternalCache.Get<T>(key);
        }

        public static void Set<T>(string key, T value)
        {
            Check.Argument.IsNotEmpty(key, "key");

            RemoveIfExists(key);

            InternalCache.Set(key, value);
        }

        public static void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            Check.Argument.IsNotEmpty(key, "key");
            Check.Argument.IsNotInPast(absoluteExpiration, "absoluteExpiration");

            RemoveIfExists(key);

            InternalCache.Set(key, value, absoluteExpiration);
        }

        public static void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            Check.Argument.IsNotEmpty(key, "key");
            Check.Argument.IsNotNegativeOrZero(slidingExpiration, "slidingExpiration");

            RemoveIfExists(key);

            InternalCache.Set(key, value, slidingExpiration);
        }

        public static void Remove(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            InternalCache.Remove(key);
        }

        internal static void RemoveIfExists(string key)
        {
            if (InternalCache.Contains(key))
            {
                InternalCache.Remove(key);
            }
        }

        public static List<string> GetKeys()
        {
            return InternalCache.CachedKeys;
        }
    }
}

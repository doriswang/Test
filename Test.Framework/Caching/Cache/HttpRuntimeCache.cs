﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Test.Framework.Configuration;

namespace Test.Framework.Caching
{
    public sealed class HttpRuntimeCache : ICache
    {
        #region Private Members
        private static Cache cache; 
        #endregion

        static HttpRuntimeCache()
        {
            cache = HttpRuntime.Cache;
        }

        public int Count
        {
            get { return cache.Count; }
        }

        public T Get<T>(string key)
        {
            T result = default(T);

            if (Contains(key))
            {
                object cached = cache.Get(key);
                if (cached != null)
                {
                    result = (T)cached;
                }
            }
            return result;
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public bool Contains(string key)
        {
            return cache[key] != null;
        }

        public List<string> CachedKeys
        {
            get
            {
                var list = new List<string>();
                IDictionaryEnumerator enumerator = cache.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Key.ToString());
                }
                return list;
            }
        }

        public void Set<T>(string key, T value)
        {
            cache.Insert(key, value, null, DateTime.Now.AddMinutes(FrameworkSettings.CachingIntervalInMinutes), Cache.NoSlidingExpiration);
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            cache.Insert(key, value, null, DateTime.Now.AddMinutes(FrameworkSettings.CachingIntervalInMinutes), slidingExpiration);
        }

        public bool TryGet<T>(string key, out T value)
        {
            value = default(T);

            if (Contains(key))
            {
                object cached = cache.Get(key);

                if (cached != null)
                {
                    value = (T)cached;

                    return true;
                }
            }
            return false;
        }
    }
}

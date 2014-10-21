using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.Caching;

namespace Test.Framework.Extensibility
{
    public class PerCachedLifetime : ILifetime
    {
        HttpRuntimeCache cache = new HttpRuntimeCache();

        public object GetInstance(Func<object> createInstance, Scope currentScope)
        {
            var instanceName = createInstance().GetType().FullName;
            string instanceKey = GetCacheKey(instanceName);
            return TryGetInstance(instanceKey, createInstance);
        }

        private object TryGetInstance(string instanceKey, Func<object> createInstance)
        {
            try
            {
                object instance = createInstance();

                if (!cache.CachedKeys.Contains(instanceKey))
                    SetInstance(instanceKey, createInstance);

                if (!cache.TryGet<object>(instanceKey, out instance))
                    instance = createInstance();

                return instance;
            }
            catch (Exception ex)
            {
                Tracing.Start("Extensibility - Light Inject - PerCachedLifeTime - Start of Error");
                Tracing.Error(ex.Message);
                Tracing.Error(ex.StackTrace);
                Tracing.Stop("Extensibility - Light Inject - PerCachedLifeTime - End of Error");
                return createInstance();
            }
        }

        private void SetInstance(string instanceKey, Func<object> createInstance)
        {
            cache.Set<object>(instanceKey, createInstance());
        }

        public string GetCacheKey(string instanceName)
        {
            return string.Format(CONSTANTS.ExtensibilityKey, instanceName, "CACHE");
        }
    }
}

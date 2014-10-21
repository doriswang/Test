using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.Session;

namespace Test.Framework.Extensibility
{
    public class PerSessionLifetime : ILifetime
    {
        ISessionStore session = new HttpContextSessionStore();

        public object GetInstance(Func<object> createInstance, Scope scope)
        {
            var instanceName = createInstance().GetType().FullName;
            string instanceKey = GetSessionKey(instanceName);
            return TryGetInstance(instanceKey, createInstance);
        }
        private object TryGetInstance(string instanceKey, Func<object> createInstance)
        {
            try
            {
                object instance = createInstance();

                if (!session.Contains(instanceKey))
                    SetInstance(instanceKey, createInstance);

                var sessionInstance = session.Get<object>(instanceKey);

                if (sessionInstance != null)
                    instance = sessionInstance;

                return instance;
            }
            catch (Exception ex)
            {
                Tracing.Start("Extensibility - Light Inject - PerSessionLifeTime - Start of Error");
                Tracing.Error(ex.Message);
                Tracing.Error(ex.StackTrace);
                Tracing.Stop("Extensibility - Light Inject - PerSessionLifeTime - End of Error");
                return createInstance();
            }
        }

        private void SetInstance(string instanceKey, Func<object> createInstance)
        {
            session.Add<object>(instanceKey, createInstance());
        }

        public string GetSessionKey(string instanceName)
        {
            return string.Format(CONSTANTS.ExtensibilityKey, instanceName, "SESSION");
        }
    }
}

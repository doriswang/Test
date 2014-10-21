using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Test.Framework.Extensibility
{
    public class CustomWebApiDependencyResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.ResolveAll<object>(serviceType);
        }

        public void Dispose()
        {
        }
    }
}

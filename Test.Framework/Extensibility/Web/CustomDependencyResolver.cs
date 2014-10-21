using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Test.Framework.Extensibility
{
    public class CustomDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            object instance = Container.Resolve(serviceType);
            if (instance == null && !serviceType.IsAbstract)
            {
                Container.Inject(Activator.CreateInstance(serviceType));
                instance = Container.Resolve(serviceType);
            }
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.ResolveAll<object>(serviceType);
        }
    }
}

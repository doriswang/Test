using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Test.Framework.Extensibility
{
    public class CustomHttpControllerActivator : IHttpControllerActivator
    {
        public CustomHttpControllerActivator() { }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return Container.Resolve(controllerType) as IHttpController;
        }
    }
}

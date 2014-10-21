using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Test.Framework.Validation;
using Test.Framework.Extensions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Test.Framework.Extensibility
{
    public class CustomControllerActivator : IControllerActivator
    {
        IController IControllerActivator.Create(RequestContext requestContext, Type controllerType)
        {
            return Container.Resolve(controllerType) as IController;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Test.WebApi.Middleware
{
    public class CustomAuthorizationFilterAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //Helper.Write("AuthorizationFilter", actionContext.RequestContext.Principal);

            var principal = actionContext.RequestContext.Principal;

            return true;
        }
    }
}
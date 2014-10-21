using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Test.Framework.Extensions;
using Test.WebApi.Middleware;

namespace Test.WebApi.Controllers
{
    [CustomAuthorizationFilter]
    public class IdentityController : ApiController
    {
        public IHttpActionResult Get()
        {
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            if (principal == null)
                return InternalServerError();

            if (principal.Claims.IsNullOrEmpty())
                return InternalServerError();

            var result = principal.Claims.Select(x => new ViewClaim { Type = x.Type, Value = x.Value });

            if (result == null)
                return InternalServerError();

            return Ok<IEnumerable<ViewClaim>>(result);
        }

        public class ViewClaim
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }
    }
}

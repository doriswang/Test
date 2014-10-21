using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.WebApi.Middleware;

namespace Test.WebApi.Extensions
{
    public static class JwtAuthenticationExtensions
    {
        public static IAppBuilder UseJwtAuthentication(this IAppBuilder app, JwtAuthenticationOptions options)
        {
            return app.Use<JwtAuthenticationMiddleware>(options);
        }
    }
}
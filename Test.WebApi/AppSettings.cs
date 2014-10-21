using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Framework.Configuration;
using Test.Framework.Extensibility;

namespace Test.WebApi
{
    public static class AppSettings
    {
        private static IWebConfiguration config = Container.Resolve<IWebConfiguration>();
        public static string JwtIssuerName { get { return config.AppSettings["Jwt:IssuerName"]; } }
        public static string JwtSigningKey { get { return config.AppSettings["Jwt:SigningKey"]; } }
        public static string JwtAllowedAudience { get { return config.AppSettings["Jwt:AllowedAudience"]; } }
        public static TimeSpan JwtDefaultTimeSpan { get { return TimeSpan.Parse(config.AppSettings["Jwt:DefaultTimeSpan"]); } }
    }
}
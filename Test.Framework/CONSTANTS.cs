using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework.Configuration;
using Test.Framework.Extensibility;

namespace Test.Framework
{
    public static class CONSTANTS
    {
        private static IWebConfiguration config = Container.Resolve<IWebConfiguration>();
        public static string AuthenticationConnectionStringName { get { return config.AppSettings["AuthenticationConnectionStringName"]; } }
        public static string AuthenticationConnectionString { get { return config.ConnectionStrings(AuthenticationConnectionStringName); } }

        public static string SongsConnectionStringName { get { return config.AppSettings["SongsConnectionStringName"]; } }
        public static string SongsConnectionString { get { return config.ConnectionStrings(SongsConnectionStringName); } }

        public static string ExtensibilityKey { get { return "{0}_Test_{1}_Key"; } }
    }
}

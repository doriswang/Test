using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Framework.Configuration;
using Test.Framework.Extensibility;

namespace Test.Scratch
{
    public static class AppSettings
    {
        private static IWebConfiguration config = Container.Resolve<IWebConfiguration>();
        public static string JwtIssuerName { get { return config.AppSettings["Jwt:IssuerName"]; } }
        public static string JwtSigningKey { get { return config.AppSettings["Jwt:SigningKey"]; } }
        public static string JwtAllowedAudience { get { return config.AppSettings["Jwt:AllowedAudience"]; } }
        public static TimeSpan JwtDefaultTimeSpan { get { return TimeSpan.Parse(config.AppSettings["Jwt:DefaultTimeSpan"]); } }

        public static string Root { get { return AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", ""); } }
        public static string AppData { get { return Root + config.AppSettings["Path:AppData"]; } }
        public static string XML { get { return AppData + config.AppSettings["Path:Xml"]; } }
        public static string JSON { get { return AppData + config.AppSettings["Path:Json"]; } }
        public static string XMLAlbumFile { get { return XML + config.AppSettings["File:Xml:Album"]; } }
        public static string XMLSongFile { get { return XML + config.AppSettings["File:Xml:Song"]; } }
        public static string JSONAlbumFile { get { return JSON + config.AppSettings["File:Json:Album"]; } }
        public static string JSONSongFile { get { return JSON + config.AppSettings["File:Json:Song"]; } }
    }
}
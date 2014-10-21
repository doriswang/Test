using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Test.WebApi.Extensions
{
    public static class RouteExtensions
    {
        public static void RegisterDefaultRoutes(this HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void RegisterAuthenticationRoutes(this HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                 name: "AuthenticationUser",
                 routeTemplate: "api/identity/user/{id}",
                 defaults: new { controller = "AuthenticationUser", id = RouteParameter.Optional }
            );
        }

        public static void RegisterMusicRoutes(this HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                 name: "Album",
                 routeTemplate: "api/music/album/{id}",
                 defaults: new { controller = "Album", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                 name: "Song",
                 routeTemplate: "api/music/song/{albumId}/{id}",
                 defaults: new { controller = "Song", id = RouteParameter.Optional }
            );
        }
    }
}
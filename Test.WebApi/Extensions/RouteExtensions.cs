using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

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
                 routeTemplate: "api/music/album/{albumId}/song/{id}",
                 defaults: new { controller = "Song", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                 name: "Songs",
                 routeTemplate: "api/music/album/{albumId}/songs",
                 defaults: new { controller = "Songs" }
            );

            config.Routes.MapHttpRoute(
                 name: "Artist",
                 routeTemplate: "api/music/artist/{name}",
                 defaults: new { controller = "Artist" }
            );

            config.Routes.MapHttpRoute(
                name: "Music",
                routeTemplate: "api/music",
                defaults: new { controller = "Music" }
            );
        }
    }
}
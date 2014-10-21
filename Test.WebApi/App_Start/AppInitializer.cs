using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Data;
using Test.Framework.Extensibility;
using Test.Identity;
using Test.WebApi.Middleware;
using Test.WebApi.Services;

namespace Test.WebApi
{
    public static class AppInitializer
    {
        public static void Initialize()
        {
            Test.Framework.Framework.Initialize();
            DataAccessLayer.Initialize();
            TestIdentityProvider.Initialize();
            Container.Register<ISigningCredentialsProvider, SigningCredentialsProvider>();
            ServiceRegistry();
        }

        private static void ServiceRegistry()
        {
            Container.Register<IAlbumService, AlbumService>();
            Container.Register<ISongService, SongService>();
        }
    }
}
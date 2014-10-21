using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly : OwinStartup(typeof(Test.WebApi.Startup))]
namespace Test.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AppInitializer.Initialize();

            ConfigureAuth(app);

            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(config);
        }
    }
}
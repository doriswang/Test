using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Test.Framework.Extensibility;
using Microsoft.Owin.Security;
using System.IdentityModel.Tokens;
using Test.WebApi.Middleware;
using Test.WebApi.Extensions;

namespace Test.WebApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            //PublicClientId = "self";
            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //TokenEndpointPath = new PathString("/Token"),
            //Provider = new ApplicationOAuthProvider(PublicClientId),
            //Provider = new SimpleAuthorizationServerProvider(),
            //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //AllowInsecureHttp = true
            //};

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                // for demo purposes
                AccessTokenFormat = new CustomJwtFormat(
                    Container.Resolve<ISigningCredentialsProvider>(), 
                    AppSettings.JwtDefaultTimeSpan),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),
                Provider = new SimpleAuthorizationServerProvider()
            });

            // token consumption
            app.UseJwtAuthentication(new JwtAuthenticationOptions
            {
                AccessTokenFormat = new CustomJwtFormat(Container.Resolve<ISigningCredentialsProvider>(), AppSettings.JwtDefaultTimeSpan),
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { AppSettings.JwtAllowedAudience },
                SigningCredentials = Container.Resolve<ISigningCredentialsProvider>().GetSigningCredentials(AppSettings.JwtIssuerName, AppSettings.JwtAllowedAudience)
            });

            //app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            //{
            //    AuthenticationMode = AuthenticationMode.Active,
            //    TokenValidationParameters = new TokenValidationParameters{
            //        ValidAudiences = new[] { AppSettings.JwtAllowedAudience },
            //        IssuerSigningKey = new InMemorySymmetricSecurityKey(Convert.FromBase64String(AppSettings.JwtSigningKey)),
            //        ValidIssuer = AppSettings.JwtIssuerName
            //    }
            //});



            // Enable the application to use bearer tokens to authenticate users
            //app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
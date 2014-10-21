using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Framework.Extensibility;

namespace Test.WebApi.Middleware
{
    public class JwtAuthenticationMiddleware : AuthenticationMiddleware<JwtAuthenticationOptions>
    {
        private string challenge;

        public JwtAuthenticationMiddleware(OwinMiddleware next, JwtAuthenticationOptions options)
            : base(next, options)
        {
            if (!string.IsNullOrWhiteSpace(Options.Challenge))
            {
                challenge = Options.Challenge;
            }
            else if (string.IsNullOrWhiteSpace(Options.Realm))
            {
                challenge = "Bearer";
            }
            else
            {
                challenge = "Bearer realm=\"" + Options.Realm + "\"";
            }
            if (Options.AccessTokenFormat == null)
            {
                Options.AccessTokenFormat = new CustomJwtFormat(Container.Resolve<ISigningCredentialsProvider>(), AppSettings.JwtDefaultTimeSpan);
            }
            if (Options.Provider == null)
            {
                Options.Provider = new OAuthBearerAuthenticationProvider();
            }
            if (Options.AccessTokenProvider == null)
            {
                Options.AccessTokenProvider = new AuthenticationTokenProvider();
            }
        }

        protected override AuthenticationHandler<JwtAuthenticationOptions> CreateHandler()
        {
            return new JwtAuthenticationHandler(Options, challenge);
        }
    }
}
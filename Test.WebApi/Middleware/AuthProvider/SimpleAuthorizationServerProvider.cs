using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Test.Identity.Model;
using Test.Identity.Stores;
using Test.Framework.Extensibility;
using Microsoft.AspNet.Identity;

namespace Test.WebApi.Middleware
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public ApplicationUserManager userManager
        {
            get
            {
                return new ApplicationUserManager(Container.Resolve<IUserStore<IdentityUser>>());
            }
        }

        //public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    // validate client credentials
        //    // should be stored securely (salted, hashed, iterated)
        //    string id, secret;
        //    if (context.TryGetBasicCredentials(out id, out secret))
        //    {
        //        if (secret == "secret")
        //        {
        //            context.Validated();
        //        }
        //    }
        //}

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // validate user credentials (demo mode)
            // should be stored securely (salted, hashed, iterated)        
            //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            IdentityUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            //if (context.UserName != context.Password)
            //{
            //context.Rejected();
            //return;
            //}

            // create identity
            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            id.AddClaim(new Claim("sub", context.UserName));
            id.AddClaim(new Claim("issued_at", DateTime.UtcNow.ToString()));
            id.AddClaim(new Claim("email", user.Email));
            id.AddClaim(new Claim("email_verified", user.EmailConfirmed.ToString()));

            // create metadata to pass on to refresh token provider
            //var props = new AuthenticationProperties(new Dictionary<string, string>
            //    {
            //        { "as:client_id", context.ClientId }
            //    });

            var props = new AuthenticationProperties(new Dictionary<string, string> { 
                { "issuer", AppSettings.JwtIssuerName },
                { "audience", AppSettings.JwtAllowedAudience }
            });

            var ticket = new AuthenticationTicket(id, props);
            context.Validated(ticket);
        }

        //public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        //{
        //    var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
        //    var currentClient = context.ClientId;

        //    // enforce client binding of refresh token
        //    if (originalClient != currentClient)
        //    {
        //        context.Rejected();
        //        return;
        //    }

        //    // chance to change authentication ticket for refresh token requests
        //    var newId = new ClaimsIdentity(context.Ticket.Identity);
        //    newId.AddClaim(new Claim("newClaim", "refreshToken"));

        //    var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
        //    context.Validated(newTicket);
        //}

        //public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        //{
        //    foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
        //    {
        //        context.AdditionalResponseParameters.Add(property.Key, property.Value);
        //    }

        //    return Task.FromResult<object>(null);
        //}

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        //public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        //{
        //    if (context.ClientId == _publicClientId)
        //    {
        //        Uri expectedRootUri = new Uri(context.Request.Uri, "/");

        //        if (expectedRootUri.AbsoluteUri == context.RedirectUri)
        //        {
        //            context.Validated();
        //        }
        //    }

        //    return Task.FromResult<object>(null);
        //}

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}
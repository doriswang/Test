using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Test.Framework.Handlers;
using Test.Framework.Handlers.Resource;
using Test.Identity.Entity;
using Test.Identity.Model;
using Test.WebApi.Models;

namespace Test.WebApi.ResponseEnrichers
{
    public class UserResponseEnricher : IResponseEnricher
    {
        public bool CanEnrich(HttpResponseMessage response)
        {
            var content = response.Content as ObjectContent;

            return 
                content != null
                && (
                content.ObjectType == typeof(UserModel) || 
                content.ObjectType == typeof(List<UserModel>) ||
                content.ObjectType == typeof(IEnumerable<UserModel>)
                );
        }

        public HttpResponseMessage Enrich(HttpResponseMessage response)
        {
            var urlHelper = response.RequestMessage.GetUrlHelper();

            UserModel singleUser;
            if (response.TryGetContentValue<UserModel>(out singleUser))
            {
                Enrich(singleUser, urlHelper);
                return response;
            }

            List<UserModel> users;
            if (response.TryGetContentValue<List<UserModel>>(out users))
            {
                foreach (var user in users)
                {
                    Enrich(user, urlHelper);
                }
                return response;
            }

            IEnumerable<UserModel> users2;
            if (response.TryGetContentValue<IEnumerable<UserModel>>(out users2))
            {
                foreach (var user in users2)
                {
                    Enrich(user, urlHelper);
                }
                return response;
            }

            return response;
        }

        private void Enrich(UserModel user, UrlHelper urlHelper)
        {
            var selfUrl = urlHelper.Link("AuthenticationUser", new { controller = "AuthenticationUser", id = user.Id});
            user.AddLink(new SelfLink(selfUrl));
            user.AddLink(new EditLink(selfUrl));
        }
    }
}
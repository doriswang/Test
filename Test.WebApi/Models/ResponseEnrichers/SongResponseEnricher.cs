using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Test.Framework.Handlers.Resource;
using Test.WebApi.Models;

namespace Test.WebApi.ResponseEnrichers
{
    public class SongResponseEnricher : IResponseEnricher
    {
        public bool CanEnrich(HttpResponseMessage response)
        {
            var content = response.Content as ObjectContent;

            return
                content != null
                && (
                content.ObjectType == typeof(SongModel) ||
                content.ObjectType == typeof(List<SongModel>) ||
                content.ObjectType == typeof(IEnumerable<SongModel>)
                );
        }

        public HttpResponseMessage Enrich(HttpResponseMessage response)
        {
            var urlHelper = response.RequestMessage.GetUrlHelper();

            SongModel singleUser;
            if (response.TryGetContentValue<SongModel>(out singleUser))
            {
                Enrich(singleUser, urlHelper);
                return response;
            }

            List<SongModel> users;
            if (response.TryGetContentValue<List<SongModel>>(out users))
            {
                foreach (var user in users)
                {
                    Enrich(user, urlHelper);
                }
                return response;
            }

            IEnumerable<SongModel> users2;
            if (response.TryGetContentValue<IEnumerable<SongModel>>(out users2))
            {
                foreach (var user in users2)
                {
                    Enrich(user, urlHelper);
                }
                return response;
            }

            return response;
        }

        private void Enrich(SongModel song, UrlHelper urlHelper)
        {
            var selfUrl = urlHelper.Link("Song", new { controller = "Song", albumId = song.AlbumId, id = song.Id });
            var albumUrl = urlHelper.Link("Album", new { controller = "Album", id = song.AlbumId });
            //song.AddLink(new SelfLink(selfUrl));
            song.AddLink(new RUDLink(selfUrl));
            //song.AddLink(new EditLink(selfUrl));
            song.AddLink(new RelatedLink(albumUrl));
        }
    }
}
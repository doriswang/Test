using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Test.Framework.Handlers.Resource;
using Test.Framework.Extensions;
using Test.WebApi.Models;

namespace Test.WebApi.ResponseEnrichers
{
    public class ArtistResponseEnricher : IResponseEnricher
    {
        public bool CanEnrich(HttpResponseMessage response)
        {
            var content = response.Content as ObjectContent;

            return
                content != null && 
                content.ObjectType == typeof(ArtistModel)
                ;
        }

        public HttpResponseMessage Enrich(HttpResponseMessage response)
        {
            var urlHelper = response.RequestMessage.GetUrlHelper();

            ArtistModel singleUser;
            if (response.TryGetContentValue<ArtistModel>(out singleUser))
            {
                Enrich(singleUser, urlHelper);
                return response;
            }

            return response;
        }

        private void Enrich(ArtistModel artist, UrlHelper urlHelper)
        {
            var selfUrl = urlHelper.Link("Artist", new { controller = "Artist", name = artist.Name });
            artist.AddLink(new SelfLink(selfUrl));

            if (artist.Albums.IsNotNullOrEmpty())
            {
                foreach (var album in artist.Albums)
                {
                    if (album == null || album.Id == 0)
                        continue;

                    Enrich(album, urlHelper);
                }
            }
        }

        private void Enrich(AlbumModel album, UrlHelper urlHelper)
        {
            var selfUrl = urlHelper.Link("Album", new { controller = "Album", id = album.Id });
            album.AddLink(new SelfLink(selfUrl));
            //album.AddLink(new EditLink(selfUrl));
            //album.AddLink(new RUDLink(selfUrl));

            if (album.Songs.IsNotNullOrEmpty())
            {
                foreach (var song in album.Songs)
                {
                    if (song == null || song.Id == 0)
                        continue;
                    if (song.AlbumId == 0)
                        song.AlbumId = album.Id;

                    Enrich(song, urlHelper);
                }
            }
        }

        private void Enrich(SongModel song, UrlHelper urlHelper)
        {
            var selfUrl = urlHelper.Link("Song", new { controller = "Song", albumId = song.AlbumId, id = song.Id });
            song.AddLink(new SelfLink(selfUrl));
            //song.AddLink(new EditLink(selfUrl));
            //song.AddLink(new RUDLink(selfUrl));
        }
    }
}
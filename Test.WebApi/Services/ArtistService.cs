using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Data;
using Test.Data.Repositories;
using Test.Framework.Extensibility;
using Test.Framework.Extensions;
using Test.WebApi.Models;

namespace Test.WebApi.Services
{
    public class ArtistService : IArtistService
    {
        public ArtistService()
        {
            this.provider = Container.Resolve<IDataProvider>();
            this.repository = provider.MusicRepository;
            this.albumService = Container.Resolve<IAlbumService>();
            this.songService = Container.Resolve<ISongService>();
        }

        public IDataProvider provider { get; set; }

        public IMusicRepository repository { get; set; }

        public IAlbumService albumService { get; set; }

        public ISongService songService { get; set; }

        public ArtistModel Get(string name)
        {
            var albums = albumService.GetAlbum(name);

            if (!albums.IsNotNullOrEmpty())
                return null;

            return new ArtistModel {
                Name = name,
                Albums = albums.ToList(),
            };
        }
    }
}
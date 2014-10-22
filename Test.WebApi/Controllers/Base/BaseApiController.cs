using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.Data;
using Test.Data.Repositories;
using Test.Framework.Extensibility;
using Test.WebApi.Services;

namespace Test.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        public BaseApiController()
        {
            this.provider = Container.Resolve<IDataProvider>();
            this.musicRepository = provider.MusicRepository;
            this.songService = Container.Resolve<ISongService>();
            this.albumService = Container.Resolve<IAlbumService>();
            this.artistService = Container.Resolve<IArtistService>();
            this.musicService = Container.Resolve<IMusicService>();
        }

        public IDataProvider provider { get; set; }

        public IMusicRepository musicRepository { get; set; }

        public IAlbumService albumService { get; set; }

        public ISongService songService { get; set; }

        public IArtistService artistService { get; set; }

        public IMusicService musicService { get; set; }
    }
}

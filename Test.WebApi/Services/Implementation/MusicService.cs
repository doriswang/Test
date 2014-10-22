using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Test.Data;
using Test.Data.Repositories;
using Test.Framework.Extensibility;
using Test.Framework.Extensions;
using Test.WebApi.Models;

namespace Test.WebApi.Services
{
    public class MusicService : IMusicService
    {
        public MusicService(IDataProvider provider, ISongService songService, IAlbumService albumService, IArtistService artistService)
        {
            this.provider = provider;
            this.repository = provider.MusicRepository;
            this.songService = songService;
            this.albumService = albumService;
            this.artistService = artistService;
        }

        public IDataProvider provider { get; set; }

        public IMusicRepository repository { get; set; }

        public ISongService songService { get; set; }

        public IAlbumService albumService { get; set; }

        public IArtistService artistService { get; set; }


        public RequestResult<MusicModel> GetMusic()
        {
            var result = new MusicModel();

            var artistNames = repository.GetArtistNames();

            if (artistNames.IsNullOrEmpty())
                return new RequestResult<MusicModel>(HttpStatusCode.NotFound);

            List<ArtistModel> artistResults = new List<ArtistModel>();
            foreach (var artistName in artistNames)
            {
                if (artistName.IsNullOrEmpty())
                    continue;

                var artistResult = artistService.GetArtist(artistName);

                if (artistResult == null)
                    continue;

                artistResults.Add(artistResult);
            }

            if (artistResults.IsNullOrEmpty())
                return new RequestResult<MusicModel>(HttpStatusCode.NotFound);

            result.Artists = artistResults;

            return new RequestResult<MusicModel>(result);
        }
    }
}
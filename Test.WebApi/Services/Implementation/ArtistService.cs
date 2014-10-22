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

        public ArtistModel GetArtist(string name)
        {
            var albums = albumService.GetAlbum(name);

            if (!albums.IsNotNullOrEmpty())
                return null;

            return new ArtistModel {
                Name = name,
                Albums = albums.ToList(),
            };
        }

        public RequestResult<ArtistModel> AddArtist(ArtistModel artistModel)
        { 
            if (artistModel == null || 
                artistModel.Name.IsNullOrEmpty() || 
                artistModel.Albums.IsNullOrEmpty())
                return new RequestResult<ArtistModel>(HttpStatusCode.BadRequest);

            List<RequestResult<AlbumModel>> albumResult = new List<RequestResult<AlbumModel>>();
            foreach (var album in artistModel.Albums)
            {
                album.ArtistName = artistModel.Name;
                albumResult.Add(albumService.AddAlbum(album));
            }

            if (albumResult.Where(x => x.InternalServerError == true).Count() > 0)
                return new RequestResult<ArtistModel>("Albums Not Added");

            if (albumResult.Where(x => x.Status != HttpStatusCode.OK).Count() > 0)
                return new RequestResult<ArtistModel>("Albums Not Added");

            artistModel.Albums = albumResult.Where(x => x.Model != null).Select(x => x.Model).ToList();

            return new RequestResult<ArtistModel>(artistModel);
        }

        public RequestResult<ArtistModel> UpdateArtist(ArtistModel artistModel)
        {
            if (artistModel == null || artistModel.Name.IsNullOrEmpty())
                return new RequestResult<ArtistModel>(HttpStatusCode.BadRequest);

            List<AlbumModel> albumsToEdit = new List<AlbumModel>();
            if (artistModel.Albums.IsNullOrEmpty())
            {
                var albums = albumService.GetAlbum(artistModel.Name);
                if (albums.IsNullOrEmpty())
                    return new RequestResult<ArtistModel>(HttpStatusCode.NotFound);

                albums = albums.Select(x => { x.ArtistName = artistModel.Name; return x; });

                albumsToEdit.AddRange(albums.ToList());
            }
            else 
            {
                albumsToEdit.AddRange(artistModel.Albums.Select(x => { x.ArtistName = artistModel.Name; return x; }).ToList());
            }

            List<SongModel> songsToEdit = new List<SongModel>();
            songsToEdit = albumsToEdit.SelectMany(x => x.Songs.Select(y => { if (y.AlbumId == 0) { y.AlbumId = x.Id; } return y; })).ToList();

            List<SongModel> songsEdited = new List<SongModel>();
            foreach (var song in songsToEdit)
            {
                var tempResult = songService.UpdateSong(song.AlbumId, song);
                if (tempResult.Model != null)
                    songsEdited.Add(tempResult.Model);
            }

            List<RequestResult<AlbumModel>> albumResult = new List<RequestResult<AlbumModel>>();
            artistModel.Albums = new List<AlbumModel>();
            foreach (var album in albumsToEdit)
            {
                var tempResult = albumService.UpdateAlbum(album);

                albumResult.Add(tempResult);

                if (tempResult.Model == null)
                    continue;

                tempResult.Model.Songs = songsEdited.Where(x => x.AlbumId == tempResult.Model.Id).ToList();
                artistModel.Albums.Add(tempResult.Model);
            }

            if (albumResult.Where(x => x.InternalServerError == true).Count() > 0)
                return new RequestResult<ArtistModel>("Albums Not Updated");

            if (albumResult.Where(x => x.Status != HttpStatusCode.OK).Count() > 0)
                return new RequestResult<ArtistModel>("Albums Not Updated");

            return new RequestResult<ArtistModel>(artistModel);
        }

        public RequestResult<ArtistModel> DeleteArtist(string name)
        {
            var albums = albumService.GetAlbum(name);

            if (!albums.IsNotNullOrEmpty())
                return new RequestResult<ArtistModel>(HttpStatusCode.NotFound);

            List<RequestResult<AlbumModel>> albumResult = new List<RequestResult<AlbumModel>>();
            foreach (var album in albums)
            {
                albumResult.Add(albumService.DeleteAlbum(album));
            }

            if (albumResult.Where(x => x.Status != HttpStatusCode.OK).Count() > 0)
                return new RequestResult<ArtistModel>("Albums Not Deleted");

            return new RequestResult<ArtistModel>();
        }
    }
}
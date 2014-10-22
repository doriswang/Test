using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Data;
using Test.Data.Repositories;
using Test.Framework.Extensibility;
using Test.Framework.Extensions;
using Test.WebApi.Middleware;
using AutoMapper;
using Test.Entities.Entity.Songs;
using Test.WebApi.Models;
using System.Net;

namespace Test.WebApi.Services
{
    public class AlbumService : IAlbumService
    {
        public AlbumService(IDataProvider dataprovider, ISongService songService)
        {
            this.provider = dataprovider;
            this.repository = provider.MusicRepository;
            this.songService = songService;
        }

        public IDataProvider provider { get; set; }
        public IMusicRepository repository { get; set; }
        public ISongService songService { get; set; }

        #region IAlbumService Members

        public AlbumModel GetAlbum(int id)
        {
            var result = new AlbumModel();
            var album = repository.GetAlbum(id);
            if (album == null)
                return null;

            Mapper.CreateMap<Album, AlbumModel>();
            result = Mapper.Map<AlbumModel>(album);

            var albumSongs = repository.GetSong(id);

            if (!albumSongs.IsNotNullOrEmpty())
                return result;

            Mapper.CreateMap<Song, SongModel>();
            List<SongModel> songs = new List<SongModel>();
            foreach (var albumSong in albumSongs)
            {
                var song = Mapper.Map<SongModel>(albumSong);
                songs.Add(song);
            }
            result.Songs = songs;

            return result;
        }

        public IEnumerable<AlbumModel> GetAlbum(string artistName)
        {
            var albumIds = repository.GetAlbumIds(artistName);
            if (!albumIds.IsNotNullOrEmpty())
                yield return null;

            foreach (var albumId in albumIds)
            {
                yield return GetAlbum(albumId);
            }
        }

        public IEnumerable<AlbumModel> GetAlbum(AlbumInputModel inputModel)
        {
            List<AlbumModel> result = new List<AlbumModel>();

            IEnumerable<Album> albums = null;
            if (inputModel.artistName.IsNotNullOrEmpty())
                return GetAlbum(inputModel.artistName).ToList();
            else
                albums = repository.GetAlbum();

            if (!albums.IsNotNullOrEmpty())
                return null;

            Mapper.CreateMap<Album, AlbumModel>();
            foreach (var album in albums)
            {
                var albumModel = Mapper.Map<AlbumModel>(album);
                var albumSongs = repository.GetSong(album.Id);

                if (!albumSongs.IsNotNullOrEmpty())
                {
                    result.Add(albumModel);
                    continue;
                }

                Mapper.CreateMap<Song, SongModel>();
                List<SongModel> songs = new List<SongModel>();
                foreach (var albumSong in albumSongs)
                {
                    var song = Mapper.Map<SongModel>(albumSong);
                    songs.Add(song);
                }
                albumModel.Songs = songs;
                result.Add(albumModel);
            }

            return result;
        }

        public RequestResult<AlbumModel> AddAlbum(AlbumModel albumModel)
        {
            if (albumModel == null)
                return new RequestResult<AlbumModel>(HttpStatusCode.BadRequest);

            Mapper.CreateMap<AlbumModel, Album>();
            var album = Mapper.Map<Album>(albumModel);

            var albumId = repository.AddAlbum(album);
            albumModel.Id = albumId;

            if (!albumModel.Songs.IsNotNullOrEmpty())
                return new RequestResult<AlbumModel>(albumModel);

            Mapper.CreateMap<SongModel, Song>();

            List<SongModel> songResult = new List<SongModel>();
            var counter = 0;
            var albumSongs = albumModel.Songs.Where(x => x != null).ToList();
            foreach (var songModel in albumSongs)
            {
                if (songModel == null)
                {
                    counter++;
                    continue;
                }

                var tempResult = songService.AddSong(albumId, songModel);

                if (tempResult.Status != HttpStatusCode.OK ||
                    tempResult.Model == null)
                {
                    counter++;
                    continue;;
                }

                albumModel.Songs[counter] = tempResult.Model;

                counter++;
            }

            return new RequestResult<AlbumModel>(albumModel);
        }

        public RequestResult<AlbumModel> UpdateAlbum(AlbumModel albumModel)
        {
            var currentAlbum = repository.GetAlbum(albumModel.Id);
            if (currentAlbum == null)
                return new RequestResult<AlbumModel>(HttpStatusCode.NotFound);

            if (albumModel.ArtistName.IsNotNullOrEmpty()) currentAlbum.ArtistName = albumModel.ArtistName;
            if (albumModel.Title.IsNotNullOrEmpty()) currentAlbum.Title = albumModel.Title;
            if (albumModel.Id != 0) currentAlbum.Id = albumModel.Id;

            var repoResult = repository.UpdateAlbum(currentAlbum);
            if (!repoResult)
                return new RequestResult<AlbumModel>("Cannot Update Album");

            Mapper.CreateMap<Album, AlbumModel>();
            var songs = albumModel.Songs;
            albumModel = Mapper.Map<AlbumModel>(currentAlbum);
            albumModel.Songs = songs;
            return new RequestResult<AlbumModel>(albumModel);
        }

        public RequestResult<AlbumModel> DeleteAlbum(int albumId)
        {
            return DeleteAlbum(new AlbumModel { Id = albumId });
        }

        public RequestResult<AlbumModel> DeleteAlbum(AlbumModel albumModel)
        {
            if (albumModel == null || albumModel.Id == 0)
                return new RequestResult<AlbumModel>(HttpStatusCode.BadRequest);

            if (!repository.AlbumExists(albumModel.Id))
                return new RequestResult<AlbumModel>(HttpStatusCode.NotFound);

            if (HasSongs(albumModel.Id))
            {
                var songResult = songService.DeleteSong(albumModel.Id);
                if (songResult.Status != HttpStatusCode.OK)
                    return new RequestResult<AlbumModel>("Cannot delete Album's Songs");
            }

            var result = repository.DeleteAlbum(albumModel.Id);

            return new RequestResult<AlbumModel>();
        }

        public bool HasSongs(int albumId) 
        {
            return repository.SongExists(albumId);
        }

        public bool HasSongs(AlbumModel albumModel)
        {
            return HasSongs(albumModel.Id);
        }

        #endregion



    }
}
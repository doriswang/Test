using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Data;
using Test.Data.Repositories;
using Test.Framework.Extensibility;
using Test.Framework.Extensions;
using AutoMapper;
using Test.Entities.Entity.Songs;
using Test.WebApi.Models;

namespace Test.WebApi.Services
{
    public class AlbumService : IAlbumService
    {
        public AlbumService(IDataProvider dataprovider)
        {
            this.provider = dataprovider;
            this.repository = provider.MusicRepository;
        }
        public IDataProvider provider { get; set; }

        public IMusicRepository repository { get; set; }

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

        public int AddAlbum(AlbumModel albumModel)
        {
            if (albumModel == null)
                return 0;

            Mapper.CreateMap<AlbumModel, Album>();
            var album = Mapper.Map<Album>(albumModel);

            var result = repository.AddAlbum(album);
            return result;
        }

        public bool UpdateAlbum(AlbumModel albumModel)
        {
            var currentAlbum = repository.GetAlbum(albumModel.Id);
            if (currentAlbum == null)
                return false;

            Mapper.CreateMap<AlbumModel, Album>();
            currentAlbum = Mapper.Map<Album>(albumModel);

            var result = repository.UpdateAlbum(currentAlbum);
            return result;
        }

        public bool DeleteAlbum(AlbumModel albumModel)
        {
            if (albumModel == null ||
                albumModel.Id == 0)
                return false;

            var currentAlbum = repository.AlbumExists(albumModel.Id);

            if (!currentAlbum)
                return false;

            if (HasSongs(albumModel.Id))
                return false;

            var result = repository.DeleteAlbum(albumModel.Id);
            return result;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Data;
using Test.Data.Repositories;
using Test.Framework.Extensibility;
using Test.Framework.Extensions;
using Test.WebApi.Models;
using AutoMapper;
using Test.Entities.Entity.Songs;

namespace Test.WebApi.Services
{
    public class SongService : ISongService
    {
        public SongService()
        {
            this.provider = Container.Resolve<IDataProvider>();
            this.repository = provider.MusicRepository;
        }

        public IDataProvider provider { get; private set; }

        public IMusicRepository repository { get; private set; }

        public SongModel GetSong(int albumId, int id)
        {
            var song = repository.GetSong(albumId, id);

            if (song == null)
                return null;

            Mapper.CreateMap<Song, SongModel>();
            var result = Mapper.Map<SongModel>(song);

            return result;
        }

        public IEnumerable<SongModel> GetSong(int albumId)
        {
            List<SongModel> result = new List<SongModel>();

            var songs = repository.GetSong(albumId);

            if (!songs.IsNotNullOrEmpty())
                return null;

            Mapper.CreateMap<Song, SongModel>();
            foreach (var song in songs)
            {
                if (song == null)
                    continue;

                var songModel = Mapper.Map<SongModel>(song);

                if (songModel == null)
                    continue;

                result.Add(songModel);
            }

            return result;
        }

        public IEnumerable<SongModel> GetSong(string artistName)
        {
            List<SongModel> result = new List<SongModel>();

            var albums = repository.GetAlbum(artistName);

            if (!albums.IsNotNullOrEmpty())
                return null;

            foreach (var album in albums)
            {
                var songList = GetSong(album.Id);

                if (!songList.IsNotNullOrEmpty())
                    continue;

                result.AddRange(songList);
            }

            return result;
        }

        public int AddSong(int albumId, SongModel songModel)
        {
            Mapper.CreateMap<SongModel, Song>();
            var song = Mapper.Map<Song>(songModel);

            if (song.AlbumId == 0)
                song.AlbumId = albumId;

            var result = repository.AddSong(albumId, song);

            return result;
        }

        public bool UpdateSong(int albumId, SongModel songModel)
        {
            Mapper.CreateMap<SongModel, Song>();
            var song = Mapper.Map<Song>(songModel);

            if (song.AlbumId == 0)
                song.AlbumId = albumId;

            var result = repository.UpdateSong(albumId, song);

            return result;
        }
        public bool DeleteSong(int albumId)
        {
            var albumExists = repository.AlbumExists(albumId);

            if (!albumExists)
                return false;

            var result = repository.DeleteSong(albumId);

            return result;
        }

        public bool DeleteSong(int albumId, int songId)
        {
            var result = repository.DeleteSong(albumId, songId);
            return result;
        }

        public bool DeleteSong(int albumId, SongModel songModel)
        {
            return DeleteSong(albumId, songModel.Id);
        }
    }
}
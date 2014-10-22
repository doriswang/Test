using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Repositories;
using Test.Entities.Entity.Songs;
using Test.Framework.Extensions;

namespace Test.Data.File.Xml
{
    public class XmlMusicRepository : IMusicRepository
    {
        public XmlMusicRepository(string xmlAlbumFile, string xmlSongFile)
        {
            this.database = new XmlDatabase(xmlAlbumFile, xmlSongFile);
        }
        public XmlDatabase database { get; set; }

        public int GetMax<T>(int albumId = 0)
        {
            if (typeof(T) == typeof(Album))
            {
                var list = database.Select<Album>();

                if (list.IsNullOrEmpty())
                    return 0;

                return (int)list.Select(x => x.Id).Max();
            }
            else if (typeof(T) == typeof(Song))
            {
                var list = database.Select<Song>();

                if (list.IsNullOrEmpty())
                    return 0;

                return (int)list.Where(x => x.AlbumId == albumId).Select(x => x.Id).Max();
            }
            return 0;
        }

        public bool AlbumExists(int albumId)
        {
            var albums = database.Select<Album>();
            if (albums.IsNullOrEmpty())
                return false;

            return albums.Where(x => x.Id == albumId).FirstOrDefault() != null;
        }

        public bool SongExists(int albumId, int songId)
        {
            var songs = database.Select<Song>();
            if (songs.IsNullOrEmpty())
                return false;
            return songs.Where(x => x.Id == songId && x.AlbumId == albumId).FirstOrDefault() != null;
        }

        public bool SongExists(int albumId)
        {
            var songs = database.Select<Song>();
            if (songs.IsNullOrEmpty())
                return false;
            return songs.Where(x => x.AlbumId == albumId).Count() > 0;
        }

        public IEnumerable<string> GetArtistNames()
        {
            var albums = GetAlbum();
            if (albums.IsNullOrEmpty())
                return null;
            return albums.Select(x => x.ArtistName);
        }

        public Album GetAlbum(int albumId)
        {
            return GetAlbum().Where(x => x.Id == albumId).FirstOrDefault();
        }

        public IEnumerable<Album> GetAlbum(string artistName)
        {
            return GetAlbum().Where(x => x.ArtistName.Equals(artistName));
        }

        public IEnumerable<int> GetAlbumIds(string artistName)
        {
            return GetAlbum().Where(x => x.ArtistName.Equals(artistName)).Select(x => x.Id);
        }

        public IEnumerable<Album> GetAlbum()
        {
            return database.Select<Album>();
        }

        public IEnumerable<int> GetAlbumIds()
        {
            return GetAlbum().Select(x => x.Id);
        }

        private List<Song> GetSong()
        {
            return database.Select<Song>();
        }

        public Song GetSong(int albumId, int songId)
        {
            return GetSong().Where(x => x.AlbumId == albumId && x.Id == songId).FirstOrDefault();
        }

        public IEnumerable<Song> GetSong(int albumId)
        {
            return GetSong().Where(x => x.AlbumId == albumId);
        }

        public IEnumerable<Song> GetSong(string artistName)
        {
            var result = new List<Song>();

            var albums = GetAlbum();

            if(albums.IsNullOrEmpty())
                return null;

            foreach (var album in albums)
            {
                var albumSongs = GetSong(album.Id);

                if (albumSongs.IsNullOrEmpty())
                    continue;

                result.AddRange(albumSongs);
            }

            return result;
        }

        public bool UpdateAlbum(Album album)
        {
            var dbAlbums = GetAlbum();

            if (dbAlbums.IsNullOrEmpty())
                return false;

            var albums = dbAlbums.ToList();

            var currentAlbum = albums.Where(x => x.Id == album.Id).FirstOrDefault();

            if (currentAlbum == null)
                return false;

            if (album.Title.IsNotNullOrEmpty()) currentAlbum.Title = album.Title;
            if (album.ArtistName.IsNotNullOrEmpty()) currentAlbum.ArtistName = album.ArtistName;
            if (album.Id != 0) currentAlbum.Id = album.Id;

            return database.Insert<Album>(albums);
        }

        public bool UpdateSong(Album album, Song song)
        {
            return UpdateSong(album.Id, song);
        }

        public bool UpdateSong(int albumId, Song song)
        {
            var dbSongs = GetSong();

            if (dbSongs.IsNullOrEmpty())
                return false;

            var songs = dbSongs.ToList();

            var currentSong = songs.Where(x => x.Id == song.Id && x.AlbumId == albumId).FirstOrDefault();

            if (currentSong == null)
                return false;

            if (song.Id != 0) currentSong.Id = song.Id;
            if (song.AlbumId != 0) currentSong.AlbumId = song.AlbumId;
            if (song.Title.IsNotNullOrEmpty()) currentSong.Title = song.Title;
            if (song.Length.IsNotNullOrEmpty()) currentSong.Length = song.Length;
            if (song.TrackNumber != 0) currentSong.TrackNumber = song.TrackNumber;
            if (song.Genre.IsNotNullOrEmpty()) currentSong.Genre = song.Genre;

            return database.Insert<Song>(songs);
        }

        public int AddAlbum(Album album)
        {
            var dbAlbums = GetAlbum();

            if (dbAlbums.IsNullOrEmpty())
                dbAlbums = new List<Album>();

            var albums = dbAlbums.ToList();

            var maxAlbumId = 0;

            if(albums.IsNotNullOrEmpty())
                maxAlbumId = albums.Max(x => x.Id);

            album.Id = (int)maxAlbumId + 1;
            albums.Add(album);

            if (database.Insert<Album>(albums))
                return album.Id;

            return 0;
        }

        public int AddSong(Album album, Song song)
        {
            return AddSong(album.Id, song);
        }

        public int AddSong(int albumId, Song song)
        {
            var dbSongs = GetSong();

            if (dbSongs.IsNullOrEmpty())
                dbSongs = new List<Song>();

            var songs = dbSongs.ToList();

            var maxSongId = 0;

            if (songs.IsNotNullOrEmpty())
            { 
                var albumSongs = songs.Where(x => x.AlbumId == albumId);
                if(albumSongs.IsNotNullOrEmpty())
                    maxSongId = albumSongs.Max(x => x.Id);
            }

            song.Id = (int)maxSongId + 1;
            songs.Add(song);

            if (database.Insert<Song>(songs))
                return song.Id;

            return 0;
        }

        public List<int> AddSong(int albumId, IEnumerable<Song> inputSongs)
        {
            var dbSongs = GetSong();

            if (dbSongs.IsNullOrEmpty())
                dbSongs = new List<Song>();

            var songs = dbSongs.ToList();

            List<int> result = new List<int>();

            var maxSongId = 0;

            if (songs.IsNotNullOrEmpty())
            {
                var albumSongs = songs.Where(x => x.AlbumId == albumId);
                if (albumSongs.IsNotNullOrEmpty())
                    maxSongId = albumSongs.Max(x => x.Id);
            }

            foreach (var song in inputSongs)
            {
                song.Id = (int)maxSongId + 1;
                songs.Add(song);
                result.Add(song.Id);
                maxSongId++;
            }

            if (database.Insert<Song>(songs))
                return result;

            return null;
        }

        public List<int> AddSong(Album album, IEnumerable<Song> songs)
        {
            return AddSong(album.Id, songs);
        }

        public bool DeleteAlbum(Album album)
        {
            return DeleteAlbum(album.Id);
        }

        public bool DeleteAlbum(int albumId)
        {
            var dbAlbums = GetAlbum();

            if (dbAlbums.IsNullOrEmpty())
                return false;

            var albums = dbAlbums.ToList();

            if(albums.RemoveAll(x => x.Id == albumId) > 0)
                return true;

            return false;
        }

        public bool DeleteSong(int albumId)
        {
            var dbSongs = GetSong();

            if (dbSongs.IsNullOrEmpty())
                dbSongs = new List<Song>();

            var songs = dbSongs.ToList();

            if (songs.RemoveAll(x => x.AlbumId == albumId) > 0)
                return true;

            return false;
        }

        public bool DeleteSong(Album album, Song song)
        {
            return DeleteSong(album.Id, song.Id);
        }

        public bool DeleteSong(int albumId, Song song)
        {
            return DeleteSong(albumId, song.Id);
        }

        public bool DeleteSong(int albumId, int songId)
        {
            var dbSongs = GetSong();

            if (dbSongs.IsNullOrEmpty())
                dbSongs = new List<Song>();

            var songs = dbSongs.ToList();

            if (songs.RemoveAll(x => x.AlbumId == albumId && x.Id == songId) > 0)
                return true;

            return false;
        }
    }
}

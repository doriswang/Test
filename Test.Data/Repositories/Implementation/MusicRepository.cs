using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Entities.Entity.Songs;
using Test.Framework.DataAccess;
using Test.Framework.Extensions;
using Dapper;

namespace Test.Data.Repositories
{
    public class MusicRepository : BaseRepository, IMusicRepository
    {
        #region Private Members

        #endregion

        #region Constructors
        public MusicRepository(IDatabase database)
            : base(database)
        {
        }

        public MusicRepository(string connectionName)
            : base(connectionName)
        {
        }
        #endregion

        #region Public Methods

        public int GetMax<T>(int albumId = 0)
        {
            int result = 0;
            string sql;
            List<Parameter> parameters = new List<Parameter>();

            if (typeof(T) == typeof(Album))
            {
                sql = @"SELECT MAX(Id) as Result from [testdb].[dbo].[Albums];";
            }
            else if (typeof(T) == typeof(Song))
            {
                sql = @"SELECT MAX(Id) as Result from [testdb].[dbo].[Songs] Where AlbumId = @AlbumId;";
                parameters.AddParameter("AlbumId", albumId);
            }
            else
                return int.MinValue;

            using (var connection = this.Database.Connection)
            {
                CommandDefinition command;
                if (typeof(T) == typeof(Song))
                    command = connection.CreateDapperCommand(sql, parameters);
                else if (typeof(T) == typeof(Album))
                    command = connection.CreateDapperCommand(sql);
                else
                    return int.MinValue;

                var tempResult = connection.Query<string>(command).FirstOrDefault();
                if (tempResult.IsNotNullOrEmpty() && tempResult.IsInteger())
                    result = tempResult.ToInteger();
            }
            return result;
        }

        public bool AlbumExists(int albumId)
        { 
            var sql = @"SELECT Id FROM [testdb].[dbo].[Albums] WHERE Id = @albumid;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("albumid", albumId);
            return this.Database.Select<Album>(sql, parameters).IsNotNullOrEmpty();
        }

        public bool SongExists(int albumId, int songId)
        { 
            var sql = @"SELECT Id FROM [testdb].[dbo].[Songs] WHERE Id = @songid AND AlbumId = @albumid;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("songid", songId);
            parameters.AddParameter("albumid", albumId);
            return this.Database.Select<Song>(sql, parameters).IsNotNullOrEmpty();
        }

        public bool SongExists(int albumId)
        { 
            var sql = @"SELECT Id FROM [testdb].[dbo].[Songs] WHERE AlbumId = @albumid;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("albumid", albumId);
            return this.Database.Select<Song>(sql, parameters).IsNotNullOrEmpty();
        }

        public Album GetAlbum(int albumId)
        {
            return this.Database.Get<Album>(x => x.Id == albumId);
        }

        public IEnumerable<Album> GetAlbum(string artistName)
        {
            var sql = @"SELECT * FROM [testdb].[dbo].[Albums] WHERE ArtistName = @artistname;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("artistname", artistName.Trim());
            return this.Database.Select<Album>(sql, parameters);
        }

        public IEnumerable<int> GetAlbumIds(string artistName)
        {
            var sql = @"SELECT Id FROM [testdb].[dbo].[Albums] WHERE ArtistName = @artistname;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("artistname", artistName.Trim());
            return this.Database.Select<Album>(sql, parameters).Select(x => x.Id);
        }

        public IEnumerable<Album> GetAlbum()
        {
            var sql = @"SELECT * FROM [testdb].[dbo].[Albums]";
            return this.Database.Select<Album>(sql);
        }

        public IEnumerable<int> GetAlbumIds()
        {
            var sql = @"SELECT Id FROM [testdb].[dbo].[Albums]";
            return this.Database.Select<Album>(sql).Select(x => x.Id);
        }

        public Song GetSong(int albumId, int songId)
        {
            return this.Database.Get<Song>(x => x.AlbumId == albumId && x.Id == songId);
        }

        public IEnumerable<Song> GetSong(int albumId)
        {
            return this.Database.Select<Song>(x => x.AlbumId == albumId);
        }

        public IEnumerable<Song> GetSong(string artistName)
        {
            var albums = GetAlbum(artistName);

            if (!albums.IsNotNullOrEmpty())
                return null;

            IEnumerable<Song> songs = null;
            foreach (var album in albums)
            {
                var albumSongs = GetSong(album.Id);
                if (!albumSongs.IsNotNullOrEmpty())
                    continue;
                songs.Concat<Song>(albumSongs);
            }

            return songs;
        }

        public bool UpdateAlbum(Album album)
        {
            if (album == null)
                return false;

            var sql = @"
                            UPDATE 
                                [testdb].[dbo].[Albums] 
                            SET
                                ArtistName=@artistname, 
                                Title=@title
                            WHERE
                                Id = @albumid;";

            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("artistname", album.ArtistName);
            parameters.AddParameter("title", album.Title);
            parameters.AddParameter("albumid", album.Id);

            return this.Database.Execute(new List<SqlCommand> { new SqlCommand(sql, parameters) });
        }

        public bool UpdateSong(Album album, Song song)
        {
            return UpdateSong(album.Id, song);
        }

        public bool UpdateSong(int albumId, Song song)
        {
            if (song == null)
                return false;

            if (song.AlbumId == 0)
                song.AlbumId = albumId;

            song.DateModified = DateTime.Now;

            var sql = @"
                            UPDATE 
                                [testdb].[dbo].[Songs]
                            SET 
                                Title = @title, 
                                Length = @length, 
                                TrackNumber = @tracknumber, 
                                Genre = @genre, 
                                DateModified = @datemodified 
                            WHERE 
                                Id = @songid
                            AND
                                AlbumId = @albumid;";

            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("title", song.Title);
            parameters.AddParameter("length", song.Length);
            parameters.AddParameter("tracknumber", song.TrackNumber);
            parameters.AddParameter("genre", song.Genre);
            parameters.AddParameter("datemodified", song.DateModified);
            parameters.AddParameter("songid", song.Id);
            parameters.AddParameter("albumid", albumId);

            return this.Database.Execute(new List<SqlCommand> { new SqlCommand(sql, parameters) });
        }

        public int AddAlbum(Album album)
        {
            var maxAlbumId = GetMax<Album>() + 1;
            album.Id = maxAlbumId;

            if (this.Database.Insert<Album>(album))
                return maxAlbumId;

            return int.MinValue;
        }

        public int AddSong(Album album, Song song)
        {
            return AddSong(album.Id, song);
        }

        public int AddSong(int albumId, Song song)
        {
            if (song.AlbumId == 0)
                song.AlbumId = albumId;

            var maxSongId = GetMax<Song>(albumId) + 1;
            song.Id = maxSongId;

            song.DateAdded = DateTime.Now;
            song.DateModified = DateTime.Now;

            if (this.Database.Insert<Song>(song))
                return maxSongId;

            return int.MinValue;
        }

        public List<int> AddSong(int albumId, IEnumerable<Song> songs)
        {
            List<int> result = new List<int>();
            foreach (var song in songs)
            {
                var songResult = AddSong(albumId, song);
                result.Add(songResult);
            }
            return result;
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
            var sql = @"DELETE FROM [testdb].[dbo].[Albums] WHERE Id = @albumid;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("albumid", albumId);
            return this.Database.Execute(new List<SqlCommand> { new SqlCommand(sql, parameters) });
        }

        public bool DeleteSong(int albumId)
        {
            var sql = @"DELETE FROM [testdb].[dbo].[Songs] WHERE AlbumId = @albumid";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("albumid", albumId);
            return this.Database.Execute(new List<SqlCommand> { new SqlCommand(sql, parameters) });
        }

        public bool DeleteSong(Album album, Song song)
        {
            return DeleteSong(album.Id, song);
        }

        public bool DeleteSong(int albumId, Song song)
        {
            return DeleteSong(albumId, song.Id);
        }

        public bool DeleteSong(int albumId, int songId)
        {
            var sql = @"DELETE FROM [testdb].[dbo].[Songs] WHERE Id = @songid AND AlbumId = @albumid;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.AddParameter("songid", songId);
            parameters.AddParameter("albumid", albumId);
            return this.Database.Execute(new List<SqlCommand> { new SqlCommand(sql, parameters) });
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Entities.Entity.Songs;

namespace Test.Data.Repositories
{
    public interface IMusicRepository
    {
        int GetMax<T>(int albumId = 0);
        bool AlbumExists(int albumId);
        bool SongExists(int albumId, int songId);
        bool SongExists(int albumId);

        IEnumerable<string> GetArtistNames();

        Album GetAlbum(int albumId);
        IEnumerable<Album> GetAlbum(string artistName);
        IEnumerable<int> GetAlbumIds(string artistName);
        IEnumerable<Album> GetAlbum();
        IEnumerable<int> GetAlbumIds();
        Song GetSong(int albumId, int songId);
        IEnumerable<Song> GetSong(int albumId);
        IEnumerable<Song> GetSong(string artistName);

        bool UpdateAlbum(Album album);
        bool UpdateSong(Album album, Song song);
        bool UpdateSong(int albumId, Song song);

        int AddAlbum(Album album);
        int AddSong(Album album, Song song);
        int AddSong(int albumId, Song song);
        List<int> AddSong(int albumId, IEnumerable<Song> songs);
        List<int> AddSong(Album album, IEnumerable<Song> songs);

        bool DeleteAlbum(Album album);
        bool DeleteAlbum(int albumId);
        bool DeleteSong(int albumId);
        bool DeleteSong(Album album, Song song);
        bool DeleteSong(int albumId, Song song);
        bool DeleteSong(int albumId, int songId);
    }
}

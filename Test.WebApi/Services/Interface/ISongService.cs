using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WebApi.Models;

namespace Test.WebApi.Services
{
    public interface ISongService
    {
        SongModel GetSong(int albumId, int id);
        IEnumerable<SongModel> GetSong(int albumId);
        IEnumerable<SongModel> GetSong(string artistName);
        int AddSong(int albumId, SongModel songModel);
        bool UpdateSong(int albumId, SongModel songModel);
        bool DeleteSong(int albumId);
        bool DeleteSong(int albumId, int songId);
        bool DeleteSong(int albumId, SongModel songModel);
    }
}

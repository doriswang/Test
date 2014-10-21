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
        RequestResult<SongModel> AddSong(int albumId, SongModel songModel);
        RequestResult<SongModel> UpdateSong(int albumId, SongModel songModel);
        RequestResult<SongModel> DeleteSong(int albumId);
        RequestResult<SongModel> DeleteSong(int albumId, int songId);
        RequestResult<SongModel> DeleteSong(int albumId, SongModel songModel);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.WebApi.Models;

namespace Test.WebApi.Services
{
    public interface IAlbumService
    {
        AlbumModel GetAlbum(int id);
        IEnumerable<AlbumModel> GetAlbum(string artistName);
        IEnumerable<AlbumModel> GetAlbum(AlbumInputModel inputModel);
        RequestResult<AlbumModel> AddAlbum(AlbumModel albumModel);
        RequestResult<AlbumModel> UpdateAlbum(AlbumModel albumModel);
        RequestResult<AlbumModel> DeleteAlbum(int albumId);
        RequestResult<AlbumModel> DeleteAlbum(AlbumModel albumModel);

        bool HasSongs(int albumId);
        bool HasSongs(AlbumModel albumModel);
    }
}
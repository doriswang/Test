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
        int AddAlbum(AlbumModel albumModel);
        bool UpdateAlbum(AlbumModel albumModel);
        bool DeleteAlbum(AlbumModel albumModel);

        bool HasSongs(int albumId);
        bool HasSongs(AlbumModel albumModel);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.WebApi.Models;
using Test.Framework.Extensions;

namespace Test.WebApi.Controllers
{
    public class SongController : BaseApiController
    {
        // GET: api/music/song/5
        public IHttpActionResult Get(int albumId)
        {
            if (albumId == 0)
                return BadRequest();

            var songs = songService.GetSong(albumId);

            if (!songs.IsNotNullOrEmpty())
                return InternalServerError();

            return Ok<IEnumerable<SongModel>>(songs);
        }

        // GET: api/music/album/5/song/2
        public IHttpActionResult Get(int albumId, int id)
        {
            if (albumId == 0 ||
                id == 0)
                return BadRequest();

            var song = songService.GetSong(albumId, id);

            if (song == null)
                return InternalServerError();

            return Ok<SongModel>(song);
        }

        // POST: api/music/album/5/song
        public IHttpActionResult Post(int albumId, SongModel songModel)
        {
            if (songModel == null ||
                (songModel.AlbumId == 0 && albumId == 0))
                return BadRequest();

            if (songModel.AlbumId == 0)
                songModel.AlbumId = albumId;

            var result = songService.AddSong(songModel.AlbumId, songModel);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            if (result.Model != null)
                return Ok<SongModel>(result.Model);

            return Ok();
        }

        // PUT: api/music/album/5/song/5
        public IHttpActionResult Put(int albumId, SongModel songModel, int id = 0)
        {
            if (albumId == 0 ||
                songModel == null ||
                (songModel.Id == 0 && id == 0))
                return BadRequest();

            if (songModel.Id == 0)
                songModel.Id = id;

            var result = songService.UpdateSong(albumId, songModel);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            if (result.Model != null)
                return Ok<SongModel>(result.Model);

            return Ok(); ;
        }

        // DELETE: api/music/album/5/song
        public IHttpActionResult Delete(int albumId)
        {
            if (albumId == 0)
                return BadRequest();

            var result = songService.DeleteSong(albumId);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            if (result.Model != null)
                return Ok<SongModel>(result.Model);

            return Ok();
        }

        // DELETE: api/music/album/5/song/1
        public IHttpActionResult Delete(int albumId, int id)
        {
            if (albumId == 0 ||
                id == 0)
                return BadRequest();

            var result = songService.DeleteSong(albumId, id);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            return Ok();
        }
    }
}

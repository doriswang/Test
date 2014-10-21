using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.Entities.Entity.Songs;
using Test.Framework.Extensions;
using Test.WebApi.Models;

namespace Test.WebApi.Controllers
{
    public class AlbumController : BaseApiController
    {
        // GET: api/music/album&&artistName=Passion Pit
        public IHttpActionResult Get([FromUri] AlbumInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (inputModel.id != 0)
                return Get(inputModel.id);

            var result = albumService.GetAlbum(inputModel);

            return Ok<IEnumerable<AlbumModel>>(result);
        }

        // GET: api/music/album/5
        public IHttpActionResult Get(int id)
        {
            if (id == 0)
                return BadRequest();

            var album = albumService.GetAlbum(id);

            if (album == null)
                return NotFound();

            return Ok<AlbumModel>(album);
        }

        // POST: api/music/album
        public IHttpActionResult Post(AlbumModel album)
        {
            if (album == null ||
                album.Title.IsNullOrEmpty() ||
                album.ArtistName.IsNullOrEmpty())
                return BadRequest();

            var result = albumService.AddAlbum(album);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            if (result.Model != null)
                return Ok<AlbumModel>(result.Model);

            return Ok();
        }

        // PUT: api/music/album
        public IHttpActionResult Put(AlbumModel album, int id = 0)
        {
            if (album == null ||
                (album.Id == 0 && id == 0) ||
                (album.ArtistName.IsNullOrEmpty() && album.Title.IsNullOrEmpty()))
                return BadRequest();

            if (album.Id == 0)
                album.Id = id;

            var result = albumService.UpdateAlbum(album);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            if (result.Model != null)
                return Ok<AlbumModel>(result.Model);

            return Ok();
        }

        // DELETE: api/music/album/5
        public IHttpActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var result = albumService.DeleteAlbum(id);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.Framework.Extensions;
using Test.WebApi.Models;

namespace Test.WebApi.Controllers
{
    public class SongsController : BaseApiController
    {
        // POST: api/music/albums/5/songs
        public IHttpActionResult Post(int albumId, IEnumerable<SongModel> songModels)
        {
            if (albumId == 0 ||
                !songModels.IsNotNullOrEmpty())
                return BadRequest();

            var result = 0;

            foreach (var song in songModels)
            {
                result = songService.AddSong(albumId, song);
            }

            if (result == 0)
                return InternalServerError();

            return Ok();
        }
    }
}

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

            List<RequestResult<SongModel>> result = new List<RequestResult<SongModel>>();
            foreach (var song in songModels)
            {
                result.Add(songService.AddSong(albumId, song));
            }

            if (result.Where(x => x.InternalServerError == true).Count() > 0)
                return InternalServerError();

            if (result.Where(x => x.Status != HttpStatusCode.OK).Count() > 0)
                return InternalServerError();

            var newResult = result.Where(x => x.Model != null).Select(x => x.Model).ToList();

            return Ok<List<SongModel>>(newResult);
        }
    }
}

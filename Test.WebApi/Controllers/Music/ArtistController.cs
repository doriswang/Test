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
    public class ArtistController : BaseApiController
    {
        // GET: api/music/artist/Passion Pit
        public IHttpActionResult Get(string name)
        {
            if (name.IsNullOrEmpty())
                return BadRequest();

            var artist = artistService.GetArtist(name);

            if (artist == null)
                return NotFound();

            return Ok<ArtistModel>(artist);
        }

        // POST: api/music/artist
        public IHttpActionResult Post(ArtistModel artistModel)
        {
            if (artistModel == null ||
                artistModel.Name.IsNullOrEmpty() ||
                artistModel.Albums.IsNullOrEmpty())
                return BadRequest();

            var result = artistService.AddArtist(artistModel);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            if (result.Model != null)
                return Ok<ArtistModel>(result.Model);

            return Ok();
        }

        // PUT: api/music/artist
        public IHttpActionResult Put(ArtistModel artistModel)
        {
            if (artistModel == null ||
                artistModel.Name.IsNullOrEmpty())
                return BadRequest();

            var result = artistService.UpdateArtist(artistModel);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            if (result.Model != null)
                return Ok<ArtistModel>(result.Model);

            return Ok();
        }

        // DELETE: api/music/artist/Passion Pit
        public IHttpActionResult Delete(string name)
        {
            if (name.IsNullOrEmpty())
                return BadRequest();

            var result = artistService.DeleteArtist(name);

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return (IHttpActionResult)new HttpResponseMessage(result.Status);

            return Ok();
        }
    }
}

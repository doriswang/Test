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
    public class MusicController : BaseApiController
    {
        // GET: api/music
        public IHttpActionResult Get()
        {
            var result = this.musicService.GetMusic();

            if (result.InternalServerError)
                return InternalServerError();

            if (result.Status != HttpStatusCode.OK)
                return ResponseMessage(new HttpResponseMessage(result.Status));

            if (result.Model == null || result.Model.Artists.IsNullOrEmpty())
                return NotFound();

            return Ok<MusicModel>(result.Model);
        }
    }
}

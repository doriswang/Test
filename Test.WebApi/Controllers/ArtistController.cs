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
        public IHttpActionResult Get(string name)
        {
            if (name.IsNullOrEmpty())
                return BadRequest();

            var artist = artistService.Get(name);

            if (artist == null)
                return NotFound();

            return Ok<ArtistModel>(artist);
        }
    }
}

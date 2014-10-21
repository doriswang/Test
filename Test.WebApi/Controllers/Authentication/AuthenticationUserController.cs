using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Test.WebApi.Controllers
{
    public class AuthenticationUserController : ApiController
    {
        public IHttpActionResult Get()
        {
            return NotFound();
        }

        public IHttpActionResult Get(string id)
        {
            return NotFound();
        }
    }
}

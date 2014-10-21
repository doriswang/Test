using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.WebApi.Models;

namespace Test.WebApi.Services
{
    public interface IArtistService
    {
        ArtistModel Get(string name);
    }
}
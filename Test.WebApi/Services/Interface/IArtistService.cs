using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.WebApi.Models;

namespace Test.WebApi.Services
{
    public interface IArtistService
    {
        ArtistModel GetArtist(string name);
        RequestResult<ArtistModel> AddArtist(ArtistModel artistModel);
        RequestResult<ArtistModel> UpdateArtist(ArtistModel artistModel);
        RequestResult<ArtistModel> DeleteArtist(string name);
    }
}
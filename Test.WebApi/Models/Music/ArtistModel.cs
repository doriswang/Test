using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Entities;
using Test.Framework.Handlers.Resource;

namespace Test.WebApi.Models
{
    public class ArtistModel : Resource, IDataModel
    {
        public string Name { get; set; }
        public List<AlbumModel> Albums { get; set; }
    }
}
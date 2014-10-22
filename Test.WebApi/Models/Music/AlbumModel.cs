using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Test.Entities;
using Test.Framework.Handlers.Resource;

namespace Test.WebApi.Models
{
    public class AlbumModel : Resource, IDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SongModel> Songs { get; set; }
    }
}
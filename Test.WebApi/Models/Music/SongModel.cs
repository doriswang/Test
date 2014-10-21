using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Entities;
using Test.Framework.Handlers.Resource;

namespace Test.WebApi.Models
{
    public class SongModel : Resource, IDataModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int AlbumId { get; set; }

        public string Title { get; set; }
        public string Length { get; set; }
        public int TrackNumber { get; set; }
        public string Genre { get; set; }

        [JsonIgnore]
        public DateTime DateAdded { get; set; }

        [JsonIgnore]
        public DateTime DateModified { get; set; }
    }
}
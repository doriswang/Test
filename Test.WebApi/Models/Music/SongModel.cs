using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;
using Test.Entities;
using Test.Framework.Handlers.Resource;
using Test.Framework.Extensions;

namespace Test.WebApi.Models
{
    [XmlRoot("song")]
    [DataContract]
    public class SongModel : Resource, IDataModel
    {
        [DataMember]
        [XmlAttribute("SongId")]
        public int Id { get; set; }

        [XmlIgnore]
        [DataMember]
        public int AlbumId { get; set; }

        [DataMember]
        [XmlAttribute("title")]
        public string Title { get; set; }

        private string _Length = string.Empty;
        [DataMember]
        [XmlAttribute("length")]
        public string Length 
        {
            get 
            {
                return _Length.IsNotNullOrEmpty() ? _Length.Trim() : string.Empty;
            }
            set { _Length = value; }
        }

        [XmlIgnore]
        [DataMember]
        public int TrackNumber { get; set; }

        [XmlIgnore]
        [DataMember]
        public string Genre { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public DateTime DateAdded { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public DateTime DateModified { get; set; }
    }
}
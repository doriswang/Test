using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Test.Entities;
using Test.Framework.Handlers.Resource;

namespace Test.WebApi.Models
{
    [XmlRoot("album")]
    [DataContract(IsReference=true)]
    public class AlbumModel : Resource, IDataModel
    {
        [XmlAttribute("Id")]
        [DataMember]
        public int Id { get; set; }

        [XmlAttribute("title")]
        [DataMember]
        public string Title { get; set; }

        [XmlIgnore]
        [DataMember]
        public string ArtistName { get; set; }

        [XmlElement(ElementName="song")]
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SongModel> Songs { get; set; }
    }
}
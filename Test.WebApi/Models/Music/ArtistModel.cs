using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;
using Test.Entities;
using Test.Framework.Handlers.Resource;

namespace Test.WebApi.Models
{
    [DataContract(IsReference=true)]
    [XmlRoot("artist")]
    public class ArtistModel : Resource, IDataModel
    {
        [DataMember]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement(ElementName="album")]
        [DataMember]
        public List<AlbumModel> Albums { get; set; }
    }
}
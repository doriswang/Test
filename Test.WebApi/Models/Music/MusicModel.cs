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
    [DataContract(IsReference = true)]
    [XmlRoot("music")]
    public class MusicModel : Resource, IDataModel
    {
        [XmlElement(ElementName = "artist")]
        [DataMember]
        public List<ArtistModel> Artists { get; set; }
    }
}
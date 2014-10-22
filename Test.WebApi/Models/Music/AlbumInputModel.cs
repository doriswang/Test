using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Test.WebApi.Models
{
    [DataContract]
    public class AlbumInputModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string artistName { get; set; }
    }
}
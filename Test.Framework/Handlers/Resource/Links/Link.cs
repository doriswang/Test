using System.Runtime.Serialization;
using Test.Framework.Validation;
using Newtonsoft.Json;

namespace Test.Framework.Handlers.Resource
{
    /// <summary>
    /// A base class for relation links
    /// </summary>
    [DataContract]
    public class Link
    {
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        [DataMember]
        public string Rel { get; private set; }
        
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        [DataMember]
        public string Href { get; private set; }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        [DataMember]
        public string Method { get; private set; }
        
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        [DataMember]
        public string Title { get; private set; }

        public Link(string rel, string href, string method = "GET", string title = null)
        {
            Ensure.Argument.NotNullOrEmpty(rel, "rel");
            Ensure.Argument.NotNullOrEmpty(href, "href");
            Ensure.Argument.NotNullOrEmpty(method, "method");

            Rel = rel;
            Href = href;
            Method = method;
            Title = title;
        }

        public override string ToString()
        {
            return Href;
        }
    }
}

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PommaLabs.Diagnostics;

namespace Finsa.Caravan.Common.Models.Linking
{
    /// <summary>
    ///   A base class for relation links. See here: https://www.iana.org/assignments/link-relations/link-relations.xhtml.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), DataContract]
    public class Link
    {
        #region HTTP Methods

        public const string HttpDeleteMethod = "DELETE";
        public const string HttpGetMethod = "GET";
        public const string HttpPostMethod = "POST";

        #endregion HTTP Methods

        [JsonProperty("rel"), DataMember(Name = "rel")]
        public string Rel { get; private set; }

        [JsonProperty("href"), DataMember(Name = "href")]
        public string Href { get; private set; }

        [JsonProperty("method"), DataMember(Name = "method")]
        public string Method { get; private set; }

        [JsonProperty("title"), DataMember(Name = "title")]
        public string Title { get; private set; }

        public Link(string rel, string href, string method, string title = null)
        {
            Raise<ArgumentException>.IfIsEmpty(rel);
            Raise<ArgumentException>.IfIsEmpty(href);
            Raise<ArgumentException>.IfIsEmpty(method);

            Rel = rel;
            Href = href;
            Method = method.ToUpper();
            Title = title;
        }

        public override string ToString()
        {
            return Href;
        }
    }
}
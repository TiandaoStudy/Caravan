using Finsa.Caravan.Common.Utilities;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Linking
{
    /// <summary>
    ///   A base class for relation links. See here: https://www.iana.org/assignments/link-relations/link-relations.xhtml.
    /// </summary>
    [Serializable, JsonObject(MemberSerialization.OptIn), DataContract]
    public class Link : FormattableObject
    {
        #region HTTP Methods

        public const string HttpDeleteMethod = "DELETE";
        public const string HttpGetMethod = "GET";
        public const string HttpPostMethod = "POST";

        #endregion HTTP Methods

        public Link(string rel, string href, string method, string title = null)
        {
            Raise<ArgumentException>.IfIsEmpty(rel);
            Raise<ArgumentException>.IfIsEmpty(href);
            Raise<ArgumentException>.IfIsEmpty(method);
            Raise<ArgumentException>.If(title != null && String.IsNullOrWhiteSpace(title));

            Rel = rel;
            Title = title;
            Href = href;
            Method = method.ToUpper();
        }

        [JsonProperty("rel", Order = 0), DataMember(Name = "rel", Order = 0)]
        public string Rel { get; private set; }

        [JsonProperty("title", Order = 1), DataMember(Name = "title", Order = 1)]
        public string Title { get; private set; }

        [JsonProperty("href", Order = 2), DataMember(Name = "href", Order = 2)]
        public string Href { get; private set; }

        [JsonProperty("method", Order = 3), DataMember(Name = "method", Order = 3)]
        public string Method { get; private set; }

        protected override IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers()
        {
            yield return GKeyValuePair.Create("Rel", Rel);
            if (!String.IsNullOrWhiteSpace(Title))
            {
                yield return GKeyValuePair.Create("Title", Title);
            }
            yield return GKeyValuePair.Create("Href", Href);
            yield return GKeyValuePair.Create("Method", Method);
        }
    }
}
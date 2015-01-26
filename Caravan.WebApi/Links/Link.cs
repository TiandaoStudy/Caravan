using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PommaLabs.Diagnostics;

namespace Finsa.Caravan.Mvc.Core.Links
{
    /// <summary>
    /// A base class for relation links
    /// </summary>
    [JsonObject, DataContract]
    public class Link
    {
        [DataMember]
        public string Rel { get; private set; }

        [DataMember]
        public string Href { get; private set; }

        [DataMember]
        public string Title { get; private set; }

        public Link(string rel, string href, string title = null)
        {
            Raise<ArgumentException>.IfIsEmpty(rel);
            Raise<ArgumentException>.IfIsEmpty(href);

            Rel = rel;
            Href = href;
            Title = title;
        }

        public override string ToString()
        {
            return Href;
        }
    }
}
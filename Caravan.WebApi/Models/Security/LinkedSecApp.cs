using AutoMapper;
using Finsa.Caravan.Common.DataModel.Links;
using Finsa.Caravan.Common.Models.Linking;
using Finsa.Caravan.Common.Models.Security;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Web.Http.Routing;

namespace Finsa.Caravan.WebApi.Models.Security
{
    [Serializable, DataContract(IsReference = true)]
    public sealed class LinkedSecApp : LinkedObject
    {
        public LinkedSecApp(SecApp secApp, UrlHelper url)
        {
            Mapper.Map(secApp, this);
            Links.AddLink(new SelfLink(url.Link("GetApps", new { }), Link.HttpGetMethod));
        }

        #region SecApp Properties

        [JsonProperty, DataMember]
        public long Id { get; set; }

        [JsonProperty, DataMember]
        public string Name { get; set; }

        [JsonProperty, DataMember]
        public string Description { get; set; }

        #endregion SecApp Properties
    }
}
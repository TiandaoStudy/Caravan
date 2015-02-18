using System;
using System.Runtime.Serialization;
using System.Web.Http.Routing;
using AutoMapper;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.Common.DataModel.Links;
using Newtonsoft.Json;
using ApiLinks = Finsa.Caravan.Common.DataModel.Links.Links;

namespace Finsa.Caravan.Mvc.Core.Models.Security
{
    [Serializable, DataContract(IsReference = true)]
    public sealed class LinkedSecApp : LinkedObject
    {
        public LinkedSecApp(SecApp secApp, UrlHelper url)
        {
            Mapper.Map(secApp, this);
            Links.AddLink(new SelfLink(url.Link("GetApps", new {})));
        }

        #region SecApp Properties

        [JsonProperty, DataMember]
        public long Id { get; set; }

        [JsonProperty, DataMember]
        public string Name { get; set; }

        [JsonProperty, DataMember]
        public string Description { get; set; }

        #endregion
    }
}
﻿using System;
using System.Runtime.Serialization;
using System.Web.Http.Routing;
using AutoMapper;
using Finsa.Caravan.Common.Models.Linking;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Security;
using Newtonsoft.Json;

namespace Finsa.Caravan.WebApi.Models.Security
{
    [Serializable, DataContract]
    public sealed class LinkedSecApp : LinkedObject
    {
        public LinkedSecApp(SecApp secApp, UrlHelper url)
        {
            Mapper.Map(secApp, this);
            Links.AddLink(new SelfLink(url.Link("GetApps", new { }), Link.HttpGetMethod));
        }

        #region SecApp Properties

        [JsonProperty(Order = 0), DataMember(Order = 0)]
        public string Name { get; set; }

        [JsonProperty(Order = 1), DataMember(Order = 1)]
        public string Description { get; set; }

        [JsonProperty(Order = 2), DataMember(Order = 2)]
        public SecUser[] Users { get; set; }

        [JsonProperty(Order = 3), DataMember(Order = 3)]
        public SecGroup[] Groups { get; set; }

        [JsonProperty(Order = 4), DataMember(Order = 4)]
        public SecContext[] Contexts { get; set; }

        [JsonProperty(Order = 5), DataMember(Order = 5)]
        public LogSetting[] LogSettings { get; set; }

        #endregion SecApp Properties
    }
}
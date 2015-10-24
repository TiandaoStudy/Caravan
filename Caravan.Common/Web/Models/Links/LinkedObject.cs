using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Models.Linking
{
    [Serializable, DataContract]
    public abstract class LinkedObject
    {
        private readonly Links _links = new Links();

        [JsonProperty(Links.PropertyName, Order = Links.PropertyOrder)]
        public Links Links
        {
            get { return _links; }
        }
    }
}
using Newtonsoft.Json;
using ApiLinks = Finsa.Caravan.Common.DataModel.Links.Links;

namespace Finsa.Caravan.Common.DataModel.Links
{
    public abstract class LinkedObject
    {
        private readonly ApiLinks _links = new ApiLinks();

        [JsonProperty(ApiLinks.JsonPropertyName, Order = ApiLinks.JsonPropertyOrder)]
        public ApiLinks Links
        {
            get { return _links; }
        }
    }
}
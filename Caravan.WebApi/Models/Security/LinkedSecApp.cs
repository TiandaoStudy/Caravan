using Finsa.Caravan.Common.DataModel.Security;
using Newtonsoft.Json;
using ApiLinks = Finsa.Caravan.Mvc.Core.Links.Links;

namespace Finsa.Caravan.Mvc.Core.Models.Security
{
    public class LinkedSecApp : SecApp
    {
        private readonly ApiLinks _links = new ApiLinks();

        [JsonProperty(ApiLinks.JsonPropertyName, Order = ApiLinks.JsonPropertyOrder)]
        public ApiLinks Links
        {
            get { return _links; }
        }
    }
}
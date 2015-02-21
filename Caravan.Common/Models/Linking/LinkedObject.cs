using Finsa.Caravan.Common.DataModel.Links;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.Models.Linking
{
    public abstract class LinkedObject
    {
        private readonly Links _links = new Links();

        [JsonProperty(Links.JsonPropertyName, Order = Links.JsonPropertyOrder)]
        public Links Links
        {
            get { return _links; }
        }
    }
}
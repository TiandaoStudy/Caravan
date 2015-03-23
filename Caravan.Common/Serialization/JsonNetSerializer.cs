using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Finsa.Caravan.Common.Serialization
{
    internal sealed class JsonNetSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            TypeNameHandling = TypeNameHandling.Objects,
        };

        public string SerializeObject<TObj>(TObj obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSerializerSettings);
        }

        public TObj DeserializeObject<TObj>(string json)
        {
            return JsonConvert.DeserializeObject<TObj>(json, JsonSerializerSettings);
        }
    }
}
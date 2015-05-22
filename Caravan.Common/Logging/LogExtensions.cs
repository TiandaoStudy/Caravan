using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Finsa.CodeServices.Serialization.JsonSerializer;
using JsonSerializerSettings = Finsa.CodeServices.Serialization.JsonSerializerSettings;

namespace Finsa.Caravan.Common.Logging
{
    public static class LogExtensions
    {
        /// <summary>
        ///   The cached JSON serializer, configured in order to produce a very compact output.
        /// </summary>
        private static readonly JsonSerializer CachedJsonSerializer = new JsonSerializer(new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.None,
        });

        /// <summary>
        ///   Converts given object in a very compact JSON format. If given object is null, an empty
        ///   string is returned.
        /// </summary>
        /// <typeparam name="TObj">The type of the object. Used to avoid boxing.</typeparam>
        /// <param name="obj">The object that should be converted.</param>
        /// <returns>A very compact JSON corresponding to given object.</returns>
        public static string LogAsJson<TObj>(this TObj obj)
        {
            return ReferenceEquals(obj, null) ? String.Empty : CachedJsonSerializer.SerializeToString(obj);
        }
    }
}

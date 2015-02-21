using System.Web.Http;
using AutoMapper;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebApi.Models.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PommaLabs.KVLite;
using PommaLabs.KVLite.Web.Http;

namespace Finsa.Caravan.WebApi
{
    /// <summary>
    ///   Gestisce alcuni eventi comuni a tutte le applicazioni.
    /// </summary>
    public sealed class ServiceHelper
    {
        public static void OnStart(HttpConfiguration configuration)
        {
            // Personalizzo le impostazioni del serializzatore JSON.
            configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Mapping per le risorse dotate di link.
            Mapper.CreateMap<SecApp, LinkedSecApp>();

            // Loggo l'avvio dell'applicazione.
            Db.Logger.LogInfoAsync<ServiceHelper>("Application started");

            // Run vacuum on the persistent cache. It should be put AFTER the connection string is
            // set, since that string it stored on the cache itself and we do not want conflicts, right?
            if (Cache.Instance is PersistentCache)
            {
                PersistentCache.DefaultInstance.VacuumAsync();
            }

            // Imposta KVLite come gestore della cache di output.
            ApiOutputCache.RegisterAsCacheOutputProvider(configuration);
        }
    }
}
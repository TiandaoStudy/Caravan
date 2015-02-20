using System.Web.Http;
using AutoMapper;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebApi.Models.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PommaLabs.KVLite;

namespace Finsa.Caravan.WebApi
{
    public sealed class GlobalHelper
    {
        public static void Application_Start()
        {
            // Personalizzo le impostazioni del serializzatore JSON.
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
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
            Db.Logger.LogInfoAsync<GlobalHelper>("Application started");

            // Run vacuum on the persistent cache. It should be put AFTER the connection string is
            // set, since that string it stored on the cache itself and we do not want conflicts, right?
            if (Cache.Instance is PersistentCache)
            {
                PersistentCache.DefaultInstance.VacuumAsync();
            }
        }
    }
}
using System.Web.Http;
using Common.Logging;
using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess;
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
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Personalizzo le impostazioni del serializzatore XML.
            var xml = configuration.Formatters.XmlFormatter;
            xml.Indent = false;

            // Loggo l'avvio dell'applicazione.
            Db.Logger.LogInfoAsync<ServiceHelper>("Application started");

            // Run vacuum on the persistent cache. It should be put AFTER the connection string is
            // set, since that string it stored on the cache itself and we do not want conflicts, right?
            var persistentCache = Cache.Instance as PersistentCache;
            if (persistentCache != null)
            {
                persistentCache.VacuumAsync();
            }

            // Imposta KVLite come gestore della cache di output.
            ApiOutputCache.RegisterAsCacheOutputProvider(configuration, Cache.Instance);

            // Registra l'handler che si occupa del logging.
            var log = LogManager.GetLogger<LogRequestAndResponseHandler>();
            log.Debug("PING");
            configuration.MessageHandlers.Add(new LogRequestAndResponseHandler(log));
        }
    }
}
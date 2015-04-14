using Common.Logging;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PommaLabs.KVLite;
using PommaLabs.KVLite.Web.Http;
using System.Web.Http;

namespace Finsa.Caravan.WebApi
{
    /// <summary>
    ///   Gestisce alcuni eventi comuni a tutte le applicazioni.
    /// </summary>
    public sealed class ServiceHelper
    {
        public static void OnStart(HttpConfiguration configuration, ILogManager logManager)
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
            var serviceHelperLog = logManager.GetLogger<ServiceHelper>();
            serviceHelperLog.Info(m => m("Application {0} started", Common.Properties.Settings.Default.ApplicationDescription));

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
            var requestAndResponseLog = logManager.GetLogger<LogRequestAndResponseHandler>() as ICaravanLog;
            configuration.MessageHandlers.Add(new LogRequestAndResponseHandler(requestAndResponseLog));
        }
    }
}
using Common.Logging;
using Finsa.Caravan.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PommaLabs.KVLite;
using PommaLabs.KVLite.Web.Http;
using System;
using System.Web.Http;
using Finsa.CodeServices.Common.Diagnostics;

namespace Finsa.Caravan.WebApi
{
    /// <summary>
    ///   Gestisce alcuni eventi comuni a tutte le applicazioni.
    /// </summary>
    public sealed class ServiceHelper
    {
        public static void OnStart(ILog log, ICache cache)
        {
            // Controlli di integrità.
            Raise<ArgumentNullException>.IfIsNull(log);

            // Loggo l'avvio dell'applicazione.
            log.InfoFormat("Application {0} started", CommonConfiguration.Instance.AppName);

            // Run vacuum on the persistent cache. It should be put AFTER the connection string is
            // set, since that string it stored on the cache itself and we do not want conflicts, right?
            var persistentCache = cache as PersistentCache;
            if (persistentCache != null)
            {
                persistentCache.VacuumAsync();
            }
        }

        public static void ConfigureFormatters(HttpConfiguration configuration)
        {
            // Controlli di integrità.
            Raise<ArgumentNullException>.IfIsNull(configuration);

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
        }

        public static void ConfigureOutputCache(HttpConfiguration configuration, ICache cache)
        {
            // Controlli di integrità.
            Raise<ArgumentNullException>.IfIsNull(configuration);
            Raise<ArgumentNullException>.IfIsNull(cache);

            // Imposta KVLite come gestore della cache di output.
            ApiOutputCache.RegisterAsCacheOutputProvider(configuration, cache);
        }
    }
}
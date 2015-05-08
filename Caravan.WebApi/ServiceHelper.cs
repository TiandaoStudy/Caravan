using System.Net.Http;
using System.Web.Http.Dispatcher;
using Common.Logging;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Finsa.Caravan.DataAccess.Drivers.Sql;
using Finsa.Caravan.WebApi.DelegatingHandlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PommaLabs.KVLite;
using PommaLabs.KVLite.Web.Http;
using System;
using System.Web.Http;

namespace Finsa.Caravan.WebApi
{
    /// <summary>
    ///   Gestisce alcuni eventi comuni a tutte le applicazioni.
    /// </summary>
    public sealed class ServiceHelper
    {
        public static void OnStart(ILog log)
        {
            // Controlli di integrità.
            Raise<ArgumentNullException>.IfIsNull(log);

            // Loggo l'avvio dell'applicazione.
            log.InfoFormat("Application {0} started", Common.Properties.Settings.Default.ApplicationDescription);

            // Run vacuum on the persistent cache. It should be put AFTER the connection string is
            // set, since that string it stored on the cache itself and we do not want conflicts, right?
            var persistentCache = Cache.Instance as PersistentCache;
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

        public static void ConfigureHandlers(HttpConfiguration configuration, LoggingDelegatingHandler loggingDelegatingHandler)
        {
            // Controlli di integrità.
            Raise<ArgumentNullException>.IfIsNull(configuration);

            if (loggingDelegatingHandler != null)
            {
                configuration.MessageHandlers.Add(loggingDelegatingHandler);
            }
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
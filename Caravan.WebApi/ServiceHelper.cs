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
        public static void OnStart(HttpConfiguration config, ILog log, ICache cache)
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

            // Imposta KVLite come gestore della cache di output.
            ApiOutputCache.RegisterAsCacheOutputProvider(config, cache);
        }
    }
}
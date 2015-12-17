// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.WebApi.Middlewares;
using Ninject;
using Owin;
using PommaLabs.KVLite;
using PommaLabs.KVLite.Web.Http;
using PommaLabs.Thrower;
using System.Threading.Tasks;
using System.Web.Http;

namespace Finsa.Caravan.WebApi
{
    /// <summary>
    ///   Gestisce alcuni eventi comuni a tutte le applicazioni.
    /// </summary>
    public sealed class CaravanWebServiceHelper
    {
        /// <summary>
        ///   Esegue alcune operazioni preliminari all'avvio dell'applicazione.
        /// </summary>
        /// <param name="appBuilder">La configurazione di Owin.</param>
        /// <param name="httpConfiguration">La configurazione HTTP.</param>
        /// <param name="settings">Le impostazioni iniziali per il servizio.</param>
        public static async Task OnStartAsync(IAppBuilder appBuilder, HttpConfiguration httpConfiguration, Settings settings)
        {
            // Controlli di integrità.
            RaiseArgumentNullException.IfIsNull(httpConfiguration, nameof(httpConfiguration));

            // Il kernel Ninject usato per recuperare le dipendenze.
            var kernel = CaravanServiceProvider.NinjectKernel;
            var log = CaravanServiceProvider.FetchLog<CaravanWebServiceHelper>();

            // Loggo l'avvio dell'applicazione.
            log.Info($"Application {CaravanCommonConfiguration.Instance.AppDescription} started");

            // Run vacuum on the persistent cache. It should be put AFTER the connection string is
            // set, since that string it stored on the cache itself and we do not want conflicts, right?
            var cache = kernel.Get<ICache>();
            var persistentCache = cache as PersistentCache;
            if (persistentCache != null)
            {
                await persistentCache.VacuumAsync();
            }

            // Imposta KVLite come gestore della cache di output.
            log.Trace("Registering KVLite output cache");
            ApiOutputCache.RegisterAsCacheOutputProvider(httpConfiguration, cache);

            // Registra i componenti di middleware.
            log.Trace("Registering Owin middlewares");
            if (settings.EnableHttpCompressionMiddleware)
            {
                // Inserire la compressione PRIMA del log.
                appBuilder.Use(kernel.Get<HttpCompressionMiddleware>());
            }
            if (settings.EnableHttpLoggingMiddleware)
            {
                // Inserire il log DOPO la compressione.
                appBuilder.Use(kernel.Get<HttpLoggingMiddleware>());
            }

            // Pulizia dei log più vecchi o che superano una certa soglia di quantità.
            log.Trace("Cleaning up older log entries");
            var logRepository = kernel.Get<ICaravanLogRepository>();
            Task.Run(() => logRepository.CleanUpEntriesAsync(CaravanCommonConfiguration.Instance.AppName));
        }

        /// <summary>
        ///   Le impostazioni iniziali per il servizio.
        /// </summary>
        public sealed class Settings
        {
            /// <summary>
            ///   Abilita il componente Owin che comprime le response.
            /// 
            ///   Abilitato di default.
            /// </summary>
            public bool EnableHttpCompressionMiddleware { get; set; } = true;

            /// <summary>
            ///   Abilita il componente Owin che registra le request e le response.
            /// 
            ///   Abilitato di default.
            /// </summary>
            public bool EnableHttpLoggingMiddleware { get; set; } = true;
        }
    }
}

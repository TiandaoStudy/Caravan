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
using PommaLabs.KVLite.WebApi;
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
        public static void OnStart(IAppBuilder appBuilder, HttpConfiguration httpConfiguration, Settings settings)
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
                persistentCache.Vacuum();
            }

            // Imposta KVLite come gestore della cache di output.
            log.Trace("Registering KVLite output cache");
            OutputCacheProvider.Register(httpConfiguration, () => kernel.Get<ICache>());

            // Registra i componenti di middleware.
            RegisterOwinMiddlewares(appBuilder, settings, log);

            // Pulizia dei log più vecchi o che superano una certa soglia di quantità.
            log.Trace("Cleaning up older log entries");
            var logRepository = kernel.Get<ICaravanLogRepository>();
            Task.Run(() => logRepository.CleanUpEntriesAsync(CaravanCommonConfiguration.Instance.AppName));
        }

        private static void RegisterOwinMiddlewares(IAppBuilder appBuilder, Settings settings, ICaravanLog log)
        {
            log.Trace("Registering Owin middlewares");

            var identifierSettings = settings.HttpRequestIdentifierMiddleware;
            if (identifierSettings.Enabled)
            {
                // Inserire la parte di etichettatura prima di ogni componente.
                var identifierLog = CaravanServiceProvider.FetchLog<HttpRequestIdentifierMiddleware>();
                appBuilder.Use(typeof(HttpRequestIdentifierMiddleware), identifierSettings, identifierLog);
            }

            var compressionSettings = settings.HttpCompressionMiddleware;
            if (compressionSettings.Enabled)
            {
                // Inserire la compressione PRIMA del log.
                var compressionLog = CaravanServiceProvider.FetchLog<HttpCompressionMiddleware>();
                appBuilder.Use(typeof(HttpCompressionMiddleware), compressionSettings, compressionLog);
            }

            var loggingSettings = settings.HttpLoggingMiddleware;
            if (loggingSettings.Enabled)
            {
                // Inserire il log DOPO la compressione.
                var loggingLog = CaravanServiceProvider.FetchLog<HttpLoggingMiddleware>();
                appBuilder.Use(typeof(HttpLoggingMiddleware), loggingSettings, loggingLog);
            }

            var proxySettings = settings.HttpProxyMiddleware;
            if (proxySettings.Enabled)
            {
                // Inserire il proxy DOPO gli altri componenti.
                appBuilder.Map(proxySettings.SourceEndpointPath, ab =>
                {
                    var proxyLog = CaravanServiceProvider.FetchLog<HttpProxyMiddleware>();
                    ab.Use(typeof(HttpProxyMiddleware), proxySettings, proxyLog);
                });
            }
        }

        /// <summary>
        ///   Le impostazioni iniziali per il servizio.
        /// </summary>
        public sealed class Settings
        {
            /// <summary>
            ///   Le impostazioni del componente per la compressione delle response.
            /// </summary>
            public HttpCompressionMiddleware.Settings HttpCompressionMiddleware { get; set; } = new HttpCompressionMiddleware.Settings();

            /// <summary>
            ///   Le impostazioni del componente per la registrazione nel log.
            /// </summary>
            public HttpLoggingMiddleware.Settings HttpLoggingMiddleware { get; set; } = new HttpLoggingMiddleware.Settings();

            /// <summary>
            ///   Le impostazioni del componente di proxy HTTP.
            /// </summary>
            public HttpProxyMiddleware.Settings HttpProxyMiddleware { get; } = new HttpProxyMiddleware.Settings();

            /// <summary>
            ///   Le impostazioni del componente per l'etichettatura delle request.
            /// </summary>
            public HttpRequestIdentifierMiddleware.Settings HttpRequestIdentifierMiddleware { get; } = new HttpRequestIdentifierMiddleware.Settings();
        }
    }
}

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

using Common.Logging;
using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess;
using PommaLabs.KVLite;
using PommaLabs.KVLite.Web.Http;
using PommaLabs.Thrower;
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
        /// <param name="configuration">La configurazione HTTP.</param>
        /// <param name="log">Un'istanza valida del log.</param>
        /// <param name="cache">Un'istanza valida della cache.</param>
        public static async void OnStart(HttpConfiguration configuration, ILog log, ICache cache)
        {
            // Controlli di integrità.
            RaiseArgumentNullException.IfIsNull(configuration, nameof(configuration));
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            RaiseArgumentNullException.IfIsNull(cache, nameof(cache));

            // Loggo l'avvio dell'applicazione.
            log.Info($"Application {CaravanCommonConfiguration.Instance.AppDescription} started");

            // Run vacuum on the persistent cache. It should be put AFTER the connection string is
            // set, since that string it stored on the cache itself and we do not want conflicts, right?
            var persistentCache = cache as PersistentCache;
            if (persistentCache != null)
            {
                await persistentCache.VacuumAsync();
            }

            // Imposta KVLite come gestore della cache di output.
            ApiOutputCache.RegisterAsCacheOutputProvider(configuration, cache);

            // Pulizia dei log più vecchi o che superano una certa soglia di quantità.
            await CaravanDataSource.Logger.CleanUpEntriesAsync(CaravanCommonConfiguration.Instance.AppName);
        }
    }
}

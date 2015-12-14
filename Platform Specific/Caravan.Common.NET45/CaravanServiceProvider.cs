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
using Finsa.Caravan.Common.Logging;
using Finsa.CodeServices.Clock;
using Ninject;
using PommaLabs.KVLite;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Alcuni servizi essenziali usati all'interno di Caravan. Classe introdotta per
    ///   retrocompatibilità verso alcuni componenti che, oramai, non riescono più ad essere
    ///   adattati all'uso di Ninject.
    /// </summary>
    public static class CaravanServiceProvider
    {
        /// <summary>
        ///   Copia locale del kernel Ninject usato per la gestione automatica delle dipendenze.
        /// </summary>
        private static IKernel _ninjectKernel;

        /// <summary>
        ///   Il kernel Ninject usato per la gestione automatica delle dipendenze.
        /// </summary>
        public static IKernel NinjectKernel
        {
            get { return _ninjectKernel; }
            set
            {
                // Aggiorno immediatamente il kernel globale.
                _ninjectKernel = value;

                // Quindi, aggiorno anche le varie dipendenze.
                Clock = value.Get<IClock>();
                MemoryCache = MemoryCache.DefaultInstance;
            }
        }

        #region Services

        /// <summary>
        ///   L'orologio usato di default per il calcolo dell'ora corrente.
        /// </summary>
        public static IClock Clock { get; private set; }

        /// <summary>
        ///   Il log di emergenza, usato quando il normale log di Caravan va in errore. Scrive
        ///   rigorosamente, e semplicemente, su file, per evitare ulteriori errori.
        /// </summary>
        public static ILog EmergencyLog { get; } = LogManager.GetLogger("CaravanEmergencyLog");

        /// <summary>
        ///   La cache in memoria usata da alcuni punti critici di Caravan.
        /// </summary>
        public static MemoryCache MemoryCache { get; private set; }

        #endregion Services

        /// <summary>
        ///   Il log standard di Caravan, che usualmente viene usato per portare i messaggi di log a database.
        /// </summary>
        public static ICaravanLog FetchLog<T>() => LogManager.GetLogger<T>() as ICaravanLog;
    }
}

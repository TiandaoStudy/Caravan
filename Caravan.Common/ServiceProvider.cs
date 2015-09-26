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
using System;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Alcuni servizi essenziali usati all'interno di Caravan. Classe introdotta per
    ///   retrocompatibilità verso alcuni componenti che, oramai, non riescono più ad essere
    ///   adattati all'uso di Ninject.
    /// </summary>
    public static class ServiceProvider
    {
        /// <summary>
        ///   Il kernel Ninject usato per la gestione automatica delle dipendenze.
        /// </summary>
        public static IKernel Kernel { get; } = SetupKernel();

        /// <summary>
        ///   La cache in memoria usata da alcuni punti critici di Caravan.
        /// </summary>
        public static MemoryCache MemoryCache { get; } = MemoryCache.DefaultInstance;

        /// <summary>
        ///   L'orologio usato di default per il calcolo dell'ora corrente.
        /// </summary>
        public static IClock Clock { get; } = Kernel.Get<IClock>();

        /// <summary>
        ///   Il log di emergenza, usato quando il normale log di Caravan va in errore. Scrive
        ///   rigorosamente, e semplicemente, su file, per evitare ulteriori errori.
        /// </summary>
        public static ILog EmergencyLog { get; } = LogManager.GetLogger("CaravanEmergencyLog");

        /// <summary>
        ///   Il log standard di Caravan, che usualmente viene usato per portare i messaggi di log a database.
        /// </summary>
        public static ICaravanLog FetchLog<T>() => LogManager.GetLogger<T>() as ICaravanLog;

        /// <summary>
        ///   Va a cercare i moduli Ninject presenti nelle DLL caricate nell'AppDomain corrente.
        /// </summary>
        /// <returns>Un kernel Ninject propriamente configurato.</returns>
        private static IKernel SetupKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(AppDomain.CurrentDomain.GetAssemblies());
            return kernel;
        }
    }
}

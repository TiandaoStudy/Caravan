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

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   Gestisce l'identità delle variabili di contesto per il log. Viene usata per etichettare i
    ///   "thread" al fine di associare loro le corrette variabili di log, oppure per memorizzare le
    ///   variabili condivisi da tutti i "thread".
    /// </summary>
    /// <remarks>
    ///   Per il log di Caravan, il "thread" è un concetto esteso. Non è detto che sia
    ///   necessariamente un thread .NET, ma potrebbe anche essere un flusso di lavoro (una request
    ///   HTTP, ad esempio).
    /// </remarks>
    public interface ICaravanVariablesContextIdentifier
    {
        /// <summary>
        ///   L'identità globale, condivisa da tutti i "thread".
        /// </summary>
        string GlobalIdentity { get; }

        /// <summary>
        ///   L'identità del "thread" corrente.
        /// </summary>
        string ThreadIdentity { get; }
    }
}

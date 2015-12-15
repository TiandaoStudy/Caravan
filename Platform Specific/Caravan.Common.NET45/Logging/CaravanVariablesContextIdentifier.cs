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

using System.Threading;
using System.Web;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   Implementazione standard per <see cref="ICaravanVariablesContextIdentifier"/>.
    /// </summary>
    /// <remarks>
    ///   Questa classe può essere estesa per gestire casistiche non previste dall'implementazione standard.
    /// 
    ///   Per esempio, vedi differente gestione dell'identità del thread su Workflow Foundation e
    ///   AppFabric, dove il contesto HTTP non è valorizzato, pur essendo in una request.
    /// </remarks>
    public class CaravanVariablesContextIdentifier : ICaravanVariablesContextIdentifier
    {
        private const string CacheKeyForGlobal = "global";
        private const string CacheKeyPrefixForRequest = "request_";
        private const string CacheKeyPrefixForThread = "thread_";

        /// <summary>
        ///   L'identità globale, condivisa da tutti i "thread".
        /// </summary>
        public virtual string GlobalIdentity => CacheKeyForGlobal;

        /// <summary>
        ///   L'identità del "thread" corrente.
        /// </summary>
        public virtual string ThreadIdentity
        {
            get
            {
                // Prima cerco di capire se mi trovo in una request IIS. Se si, non importa più in
                // quale thread io sia, perché IIS può spostare il flusso di esecuzione da un thread
                // all'altro. Perciò, se mi trovo in una request, cerco di usare quella come
                // riferimento per il flusso corrente.
                if (HttpContext.Current != null)
                {
                    try
                    {
                        return CacheKeyPrefixForRequest + HttpContext.Current.Request.GetHashCode();
                    }
                    catch
                    {
                        // Potrei finire qui durante l'Application_Start del Global.asax; infatti,
                        // lì il contesto HTTP è valorizzato ma la request sembra ancora non accessibile.
                        return CacheKeyPrefixForRequest + 0;
                    }
                }

                // Se non mi trovo in una request, allora uso il thread come riferimento per il
                // flusso di lavoro.
                return CacheKeyPrefixForThread + Thread.CurrentThread.Name;
            }
        }
    }
}

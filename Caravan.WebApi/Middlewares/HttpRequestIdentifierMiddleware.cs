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
using Finsa.Caravan.WebApi.Core;
using Finsa.Caravan.WebApi.Middlewares.Models;
using Finsa.CodeServices.Common;
using Microsoft.Owin;
using PommaLabs.Thrower;
using System;
using System.Threading.Tasks;

namespace Finsa.Caravan.WebApi.Middlewares
{
    /// <summary>
    ///   Componente di middleware che si occupa di identificare ciascuna request. Questa
    ///   funzionalità è fondamentale per la parte di log.
    /// </summary>
    public sealed class HttpRequestIdentifierMiddleware : OwinMiddleware
    {
        private readonly Settings _settings;
        private readonly ILog _log;

        /// <summary>
        ///   Inizializza il componente usato per l'etichettatura delle request.
        /// </summary>
        /// <param name="next">Il componente successivo nella catena.</param>
        /// <param name="settings">Le impostazioni del componente.</param>
        /// <param name="log">Il log su cui scrivere eventuali messaggi.</param>
        public HttpRequestIdentifierMiddleware(OwinMiddleware next, Settings settings, ILog log)
            : base(next)
        {
            RaiseArgumentNullException.IfIsNull(settings, nameof(settings));
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _settings = settings;
            _log = log;
        }

        /// <summary>
        ///   Etichetta la richiesta corrente.
        /// </summary>
        /// <param name="context">Il contesto di OWIN.</param>
        /// <returns>Task per proseguire nella catena di middleware.</returns>
        public override async Task Invoke(IOwinContext context)
        {
            // Ad esempio, l'ID è utilizzato per associare request e response nel log.
            var requestId = UniqueIdGenerator.NewBase32("-");
            _log.ThreadVariablesContext.Set(Constants.RequestId, requestId);

            try
            {
                // Aggiungo l'ID della request agli header della response, in modo che sia più
                // facile il rintracciamento dei log su Caravan.
                context.Response.Headers.Append(Constants.RequestIdHeader, requestId);

                // Aggiungo lo stesso ID anche alle variabili di ambiente di OWIN, di modo che
                // l'etichetta sia recuperabile anche nelle altre componenti.
                context.Environment.Add(Constants.RequestIdHeader, requestId);
            }
            catch (Exception ex)
            {
                // Non è un problema se questa operazione fallisce, ma devo comunque registrare lo
                // strano evento.
                _log.Error("Could not add the Caravan request ID header...", ex);
            }

            // Prosegue nella pipeline.
            await Next.Invoke(context);
        }

        /// <summary>
        ///   Impostazioni del componente di middleware.
        /// </summary>
        public sealed class Settings : AbstractMiddlewareSettings
        {
            /// <summary>
            ///   Personalizzazione delle impostazioni di base.
            /// </summary>
            public Settings()
            {
                // Di base, questo componente deve essere abilitato.
                Enabled = true;
            }
        }
    }
}

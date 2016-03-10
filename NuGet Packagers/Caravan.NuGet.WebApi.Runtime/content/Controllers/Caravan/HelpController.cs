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

using Finsa.Caravan.Common.Identity.Models;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebApi.Filters;
using Finsa.CodeServices.Clock;
using PommaLabs.Thrower;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.OutputCache.V2;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Il controller di "help" contiene metodi utili per avere informazioni sul servizio stesso e
    ///   sugli utenti collegati.
    /// </summary>
    /// <remarks>
    ///   Il percorso del servizio può essere modificato a proprio piacimento.
    /// 
    ///   Questo servizio dovrebbe essere esposto pubblicamente, senza particolari autenticazioni.
    /// </remarks>
    [RoutePrefix("")]
    public sealed class HelpController : ApiController
    {
        /// <summary>
        ///   Il gestore della sorgente dati di Caravan.
        /// </summary>
        private readonly ICaravanDataSourceManager _dataSourceManager;

        /// <summary>
        ///   L'orologio di sistema.
        /// </summary>
        private readonly IClock _clock;

        /// <summary>
        ///   Inietta le dipendenze richieste dal controller.
        /// </summary>
        /// <param name="dataSourceManager">Il gestore della sorgente dati di Caravan.</param>
        /// <param name="clock">L'orologio di sistema.</param>
        public HelpController(ICaravanDataSourceManager dataSourceManager, IClock clock)
        {
            RaiseArgumentNullException.IfIsNull(dataSourceManager, nameof(dataSourceManager));
            RaiseArgumentNullException.IfIsNull(clock, nameof(clock));
            _dataSourceManager = dataSourceManager;
            _clock = clock;
        }

        /// <summary>
        ///   Effettua la redirezione alle pagine di "help" di SwaggerUI.
        /// </summary>
        [Route(""), CacheOutput(NoCache = true)]
        public RedirectResult Get()
        {
            var uri = Request.RequestUri.ToString();
            var uriWithoutQuery = uri.Substring(0, uri.Length - Request.RequestUri.Query.Length);
            return Redirect(uriWithoutQuery + "swagger/ui/index");
        }

        /// <summary>
        ///   Restituisce informazioni sul servizio, come la sorgente dati e la versione.
        /// </summary>
        [Route("help/serviceInfo"), CacheOutput(NoCache = true)]
        public ServiceInfoDTO GetServiceInfo()
        {
            // Lettura della versione dell'assembly del servizio.
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            // Lettura delle informazioni sulla macchina server.
            var hostName = Environment.MachineName.ToLowerInvariant();
            var bitness = (IntPtr.Size == 4) ? "x86" : "x64";

            // Lettura delle informazioni sulla sorgente dati.
            var dsName = _dataSourceManager.DataSourceName.ToLowerInvariant();
            var dsKind = _dataSourceManager.DataSourceKind.ToString().ToLowerInvariant();

            return new ServiceInfoDTO
            {
                Version = fvi.FileVersion,
                Bitness = bitness,
                HostName = hostName,
                HostDateTime = _clock.UtcNow,
                DataSourceName = dsName,
                DataSourceKind = dsKind,
                DataSourceDateTime = _dataSourceManager.DataSourceDateTime
            };
        }

        /// <summary>
        ///   Restituisce informazioni sull'utente correntemente autenticato.
        /// </summary>
        [Route("help/userInfo"), CacheOutput(NoCache = true), OAuth2Authorize]
        public SecUser GetUserInfo() => (User as IdnPrincipal).User;

        /// <summary>
        ///   Rappresenta una descrizione sintetica delle informazioni sul servizio.
        /// </summary>
        [DataContract]
        public sealed class ServiceInfoDTO
        {
            /// <summary>
            ///   La versione del servizio, letta direttamente dall'assembly .NET del servizio stesso.
            /// </summary>
            [DataMember(Order = 0)]
            public string Version { get; set; }

            /// <summary>
            ///   L'architettura (x86 oppure x64) del processo che esegue il servizio.
            /// </summary>
            [DataMember(Order = 1)]
            public string Bitness { get; set; }

            /// <summary>
            ///   Il nome di rete del server su cui è ospitato il servizio.
            /// </summary>
            [DataMember(Order = 2)]
            public string HostName { get; set; }

            /// <summary>
            ///   Data e ora UTC del server su cui è ospitato il servizio.
            /// </summary>
            [DataMember(Order = 3)]
            public DateTime HostDateTime { get; set; }

            /// <summary>
            ///   Il nome della sorgente dati a cui è collegato Caravan e che, presumibilmente, è
            ///   condivisa con l'applicativo.
            /// </summary>
            [DataMember(Order = 4)]
            public string DataSourceName { get; set; }

            /// <summary>
            ///   Il tipo della sorgente dati a cui è collegato Caravan e che, presumibilmente, è
            ///   condivisa con l'applicativo.
            /// </summary>
            [DataMember(Order = 5)]
            public string DataSourceKind { get; set; }

            /// <summary>
            ///   Data e ora della sorgente dati a cui è collegato Caravan e che, presumibilmente, è
            ///   condivisa con l'applicativo.
            /// </summary>
            [DataMember(Order = 6)]
            public DateTime DataSourceDateTime { get; set; }
        }
    }
}

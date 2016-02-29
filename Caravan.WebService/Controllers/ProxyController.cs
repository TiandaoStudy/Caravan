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
using Finsa.CodeServices.Serialization;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Troschuetz.Random;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Controller usato per "giocare" con il middleware di proxy.
    /// </summary>
    [RoutePrefix("proxy")]
    public sealed class ProxyController : ApiController
    {
        readonly ILog _log;

        /// <summary>
        ///   Inietta le dipendenze.
        /// </summary>
        /// <param name="log">Il log.</param>
        public ProxyController(ILog log)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _log = log;
        }

        /// <summary>
        ///   Restituisce una lista di tuple casuali.
        /// </summary>
        /// <param name="count">Il numero di tuple richieste.</param>
        /// <returns>Una lista di tuple casuali.</returns>
        [Route("randtuples/{count}")]
        public IEnumerable<Tuple<int, string, bool>> GetRandomTuples(int count)
        {
            var r = new TRandom();
            return Enumerable
                .Range(0, count)
                .Select(i => Tuple.Create(r.Next(), r.NextDouble().ToString(), r.NextBoolean()));
        }

        /// <summary>
        ///   Registra nel log le tuple inviate.
        /// </summary>
        /// <param name="randomTuples">Lista di tuple casuali.</param>
        [Route("randtuples")]
        public void PostRandomTuples(IEnumerable<Tuple<int, string, bool>> randomTuples)
        {
            _log.Info(randomTuples.ToJsonString());
        }

        /// <summary>
        ///   Restituisce una response di errore.
        /// </summary>
        [Route("error")]
        public void GetErrorResponse()
        {
            throw new HttpException(HttpStatusCode.BadRequest, "PROXY ERROR TEST");
        }
    }
}
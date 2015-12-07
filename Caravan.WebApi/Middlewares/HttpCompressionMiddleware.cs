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
using Finsa.CodeServices.Compression;
using Microsoft.Owin;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Finsa.Caravan.WebApi.Middlewares
{
    /// <summary>
    ///   Middleware che si occupa della compressione del contenuto della response, se e solo se la
    ///   request ha dichiarato di accettare una risposta compressa (Accept-Enconding uguale a gzip
    ///   o deflate).
    /// 
    ///   Se la request supporta sia gzip che deflate, allora viene data precedenza a gzip.
    /// </summary>
    public sealed class HttpCompressionMiddleware : IDisposable
    {
        const string GZipEncoding = "gzip";
        const string DeflateEncoding = "deflate";

        static readonly string[] GZipEncodingHeader = { GZipEncoding };
        static readonly string[] DeflateEncodingHeader = { DeflateEncoding };

        static readonly GZipCompressor GZipCompressor = new GZipCompressor();
        static readonly DeflateCompressor DeflateCompressor = new DeflateCompressor();

        readonly ILog _log;
        AppFunc _next;
        bool _disposed;
        Stream _compressedBody;

        /// <summary>
        ///   Inizializza il componente usato per la compressione.
        /// </summary>
        /// <param name="log">Il log su cui scrivere eventuali messaggi.</param>
        public HttpCompressionMiddleware(ILog log)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _log = log;
        }

        /// <summary>
        ///   Inizializza il componente di Owin.
        /// </summary>
        /// <param name="next">Un riferimento al prossimo componente della pipeline.</param>
        public void Initialize(AppFunc next)
        {
            _next = next;
        }

        /// <summary>
        ///   Esegue la Dispose del componente e dello stream compresso.
        /// </summary>
        public void Dispose()
        {
            if (_compressedBody != null)
            {
                _compressedBody.Dispose();
            }
            _disposed = true;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            if (!_disposed)
            {
                // Eseguo la request, come prima cosa.
                await _next.Invoke(environment);
            }

            // Dopodiché, valuto se posso applicare un algoritmo di compressione.
            var owinContext = new OwinContext(environment);
            var acceptEncoding = owinContext.Request.Headers.Get("Accept-Encoding");

            if (acceptEncoding.Contains(GZipEncoding))
            {
                _log.Trace($"Client accepts GZIP enconding - Accept-Enconding: {acceptEncoding}");
                var uncompressedBody = owinContext.Response.Body;

                if (!_disposed)
                {
                    owinContext.Response.Headers.Add("Content-Encoding", GZipEncodingHeader);
                    owinContext.Response.Body = _compressedBody = GZipCompressor.CreateCompressionStream(uncompressedBody);
                }
            }
            else if (acceptEncoding.Contains(DeflateEncoding))
            {
                _log.Trace($"Client accepts DEFLATE enconding - Accept-Enconding: {acceptEncoding}");
                var uncompressedBody = owinContext.Response.Body;

                if (!_disposed)
                {
                    owinContext.Response.Headers.Add("Content-Encoding", DeflateEncodingHeader);
                    owinContext.Response.Body = _compressedBody = DeflateCompressor.CreateCompressionStream(uncompressedBody);
                }
            }
            else
            {
                // Nulla da fare, il client non accetta alcun tipo di compressione.
                return;
            }
        }
    }
}

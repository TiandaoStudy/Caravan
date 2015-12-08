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

using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.WebApi.Core;
using Finsa.CodeServices.Common.IO.RecyclableMemoryStream;
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

        static readonly GZipCompressor GZipCompressor = new GZipCompressor();
        static readonly DeflateCompressor DeflateCompressor = new DeflateCompressor();

        readonly ICaravanLog _log;
        AppFunc _next;
        bool _disposed;

        /// <summary>
        ///   Inizializza il componente usato per la compressione.
        /// </summary>
        /// <param name="log">Il log su cui scrivere eventuali messaggi.</param>
        public HttpCompressionMiddleware(ICaravanLog log)
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
            _disposed = true;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var owinContext = new OwinContext(environment);
            var request = owinContext.Request;
            var response = owinContext.Response;

            // Valuto se posso applicare un algoritmo di compressione osservando gli header della
            // richiesta HTTP (leggo l'header Accept-Encoding).
            bool canGZip, canDeflate;
            var acceptEncoding = owinContext.Request.Headers.Get("Accept-Encoding");

            if (!(canGZip = acceptEncoding.Contains(GZipEncoding)) || !(canDeflate = acceptEncoding.Contains(DeflateEncoding)))
            {
                _log.Trace($"Client does not accept a compressed response - Accept-Enconding: {acceptEncoding}");
                await _next(environment);
                return;
            }

            if (canGZip)
            {
                _log.Trace($"Client accepts GZIP enconding - Accept-Enconding: {acceptEncoding}");
            }
            if (canDeflate)
            {
                _log.Trace($"Client accepts DEFLATE enconding - Accept-Enconding: {acceptEncoding}");
            }

            // Replaces the response stream by a memory stream and keeps track of the real response stream.
            var responseStream = response.Body;
            response.Body = RecyclableMemoryStreamManager.Instance.GetStream(Constants.ResponseBufferTag, Constants.MinResponseBufferSize);

            try
            {
                await _next(environment);

                // Verifies that the response stream is still a readable and seekable stream.
                if (!response.Body.CanSeek || !response.Body.CanRead)
                {
                    throw new InvalidOperationException("The response stream has been replaced by an unreadable or unseekable stream");
                }

                // Determines if the response stream meets the length requirements to be compressed.
                if (!_disposed && response.Body.Length >= Constants.MinResponseLengthForCompression)
                {
                    response.Headers["Content-Encoding"] = canGZip ? GZipEncoding : DeflateEncoding;

                    // Opens a new buffer to determine the compressed response stream length.
                    using (var tmpBuffer = RecyclableMemoryStreamManager.Instance.GetStream(Constants.ResponseBufferTag, Constants.MinResponseBufferSize))
                    {
                        // Opens a new GZip stream pointing to the buffer stream.
                        using (var compressed = canGZip ? GZipCompressor.CreateCompressionStream(tmpBuffer) : DeflateCompressor.CreateCompressionStream(tmpBuffer))
                        {
                            // Rewinds the memory stream and copies it to the compressed stream.
                            response.Body.Seek(0, SeekOrigin.Begin);
                            await response.Body.CopyToAsync(compressed, Constants.ResponseChunkSize, request.CallCancelled);
                        }

                        // Rewinds the buffer stream and copies it to the real stream. See:
                        // http://blogs.msdn.com/b/bclteam/archive/2006/05/10/592551.aspx to
                        // understand why the buffer is only read after the compressed stream has
                        // been disposed.
                        tmpBuffer.Seek(0, SeekOrigin.Begin);
                        response.ContentLength = tmpBuffer.Length;
                        await tmpBuffer.CopyToAsync(responseStream, Constants.ResponseChunkSize, request.CallCancelled);
                    }

                    return;
                }

                // Rewinds the memory stream and copies it to the real response stream.
                response.Body.Seek(0, SeekOrigin.Begin);
                response.ContentLength = response.Body.Length;
                await response.Body.CopyToAsync(responseStream, Constants.ResponseChunkSize, request.CallCancelled);
            }
            catch (Exception ex) when (_log.Fatal(new LogMessage { Context = "Compressing response", Exception = ex }))
            {
                // Eccezione rilanciata in automatico, la funzione di log ritorna sempre FALSE.
            }
            finally
            {
                // Disposes the temporary memory stream.
                response.Body.Dispose();

                // Restores the real stream in the environment dictionary.
                response.Body = responseStream;
            }
        }
    }
}

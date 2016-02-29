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
using Finsa.Caravan.WebApi.Middlewares.Models;
using Finsa.CodeServices.Common.IO.RecyclableMemoryStream;
using Finsa.CodeServices.Compression;
using Microsoft.Owin;
using PommaLabs.Thrower;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Finsa.Caravan.WebApi.Middlewares
{
    /// <summary>
    ///   Middleware che si occupa della compressione del contenuto della response, se e solo se la
    ///   request ha dichiarato di accettare una risposta compressa (Accept-Enconding uguale a gzip
    ///   o deflate).
    /// 
    ///   Se la request supporta sia gzip che deflate, allora viene data precedenza a gzip.
    /// </summary>
    public sealed class HttpCompressionMiddleware : OwinMiddleware
    {
        private const string GZipEncoding = "gzip";
        private const string DeflateEncoding = "deflate";

        private static readonly GZipCompressor GZipCompressor = new GZipCompressor();
        private static readonly DeflateCompressor DeflateCompressor = new DeflateCompressor();

        private readonly Settings _settings;
        private readonly ICaravanLog _log;

        /// <summary>
        ///   Inizializza il componente usato per la compressione.
        /// </summary>
        /// <param name="next">Un riferimento al prossimo componente della pipeline.</param>
        /// <param name="settings">Le impostazioni del componente.</param>
        /// <param name="log">Il log su cui scrivere eventuali messaggi.</param>
        public HttpCompressionMiddleware(OwinMiddleware next, Settings settings, ICaravanLog log)
            : base(next)
        {
            RaiseArgumentNullException.IfIsNull(settings, nameof(settings));
            RaiseArgumentOutOfRangeException.IfIsLessOrEqual(settings.MinResponseLengthForCompression, 0, nameof(settings.MinResponseLengthForCompression));
            RaiseArgumentOutOfRangeException.IfIsLessOrEqual(settings.ResponseChunkSize, 0, nameof(settings.ResponseChunkSize));
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _settings = settings;
            _log = log;
        }

        /// <summary>
        ///   Applica una rapida compressione alla risposta HTTP se il client ha specificato
        ///   esplicitamente che è in grado di accettare risposte compresse.
        /// </summary>
        /// <param name="context">Il contesto di Owin.</param>
        /// <returns>Task per proseguire nella catena di middleware.</returns>
        public async override Task Invoke(IOwinContext context)
        {
            // Recupero le informazioni su request e response.
            var owinRequest = context.Request;
            var owinResponse = context.Response;

            // Valuto se posso applicare un algoritmo di compressione osservando gli header della
            // richiesta HTTP (leggo l'header Accept-Encoding).
            bool canGZip, canDeflate;
            var acceptEncoding = owinRequest.Headers.Get("Accept-Encoding");

            // Se l'header non è presente, allora NON devo applicare alcuna compressione.
            if (string.IsNullOrWhiteSpace(acceptEncoding))
            {
                _log.Trace($"Client has not specified the Accept-Enconding header - No compression will be applied");
                await Next.Invoke(context);
                return;
            }

            // Se GZIP o DEFLATE non sono tra gli algoritmi accettati, allora NON devo applicare
            // alcuna compressione.
            if (!(canGZip = acceptEncoding.Contains(GZipEncoding)) || !(canDeflate = acceptEncoding.Contains(DeflateEncoding)))
            {
                _log.Trace($"Client does not accept a compressed response - Accept-Enconding: {acceptEncoding}");
                await Next.Invoke(context);
                return;
            }

            if (canGZip)
            {
                _log.Trace($"Client accepts GZIP enconding - Accept-Enconding: {acceptEncoding}");
            }
            else
            {
                _log.Trace($"Client accepts DEFLATE enconding - Accept-Enconding: {acceptEncoding}");
            }

            // Replaces the response stream by a memory stream and keeps track of the real response stream.
            var responseStream = owinResponse.Body;
            owinResponse.Body = RecyclableMemoryStreamManager.Instance.GetStream(Constants.ResponseBufferTag, Constants.MinResponseBufferSize);

            try
            {
                await Next.Invoke(context);

                // Verifies that the response stream is still a readable and seekable stream.
                if (!owinResponse.Body.CanSeek || !owinResponse.Body.CanRead)
                {
                    throw new InvalidOperationException("The response stream has been replaced by an unreadable or unseekable stream");
                }

                // Determines if the response stream meets the requirements to be compressed.
                if (CanCompress(owinResponse))
                {
                    owinResponse.Headers["Content-Encoding"] = canGZip ? GZipEncoding : DeflateEncoding;

                    // Opens a new buffer to determine the compressed response stream length.
                    using (var tmpBuffer = RecyclableMemoryStreamManager.Instance.GetStream(Constants.ResponseBufferTag, Constants.MinResponseBufferSize))
                    {
                        // Opens a new GZip stream pointing to the buffer stream.
                        using (var compressed = canGZip ? GZipCompressor.CreateCompressionStream(tmpBuffer) : DeflateCompressor.CreateCompressionStream(tmpBuffer))
                        {
                            // Rewinds the memory stream and copies it to the compressed stream.
                            owinResponse.Body.Seek(0, SeekOrigin.Begin);
                            await owinResponse.Body.CopyToAsync(compressed, _settings.ResponseChunkSize, owinRequest.CallCancelled);
                        }

                        // Rewinds the buffer stream and copies it to the real stream. See:
                        // http://blogs.msdn.com/b/bclteam/archive/2006/05/10/592551.aspx to
                        // understand why the buffer is only read after the compressed stream has
                        // been disposed.
                        tmpBuffer.Seek(0, SeekOrigin.Begin);
                        owinResponse.ContentLength = tmpBuffer.Length;
                        await tmpBuffer.CopyToAsync(responseStream, _settings.ResponseChunkSize, owinRequest.CallCancelled);
                    }

                    return;
                }

                // Rewinds the memory stream and copies it to the real response stream.
                owinResponse.Body.Seek(0, SeekOrigin.Begin);
                owinResponse.ContentLength = owinResponse.Body.Length;
                await owinResponse.Body.CopyToAsync(responseStream, _settings.ResponseChunkSize, owinRequest.CallCancelled);
            }
            catch (Exception ex) when (_log.Fatal(new LogMessage { Context = "Compressing response", Exception = ex }))
            {
                // Eccezione rilanciata in automatico, la funzione di log ritorna sempre FALSE.
            }
            finally
            {
                // Disposes the temporary memory stream.
                owinResponse.Body.Dispose();

                // Restores the real stream in the environment dictionary.
                owinResponse.Body = responseStream;
            }
        }

        /// <summary>
        ///   Determina, secondo alcune semplici regole, se sia corretto applicare la compressione
        ///   alla response.
        /// </summary>
        /// <param name="owinResponse">La response di OWIN.</param>
        /// <returns>Vero se può essere compressa, falso altrimenti.</returns>
        private bool CanCompress(IOwinResponse owinResponse)
        {
            if (owinResponse.StatusCode != 200)
            {
                // Non comprimo le risposte che non sono OK.
                return false;
            }
            if (owinResponse.Body.Length < _settings.MinResponseLengthForCompression)
            {
                // Se la risposta è troppo corta, non la comprimo.
                return false;
            }
            if (!string.IsNullOrWhiteSpace(owinResponse.Headers["Content-Encoding"]))
            {
                // Se, per qualche motivo, la risposta fosse già codificata in altro modo, non
                // applico ulteriori codifiche.
                return false;
            }
            return true;
        }

        /// <summary>
        ///   Impostazioni del componente di middleware.
        /// </summary>
        public sealed class Settings : AbstractMiddlewareSettings
        {
            /// <summary>
            ///   La lunghezza minima, in byte, per cui viene attivata la compressione della risposta.
            /// 
            ///   Viene impostata di default a 4096.
            /// </summary>
            public int MinResponseLengthForCompression { get; set; } = 4096;

            /// <summary>
            ///   Dimensione usata per le risposte di tipo "chunked".
            /// 
            ///   Viene impostata di default a 81920.
            /// </summary>
            public int ResponseChunkSize { get; set; } = 81920;
        }
    }
}

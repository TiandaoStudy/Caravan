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

namespace Finsa.Caravan.WebApi.Core
{
    /// <summary>
    ///   Constanti usate nella libreria Finsa.Caravan.WebApi.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///   ID usato come correlazione nei log.
        /// </summary>
        public const string RequestId = "request_id";

        /// <summary>
        ///   Header usato per facilitare il tracciamento dei log.
        /// </summary>
        public const string RequestIdHeader = "x-caravan-request-id";

        /// <summary>
        ///   Il tag usato dai buffer di risposta dei componenti di middleware.
        /// </summary>
        public const string ResponseBufferTag = "Finsa.Caravan.WebApi.ResponseBuffers";

        /// <summary>
        ///   La dimensione minima usata dai buffer di risposta dei componenti di middleware.
        /// </summary>
        public const int MinResponseBufferSize = 512;
    }
}

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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Security.Exceptions
{
    /// <summary>
    ///   Segnala che esiste l'utente che sta per essere inserito o aggiornato presenta dettagli non validi.
    /// </summary>
    [Serializable]
    public class SecUserNotValidException : Exception
    {
        /// <summary>
        ///   Costruisce l'eccezione.
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="userLogin"></param>
        /// <param name="errors"></param>
        public SecUserNotValidException(string appName, string userLogin, IList<string> errors)
            : base($"User '{userLogin}' of application '{appName}' is not valid because of the following errors:" + FormatErrors(errors))
        {
            Errors = errors;
        }

        /// <summary>
        ///   Costruisce l'eccezione.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SecUserNotValidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///   Gli errori riscontrati durante la validazione.
        /// </summary>
        public IList<string> Errors { get; }

        private static string FormatErrors(IList<string> errors) => Environment.NewLine + string.Join(Environment.NewLine, errors);
    }
}

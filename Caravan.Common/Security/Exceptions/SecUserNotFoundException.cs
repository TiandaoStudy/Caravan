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
using System.Runtime.Serialization;

namespace Finsa.Caravan.Common.Security.Exceptions
{
    /// <summary>
    ///   Segnala che non esiste un utente con la login data all'interno dell'applicazione data.
    /// </summary>
    [Serializable]
    public class SecUserNotFoundException : Exception
    {
        public SecUserNotFoundException(string appName, string userLogin)
            : base($"User '{userLogin}' not found inside application '{appName}'")
        {
        }

        public SecUserNotFoundException(string appName, long userId)
            : base($"User #{userId} not found inside application '{appName}'")
        {
        }

        protected SecUserNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

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
    ///   Segnala che esiste già un ruolo con il medesimo nome all'interno del gruppo e dell'applicazione dati.
    /// </summary>
    [Serializable]
    public class SecEntryExistingException : Exception
    {
        public SecEntryExistingException(string appName, string userLogin, string groupName, string roleName, string contextName, string objectName)
            : base($"A security entry for application '{appName}', user '{userLogin}', group '{groupName}', role '{roleName}', context '{contextName}' and object '{objectName}' is already existing")
        {
        }

        protected SecEntryExistingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

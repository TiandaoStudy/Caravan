﻿// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
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

using Finsa.Caravan.WebApi.Identity.Models;
using System.Web.Http.Controllers;

namespace Finsa.Caravan.WebApi.Identity
{
    /// <summary>
    ///   Gestisce gli errori avvenuti durante la validazione dell'accesso.
    /// </summary>
    public interface IAuthorizationErrorHandler
    {
        /// <summary>
        ///   Gestisce un errore avvenuto durante la validazione dell'accesso.
        /// </summary>
        /// <param name="actionContext">L'azione per cui si stava validando l'accesso.</param>
        /// <param name="errorContext">Il tipo di errore riscontrato.</param>
        void HandleError(HttpActionContext actionContext, AuthorizationErrorContext errorContext);
    }
}

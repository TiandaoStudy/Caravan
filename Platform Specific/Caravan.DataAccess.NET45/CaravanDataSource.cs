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

using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.DataAccess.Sql;
using Ninject;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Punto di accesso ai dati - logger, security, ecc ecc.
    /// </summary>
    /// <remarks>Mantenuta (momentaneamente) per retrocompatibilità interna a Caravan.</remarks>
    public static class CaravanDataSource
    {
        internal static ICaravanDataSourceManager Manager => CaravanServiceProvider.NinjectKernel.Get<ICaravanDataSourceManager>();

        internal static ICaravanLogRepository Logger => CaravanServiceProvider.NinjectKernel.Get<ICaravanLogRepository>();

        internal static ICaravanSecurityRepository Security => CaravanServiceProvider.NinjectKernel.Get<ICaravanSecurityRepository>();

        /// <summary>
        ///   Usato negli unit test per resettare rapidamente la base dati di Caravan.
        /// </summary>
        public static void Reset() => SqlDbContext.Reset();
    }
}
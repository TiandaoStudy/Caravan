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

namespace Finsa.Caravan.Common.Reporting
{
    /// <summary>
    ///   Descrive i metodi necessari da tutte le raccolte di servizi delle sorgenti dati.
    /// </summary>
    public interface IDataSourceRequiredServices
    {
        /// <summary>
        ///   Meotodo invocato da <see cref="AbstractObjectDataSource{TRequiredServices}"/> quando
        ///   non è disponibile <see cref="ServiceProvider.NinjectKernel"/>. Un caso in cui questo
        ///   avviene è quando si sta cercando di fare un'anteprima del report.
        /// </summary>
        void SetDefaults();
    }
}
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

using Ninject;
using System.ComponentModel;

namespace Finsa.Caravan.Common.Reporting
{
    /// <summary>
    ///   La sorgente dati di base. Si occupa principalmente di iniettare via Ninject i servizi a
    ///   codice richiesti dalla sorgente dati figlia.
    /// </summary>
    /// <typeparam name="TRequiredServices">
    ///   I servizi a codice richiesti dalla sorgente dati.
    /// </typeparam>
    [DataObject]
    public abstract class AbstractObjectDataSource<TRequiredServices>
        where TRequiredServices : class, IDataSourceRequiredServices, new()
    {
        /// <summary>
        ///   Inietta via Ninject i servizi a codice richiesti dalla sorgente dati figlia.
        /// </summary>
        static AbstractObjectDataSource()
        {
            var kernel = ServiceProvider.NinjectKernel;
            if (kernel != null)
            {
                Services = kernel.Get<TRequiredServices>();
            }
            else
            {
                // Se il kernel non è presente, provo a invocare il metodo che imposta i servizi di default.
                Services = new TRequiredServices();
                Services.SetDefaults();
            }
        }

        /// <summary>
        ///   I servizi a codice richiesti dalla sorgente dati.
        /// </summary>
        protected static TRequiredServices Services { get; set; }
    }
}

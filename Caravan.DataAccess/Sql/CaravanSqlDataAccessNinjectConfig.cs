using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Sql
{
    public abstract class CaravanSqlDataAccessNinjectConfig
    {
        /// <summary>
        ///   Inizializza il modulo.
        /// </summary>
        /// <param name="dependencyHandling">Modalità di gestione delle dipendenze.</param>
        /// <param name="dataSourceKind">
        ///   Il tipo della sorgente dati che verrà usato dalla componente di accesso ai dati.
        /// </param>
        protected CaravanSqlDataAccessNinjectConfig(DependencyHandling dependencyHandling, CaravanDataSourceKind dataSourceKind)
        {
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(DependencyHandling), dependencyHandling), ErrorMessages.InvalidEnumValue, nameof(dependencyHandling));
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(CaravanDataSourceKind), dataSourceKind), ErrorMessages.InvalidEnumValue, nameof(dataSourceKind));
            DependencyHandling = dependencyHandling;
            DataSourceKind = dataSourceKind;
        }
    }
}

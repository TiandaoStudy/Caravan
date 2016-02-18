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

using System.Data.Common;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Gestisce la sorgente dati di Caravan.
    /// </summary>
    public interface ICaravanDataSourceManager
    {
        /// <summary>
        ///   Gets or sets the connection string used to access Caravan data source.
        /// </summary>
        /// <value>The connection string used to access Caravan data source.</value>
        string ConnectionString { get; set; }

        /// <summary>
        ///   The database vendor (Oracle, SQL Server, etc etc) or the driver type (REST).
        /// </summary>
        CaravanDataSourceKind DataSourceKind { get; }

        /// <summary>
        ///   Il nome della sorgente dati a cui è collegato Caravan.
        /// </summary>
        string DataSourceName { get; }

        /// <summary>
        ///   Processa la stringa di connessione aggiungendo flag potenzialmente utili.
        /// </summary>
        /// <param name="connectionString">La stringa di connessione di processare.</param>
        /// <returns>La stringa di connessione processata.</returns>
        string ElaborateConnectionString(string connectionString);

        /// <summary>
        ///   Returns an open DB connection using <see cref="ConnectionString"/> as the connection string.
        /// </summary>
        /// <returns>An open DB connection.</returns>
        DbConnection OpenConnection();

        /// <summary>
        ///   Returns a _not_ open DB connection using <see cref="ConnectionString"/> as the
        ///   connection string.
        /// </summary>
        /// <returns>A _not_ open DB connection.</returns>
        DbConnection CreateConnection();
    }
}

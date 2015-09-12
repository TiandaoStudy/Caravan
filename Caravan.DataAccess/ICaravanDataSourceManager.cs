using System.Data.Common;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   </summary>
    public interface ICaravanDataSourceManager
    {
        /// <summary>
        ///   Gets or sets the connection string used to access Caravan data source.
        /// </summary>
        /// <value>
        /// The connection string used to access Caravan data source.
        /// </value>
        string ConnectionString { get; set; }

        /// <summary>
        ///   The database vendor (Oracle, SQL Server, etc etc) or the driver type (REST).
        /// </summary>
        CaravanDataSourceKind DataSourceKind { get; }

        /// <summary>
        ///   </summary>
        /// <param name="connectionString"></param>
        string ElaborateConnectionString(string connectionString);

        /// <summary>
        ///   Returns an open DB connection using <see cref="ConnectionString"/> as the connection string.
        /// </summary>
        /// <returns>An open DB connection.</returns>
        DbConnection OpenConnection();

        /// <summary>
        ///   Returns a _not_ open DB connection using <see cref="ConnectionString"/> as the connection string.
        /// </summary>
        /// <returns>A _not_ open DB connection.</returns>
        DbConnection CreateConnection();
    }
}
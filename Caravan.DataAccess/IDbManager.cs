using System.Data.Common;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   </summary>
    public interface IDbManager
    {
        /// <summary>
        ///   The database vendor (Oracle, SQL Server, etc etc) or the driver type (REST).
        /// </summary>
        DataAccessKind Kind { get; }

        /// <summary>
        ///   </summary>
        /// <param name="connectionString"></param>
        void ElaborateConnectionString(ref string connectionString);

        /// <summary>
        ///   Returns an open DB connection using <see cref="Db.ConnectionString"/> as the connection string.
        /// </summary>
        /// <returns>An open DB connection.</returns>
        DbConnection OpenConnection();

        /// <summary>
        ///   Returns a _not_ open DB connection using <see cref="Db.ConnectionString"/> as the connection string.
        /// </summary>
        /// <returns>A _not_ open DB connection.</returns>
        DbConnection CreateConnection();
    }

    /// <summary>
    ///   The database vendor (Oracle, SQL Server, etc etc) or the driver type (REST).
    /// </summary>
    public enum DataAccessKind : byte
    {
        Oracle = 0,
        PostgreSql = 1,
        Rest = 2,
        SqlServer = 3,
        SqlServerCe = 4,
        MongoDb = 5,
        MySql = 6,
        FakeSql = 7
    }
}
namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   The database vendor (Oracle, SQL Server, etc etc) or the driver type (REST) that should be
    ///   used to access Caravan data source.
    /// </summary>
    public enum CaravanDataSourceKind : byte
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

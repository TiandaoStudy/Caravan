using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Finsa.Caravan.DataAccess.Drivers.Mongo;
using Finsa.Caravan.DataAccess.Drivers.Rest;
using Finsa.Caravan.DataAccess.Drivers.Sql;
using Finsa.Caravan.DataAccess.Drivers.Sql.FakeSql;
using Finsa.Caravan.DataAccess.Drivers.Sql.MySql;
using Finsa.Caravan.DataAccess.Drivers.Sql.Oracle;
using Finsa.Caravan.DataAccess.Drivers.Sql.PostgreSql;
using Finsa.Caravan.DataAccess.Drivers.Sql.SqlServer;
using Finsa.Caravan.DataAccess.Drivers.Sql.SqlServerCe;
using RestSharp;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Punto di accesso ai dati - logger, security, ecc ecc.
    /// </summary>
    public static class DataSource
    {
        private static ICaravanLogRepository _loggerInstance;
        private static ICaravanSecurityRepository _securityRepositoryInstance;
        private static IDataSourceManager _dataSourceManagerInstance;

        static DataSource()
        {
            var dataSourceKind = DataAccessConfiguration.Instance.DataSourceKind;
            if (Enum.IsDefined(typeof(DataSourceKind), dataSourceKind))
            {
                SetDataAccessKind(dataSourceKind);
            }
            else
            {
                throw new ConfigurationErrorsException();
            }
        }

        #region Public Properties - Instances

        public static DataSourceKind DataSourceKind { get; set; }

        public static IDataSourceManager Manager
        {
            get { return _dataSourceManagerInstance; }
        }

        public static ICaravanLogRepository Logger
        {
            get { return _loggerInstance; }
        }

        public static ICaravanSecurityRepository Security
        {
            get { return _securityRepositoryInstance; }
        }

        #endregion Public Properties - Instances

        #region Public Properties - REST Driver

        /// <summary>
        ///   Object used to authenticate each REST request.
        /// </summary>
        public static dynamic RestAuthObject { get; set; }

        #endregion Public Properties - REST Driver

        #region Public Properties - Common

        private static string CachedConnectionString;

        public static string ConnectionString
        {
            get
            {
                if (String.IsNullOrWhiteSpace(CachedConnectionString))
                {
                    CachedConnectionString = DataAccessConfiguration.Instance.ConnectionString;
                }
                return CachedConnectionString;
            }
            set
            {
                Manager.ElaborateConnectionString(ref value);
                CachedConnectionString = value;
            }
        }

        #endregion Public Properties - Common

        #region Methods that must be used _ONLY_ inside (or for) Unit Tests

        internal static void ChangeDataAccessKindUseOnlyForUnitTestsPlease()
        {
            SetDataAccessKind(DataSourceKind.FakeSql);
        }

        internal static void ClearAllTablesUseOnlyInsideUnitTestsPlease()
        {
            switch (DataSourceKind)
            {
                // Custom actions are required for Effort.
                case DataSourceKind.FakeSql:
                    // A new connection is created and persisted for the whole test duration.
                    (Manager as FakeSqlDataSourceManager).ResetConnection();
                    // The database is recreated, since it is in-memory and probably it does not exist.
                    using (var ctx = SqlDbContext.CreateWriteContext())
                    {
                        ctx.Database.CreateIfNotExists();
                        Database.SetInitializer(new DropCreateDatabaseAlways<SqlDbContext>());
                        ctx.Database.Initialize(true);
                        Database.SetInitializer(new CreateDatabaseIfNotExists<SqlDbContext>());
                    }
                    break;

                case DataSourceKind.MySql:
                case DataSourceKind.Oracle:
                case DataSourceKind.PostgreSql:
                case DataSourceKind.SqlServer:
                case DataSourceKind.SqlServerCe:
                    using (var trx = new TransactionScope(TransactionScopeOption.Suppress))
                    using (var ctx = SqlDbContext.CreateWriteContext())
                    {
                        ctx.LogEntries.RemoveRange(ctx.LogEntries.ToList());
                        ctx.SaveChanges();
                        ctx.LogSettings.RemoveRange(ctx.LogSettings.ToList());
                        ctx.SaveChanges();
                        ctx.SecEntries.RemoveRange(ctx.SecEntries.ToList());
                        ctx.SaveChanges();
                        ctx.SecObjects.RemoveRange(ctx.SecObjects.ToList());
                        ctx.SaveChanges();
                        ctx.SecContexts.RemoveRange(ctx.SecContexts.ToList());
                        ctx.SaveChanges();
                        ctx.SecUsers.RemoveRange(ctx.SecUsers.ToList());
                        ctx.SaveChanges();
                        ctx.SecGroups.RemoveRange(ctx.SecGroups.ToList());
                        ctx.SaveChanges();
                        ctx.SecApps.RemoveRange(ctx.SecApps.ToList());
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                    break;

                case DataSourceKind.Rest:
                    var client = new RestClient(DataAccessConfiguration.Instance.RestServiceUrl);
                    var request = new RestRequest("testing/clearAllTablesUseOnlyInsideUnitTestsPlease", Method.POST);
                    client.Execute(request);
                    break;

                case DataSourceKind.MongoDb:
                    MongoUtilities.GetLogEntryCollection().Drop();
                    MongoUtilities.GetSecAppCollection().Drop();
                    MongoUtilities.GetSequenceCollection().Drop();
                    break;
            }
        }

        internal static void StartRemoteTestingUseOnlyInsideUnitTestsPlease()
        {
            RestAuthObject = DataAccessConfiguration.Instance.RestTestAuthObject;
        }

        #endregion Methods that must be used _ONLY_ inside (or for) Unit Tests

        #region Private Methods

        private static void SetDataAccessKind(DataSourceKind kind)
        {
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(DataSourceKind), kind));
            DataSourceKind = kind;

            switch (kind)
            {
                case DataSourceKind.FakeSql:
                    _dataSourceManagerInstance = new FakeSqlDataSourceManager();
                    break;

                case DataSourceKind.MongoDb:
                    _dataSourceManagerInstance = new MongoDataSourceManager();
                    _loggerInstance = new MongoLogRepository();
                    _securityRepositoryInstance = new MongoSecurityRepository();
                    break;

                case DataSourceKind.MySql:
                    _dataSourceManagerInstance = new MySqlDataSourceManager();
                    break;

                case DataSourceKind.Oracle:
                    _dataSourceManagerInstance = new OracleDataSourceManager();
                    break;

                case DataSourceKind.PostgreSql:
                    _dataSourceManagerInstance = new PostgreSqlDataSourceManager();
                    break;

                case DataSourceKind.Rest:
                    _loggerInstance = new RestLogRepository();
                    _securityRepositoryInstance = new RestSecurityRepository();
                    break;

                case DataSourceKind.SqlServer:
                    _dataSourceManagerInstance = new SqlServerDataSourceManager();
                    break;

                case DataSourceKind.SqlServerCe:
                    _dataSourceManagerInstance = new SqlServerCeDataSourceManager();
                    break;
            }

            // Sets the implementations which are shared between SQL drivers.
            switch (kind)
            {
                case DataSourceKind.FakeSql:
                case DataSourceKind.MySql:
                case DataSourceKind.Oracle:
                case DataSourceKind.PostgreSql:
                case DataSourceKind.SqlServer:
                case DataSourceKind.SqlServerCe:
                    _loggerInstance = new SqlLogRepository();
                    _securityRepositoryInstance = new SqlSecurityRepository();
                    break;
            }
        }

        #endregion Private Methods
    }
}
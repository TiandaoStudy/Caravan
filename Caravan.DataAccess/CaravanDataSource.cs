using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.DataAccess.Drivers.Mongo;
using Finsa.Caravan.DataAccess.Drivers.Rest;
using Finsa.Caravan.DataAccess.Drivers.Sql;
using Finsa.Caravan.DataAccess.Drivers.Sql.FakeSql;
using Finsa.Caravan.DataAccess.Drivers.Sql.MySql;
using Finsa.Caravan.DataAccess.Drivers.Sql.Oracle;
using Finsa.Caravan.DataAccess.Drivers.Sql.PostgreSql;
using Finsa.Caravan.DataAccess.Drivers.Sql.SqlServer;
using Finsa.Caravan.DataAccess.Drivers.Sql.SqlServerCe;
using PommaLabs.Thrower;
using RestSharp;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Punto di accesso ai dati - logger, security, ecc ecc.
    /// </summary>
    public static class CaravanDataSource
    {
        private static ICaravanLogRepository _loggerInstance;
        private static ICaravanSecurityRepository _securityRepositoryInstance;
        private static ICaravanDataSourceManager _dataSourceManagerInstance;

        static CaravanDataSource()
        {
            var dataSourceKind = DataAccessConfiguration.Instance.DataSourceKind;
            if (Enum.IsDefined(typeof(CaravanDataSourceKind), dataSourceKind))
            {
                SetDataAccessKind(dataSourceKind);
            }
            else
            {
                throw new ConfigurationErrorsException();
            }
        }

        #region Public Properties - Instances

        public static CaravanDataSourceKind DataSourceKind { get; set; }

        public static ICaravanDataSourceManager Manager
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

        #region Methods that must be used _ONLY_ inside (or for) Unit Tests

        internal static void ChangeDataAccessKindUseOnlyForUnitTestsPlease()
        {
            SetDataAccessKind(CaravanDataSourceKind.FakeSql);
        }

        internal static void ClearAllTablesUseOnlyInsideUnitTestsPlease()
        {
            switch (DataSourceKind)
            {
                // Custom actions are required for Effort.
                case CaravanDataSourceKind.FakeSql:
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

                case CaravanDataSourceKind.MySql:
                case CaravanDataSourceKind.Oracle:
                case CaravanDataSourceKind.PostgreSql:
                case CaravanDataSourceKind.SqlServer:
                case CaravanDataSourceKind.SqlServerCe:
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

                case CaravanDataSourceKind.Rest:
                    var client = new RestClient(Manager.ConnectionString);
                    var request = new RestRequest("testing/clearAllTablesUseOnlyInsideUnitTestsPlease", Method.POST);
                    client.Execute(request);
                    break;

                case CaravanDataSourceKind.MongoDb:
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

        private static void SetDataAccessKind(CaravanDataSourceKind kind)
        {
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(CaravanDataSourceKind), kind));
            DataSourceKind = kind;

            switch (kind)
            {
                case CaravanDataSourceKind.FakeSql:
                    _dataSourceManagerInstance = new FakeSqlDataSourceManager();
                    break;

                case CaravanDataSourceKind.MongoDb:
                    _dataSourceManagerInstance = new MongoDataSourceManager();
                    _loggerInstance = new MongoLogRepository();
                    _securityRepositoryInstance = new MongoSecurityRepository();
                    break;

                case CaravanDataSourceKind.MySql:
                    _dataSourceManagerInstance = new MySqlDataSourceManager();
                    break;

                case CaravanDataSourceKind.Oracle:
                    _dataSourceManagerInstance = new OracleDataSourceManager();
                    break;

                case CaravanDataSourceKind.PostgreSql:
                    _dataSourceManagerInstance = new PostgreSqlDataSourceManager();
                    break;

                case CaravanDataSourceKind.Rest:
                    _loggerInstance = new RestLogRepository();
                    _securityRepositoryInstance = new RestSecurityRepository();
                    break;

                case CaravanDataSourceKind.SqlServer:
                    _dataSourceManagerInstance = new SqlServerDataSourceManager();
                    break;

                case CaravanDataSourceKind.SqlServerCe:
                    _dataSourceManagerInstance = new SqlServerCeDataSourceManager();
                    break;
            }

            // Sets the implementations which are shared between SQL drivers.
            switch (kind)
            {
                case CaravanDataSourceKind.FakeSql:
                case CaravanDataSourceKind.MySql:
                case CaravanDataSourceKind.Oracle:
                case CaravanDataSourceKind.PostgreSql:
                case CaravanDataSourceKind.SqlServer:
                case CaravanDataSourceKind.SqlServerCe:
                    _loggerInstance = new SqlLogRepository();
                    _securityRepositoryInstance = new SqlSecurityRepository();
                    break;
            }
        }

        #endregion Private Methods
    }
}
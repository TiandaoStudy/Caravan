using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Transactions;
using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess.Mongo;
using Finsa.Caravan.DataAccess.Properties;
using Finsa.Caravan.DataAccess.Rest;
using Finsa.Caravan.DataAccess.Sql;
using Finsa.Caravan.DataAccess.Sql.FakeSql;
using Finsa.Caravan.DataAccess.Sql.MySql;
using Finsa.Caravan.DataAccess.Sql.Oracle;
using Finsa.Caravan.DataAccess.Sql.PostgreSql;
using Finsa.Caravan.DataAccess.Sql.SqlServer;
using Finsa.Caravan.DataAccess.Sql.SqlServerCe;
using PommaLabs.Diagnostics;
using PommaLabs.KVLite;
using RestSharp;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Punto di accesso ai dati - logger, security, ecc ecc.
    /// </summary>
    public static class Db
    {
        private const string CachePartitionName = "Caravan.DataAccess";
        private const string ConnectionStringKey = "ConnectionString";

        private static ILogManager _logManagerInstance;
        private static ISecurityManager _securityManagerInstance;
        private static IDbManager _dbManagerInstance;

        static Db()
        {
            DataAccessKind accessKind;
            if (Enum.TryParse(Settings.Default.DataAccessKind, true, out accessKind))
            {
                SetDataAccessKind(accessKind);
            }
            else
            {
                throw new ConfigurationErrorsException();
            }
        }

        #region Public Properties - Instances

        public static DataAccessKind AccessKind { get; set; }

        public static IDbManager Manager
        {
            get { return _dbManagerInstance; }
        }

        public static ILogManager Logger
        {
            get { return _logManagerInstance; }
        }

        public static ISecurityManager Security
        {
            get { return _securityManagerInstance; }
        }

        #endregion Public Properties - Instances

        #region Public Properties - REST Driver

        /// <summary>
        ///   Object used to authenticate each REST request.
        /// </summary>
        public static dynamic RestAuthObject { get; set; }

        #endregion Public Properties - REST Driver

        public static string ConnectionString
        {
            get
            {
                var cachedConnectionString = Cache.Instance.Get<string>(CachePartitionName, ConnectionStringKey);
                var configConnectionString = Settings.Default.ConnectionString;

                if (String.IsNullOrWhiteSpace(configConnectionString))
                {
                    // If connection string is not in the configuration file, then return the cached
                    // one, even if empty.
                    return cachedConnectionString;
                }
                Manager.ElaborateConnectionString(ref configConnectionString);
                if (configConnectionString == cachedConnectionString)
                {
                    // Connection string has _not_ changed, return the cached one.
                    return cachedConnectionString;
                }

                // Connection string _has_ changed, update the cached one.
                Cache.Instance.AddStatic(CachePartitionName, ConnectionStringKey, configConnectionString);
                return configConnectionString;
            }
            set
            {
                Manager.ElaborateConnectionString(ref value);
                Cache.Instance.AddStatic(CachePartitionName, ConnectionStringKey, value);
            }
        }

        #region EF Helpers

        public static List<T> ToLogAndList<T>(this IQueryable<T> queryable)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var list = queryable.ToList();
            stopwatch.Stop();

            // Logging query and execution time.
            var logEntry = queryable.ToString();
            var milliseconds = stopwatch.ElapsedMilliseconds;
            Logger.LogDebugAsync<IDbManager>("EF generated query", logEntry, "Logging and timing the query", new[]
            {
                KeyValuePair.Create("milliseconds", milliseconds.ToString(CultureInfo.InvariantCulture))
            });

            return list;
        }

        #endregion EF Helpers

        #region Methods that must be used _ONLY_ inside (or for) Unit Tests

        internal static void ChangeDataAccessKindUseOnlyForUnitTestsPlease()
        {
            SetDataAccessKind(DataAccessKind.FakeSql);
        }

        internal static void ClearAllTablesUseOnlyInsideUnitTestsPlease()
        {
            switch (AccessKind)
            {
                // Custom actions are required for Effort.
                case DataAccessKind.FakeSql:
                    // A new connection is created and persisted for the whole test duration.
                    (Manager as FakeSqlDbManager).ResetConnection();
                    // The database is recreated, since it is in-memory and probably it does not exist.
                    using (var ctx = SqlDbContext.CreateWriteContext())
                    {
                        ctx.Database.CreateIfNotExists();
                        Database.SetInitializer(new DropCreateDatabaseAlways<SqlDbContext>());
                        ctx.Database.Initialize(true);
                        Database.SetInitializer(new CreateDatabaseIfNotExists<SqlDbContext>());
                    }
                    break;

                case DataAccessKind.MySql:
                case DataAccessKind.Oracle:
                case DataAccessKind.PostgreSql:
                case DataAccessKind.SqlServer:
                case DataAccessKind.SqlServerCe:
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

                case DataAccessKind.Rest:
                    var client = new RestClient(Settings.Default.RestServiceUrl);
                    var request = new RestRequest("testing/clearAllTablesUseOnlyInsideUnitTestsPlease", Method.POST);
                    client.Execute(request);
                    break;

                case DataAccessKind.MongoDb:
                    MongoUtilities.GetLogEntryCollection().Drop();
                    MongoUtilities.GetSecAppCollection().Drop();
                    MongoUtilities.GetSequenceCollection().Drop();
                    break;
            }
        }

        internal static void StartRemoteTestingUseOnlyInsideUnitTestsPlease()
        {
            RestAuthObject = Settings.Default.RestTestAuthObject;
        }

        #endregion Methods that must be used _ONLY_ inside (or for) Unit Tests

        #region Private Methods

        private static void SetDataAccessKind(DataAccessKind kind)
        {
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(DataAccessKind), kind));
            AccessKind = kind;

            switch (kind)
            {
                case DataAccessKind.FakeSql:
                    _dbManagerInstance = new FakeSqlDbManager();
                    break;

                case DataAccessKind.MongoDb:
                    _dbManagerInstance = new MongoDbManager();
                    _logManagerInstance = new MongoLogManager();
                    _securityManagerInstance = new MongoSecurityManager();
                    break;

                case DataAccessKind.MySql:
                    _dbManagerInstance = new MySqlDbManager();
                    break;

                case DataAccessKind.Oracle:
                    _dbManagerInstance = new OracleDbManager();
                    break;

                case DataAccessKind.PostgreSql:
                    _dbManagerInstance = new PostgreSqlDbManager();
                    break;

                case DataAccessKind.Rest:
                    _logManagerInstance = new RestLogManager();
                    _securityManagerInstance = new RestSecurityManager();
                    break;

                case DataAccessKind.SqlServer:
                    _dbManagerInstance = new SqlServerDbManager();
                    break;

                case DataAccessKind.SqlServerCe:
                    _dbManagerInstance = new SqlServerCeDbManager();
                    break;
            }

            // Sets the implementations which are shared between SQL drivers.
            switch (kind)
            {
                case DataAccessKind.FakeSql:
                case DataAccessKind.MySql:
                case DataAccessKind.Oracle:
                case DataAccessKind.PostgreSql:
                case DataAccessKind.SqlServer:
                case DataAccessKind.SqlServerCe:
                    _logManagerInstance = new SqlLogManager();
                    _securityManagerInstance = new SqlSecurityManager();
                    break;
            }
        }

        #endregion Private Methods
    }
}
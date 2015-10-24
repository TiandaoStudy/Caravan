using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.DataAccess.Mongo;
using Finsa.Caravan.DataAccess.Rest;
using Finsa.Caravan.DataAccess.Sql;
using Finsa.Caravan.DataAccess.Sql.FakeSql;
using Finsa.Caravan.DataAccess.Sql.MySql;
using Finsa.Caravan.DataAccess.Sql.Oracle;
using Finsa.Caravan.DataAccess.Sql.PostgreSql;
using Finsa.Caravan.DataAccess.Sql.SqlServer;
using Finsa.Caravan.DataAccess.Sql.SqlServerCe;
using Finsa.CodeServices.Common.Portability;
using InteractivePreGeneratedViews;
using PommaLabs.Thrower;
using RestSharp;
using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Transactions;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Punto di accesso ai dati - logger, security, ecc ecc.
    /// </summary>
    internal static class CaravanDataSource
    {
        static CaravanDataSource()
        {
            var dataSourceKind = CaravanDataAccessConfiguration.Instance.DataSourceKind;
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

        public static ICaravanDataSourceManager Manager { get; private set; }

        public static ICaravanLogRepository Logger { get; private set; }

        public static ICaravanSecurityRepository Security { get; private set; }

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
                    //MongoUtilities.GetLogEntryCollection().Drop();
                    //MongoUtilities.GetSecAppCollection().Drop();
                    //MongoUtilities.GetSequenceCollection().Drop();
                    break;
            }
        }

        internal static void StartRemoteTestingUseOnlyInsideUnitTestsPlease()
        {
            RestAuthObject = CaravanDataAccessConfiguration.Instance.RestTestAuthObject;
        }

        #endregion Methods that must be used _ONLY_ inside (or for) Unit Tests

        #region Private Methods

        private static void SetDataAccessKind(CaravanDataSourceKind kind)
        {
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(CaravanDataSourceKind), kind), nameof(kind));
            DataSourceKind = kind;

            switch (kind)
            {
                case CaravanDataSourceKind.FakeSql:
                    Manager = new FakeSqlDataSourceManager();
                    break;

                case CaravanDataSourceKind.MongoDb:
                    Manager = new MongoDataSourceManager();
                    Logger = new MongoLogRepository();
                    Security = new MongoSecurityRepository();
                    break;

                case CaravanDataSourceKind.MySql:
                    Manager = new MySqlDataSourceManager();
                    break;

                case CaravanDataSourceKind.Oracle:
                    Manager = new OracleDataSourceManager();
                    break;

                case CaravanDataSourceKind.PostgreSql:
                    Manager = new PostgreSqlDataSourceManager();
                    break;

                case CaravanDataSourceKind.Rest:
                    Logger = new RestLogRepository();
                    Security = new RestSecurityRepository();
                    break;

                case CaravanDataSourceKind.SqlServer:
                    Manager = new SqlServerDataSourceManager();
                    break;

                case CaravanDataSourceKind.SqlServerCe:
                    Manager = new SqlServerCeDataSourceManager();
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
                    Logger = new SqlLogRepository();
                    Security = new SqlSecurityRepository();

                    // Configura la generazione automatica delle viste per EF.
                    using (var ctx = new SqlDbContext())
                    {
                        // Per prima cosa, recupero la cartella di destinazione e mi assicuro che
                        // esista. Se non esiste, la creo, altrimenti avrei un errore alla prima query.
                        var pregenViewsPath = CaravanDataAccessConfiguration.Instance.Drivers_Sql_EFPregeneratedViews_Path;
                        var mappedPregenViewsPath = PortableEnvironment.MapPath(pregenViewsPath);
                        if (!Directory.Exists(mappedPregenViewsPath))
                        {
                            Directory.CreateDirectory(mappedPregenViewsPath);
                        }

                        // Quindi, calcolo il nome del file di destinazione e applico il nuovo meccanismo.
                        var caravanPregenViewsFileName = CaravanDataAccessConfiguration.Instance.Drivers_Sql_EFPregeneratedViews_CaravanViewsFileName;
                        var caravanPregenViews = Path.Combine(mappedPregenViewsPath, caravanPregenViewsFileName);
                        InteractiveViews.SetViewCacheFactory(ctx, new FileViewCacheFactory(caravanPregenViews));
                    }

                    break;
            }
        }

        #endregion Private Methods
    }
}
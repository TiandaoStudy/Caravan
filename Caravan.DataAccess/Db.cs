using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Transactions;
using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Mongo;
using Finsa.Caravan.DataAccess.Properties;
using Finsa.Caravan.DataAccess.Rest;
using Finsa.Caravan.DataAccess.Sql;
using Finsa.Caravan.DataAccess.Sql.Oracle;
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

      private static LogManagerBase _logManagerInstance;
      private static QueryManagerBase _queryManagerInstance;
      private static ISecurityManager _securityManagerInstance;
      private static DbManagerBase _dbManagerInstance;
      private static Func<DbContextBase> _dbContextGenerator;

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

      public static IQueryManager Query
      {
         get { return _queryManagerInstance; }
      }

      public static ISecurityManager Security
      {
         get { return _securityManagerInstance; }
      }

      #endregion

      #region Public Properties - REST Driver

      /// <summary>
      ///   Object used to authenticate each REST request.
      /// </summary>
      public static dynamic RestAuthObject { get; set; }

      #endregion

      public static string ConnectionString
      {
         get
         {
            var cachedConnectionString = Common.Properties.Settings.Default.DefaultCache.Get<string>(CachePartitionName, ConnectionStringKey);
            var configConnectionString = Settings.Default.ConnectionString;

            if (String.IsNullOrWhiteSpace(configConnectionString))
            {
               // If connection string is not in the configuration file, then return the cached one, even if empty.
               return cachedConnectionString;
            }
            Manager.ElaborateConnectionString(ref configConnectionString);
            if (configConnectionString == cachedConnectionString)
            {
               // Connection string has _not_ changed, return the cached one.
               return cachedConnectionString;
            }

            // Connection string _has_ changed, update the cached one.
            Common.Properties.Settings.Default.DefaultCache.AddStatic(CachePartitionName, ConnectionStringKey, configConnectionString);
            return configConnectionString;
         }
         set
         {
            Manager.ElaborateConnectionString(ref value);
            Common.Properties.Settings.Default.DefaultCache.AddStatic(CachePartitionName, ConnectionStringKey, value);
         }
      }

      #region DbContext Generators

      private static OracleDbContext OracleDbContextGenerator()
      {
         return new OracleDbContext();
      }

      private static SqlServerCeDbContext SqlServerCeDbContextGenerator()
      {
         return new SqlServerCeDbContext();
      }

      #endregion

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

      internal static DbContextBase CreateReadContext()
      {
         var ctx = CreateWriteContext();
         ctx.Configuration.ProxyCreationEnabled = false;
         return ctx;
      }

      internal static DbContextBase CreateWriteContext()
      {
         var ctx = _dbContextGenerator();
         ctx.Database.Initialize(false);
         ctx.Database.Connection.Open();
         return ctx;
      }

      #endregion

      #region Methods that must be used _ONLY_ inside (or for) Unit Tests

      internal static void ChangeDataAccessKindUseOnlyForUnitTestsPlease()
      {
         SetDataAccessKind(DataAccessKind.SqlServerCe);
      }

      internal static void ClearAllTablesUseOnlyInsideUnitTestsPlease()
      {
         switch (AccessKind)
         {
            case DataAccessKind.Oracle:
            case DataAccessKind.PostgreSql:
            case DataAccessKind.SqlServer:
            case DataAccessKind.SqlServerCe:
               using (var trx = new TransactionScope(TransactionScopeOption.Suppress))
               using (var ctx = CreateWriteContext())
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

      #endregion

      #region Private Methods

      private static void SetDataAccessKind(DataAccessKind kind)
      {
         Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(DataAccessKind), kind));
         AccessKind = kind;
         switch (kind)
         {
            case DataAccessKind.MongoDb:
               _dbManagerInstance = new MongoDbManager();
               _logManagerInstance = new MongoLogManager();
               _securityManagerInstance = new MongoSecurityManager();
               break;
            case DataAccessKind.Oracle:
               _dbManagerInstance = new OracleDbManager();
               _dbContextGenerator = OracleDbContextGenerator;
               _logManagerInstance = new SqlLogManager();
               _queryManagerInstance = new SqlQueryManager();
               _securityManagerInstance = new SqlSecurityManager();
               break;
            case DataAccessKind.PostgreSql:
               _logManagerInstance = new SqlLogManager();
               _queryManagerInstance = new SqlQueryManager();
               _securityManagerInstance = new SqlSecurityManager();
               break;
            case DataAccessKind.Rest:
               _logManagerInstance = new RestLogManager();
               _queryManagerInstance = new RestQueryManager();
               _securityManagerInstance = new RestSecurityManager();
               break;
            case DataAccessKind.SqlServer:
               _logManagerInstance = new SqlLogManager();
               _queryManagerInstance = new SqlQueryManager();
               _securityManagerInstance = new SqlSecurityManager();
               break;
            case DataAccessKind.SqlServerCe:
               _dbManagerInstance = new SqlServerCeDbManager();
               _dbContextGenerator = SqlServerCeDbContextGenerator;
               _logManagerInstance = new SqlLogManager();
               _queryManagerInstance = new SqlQueryManager();
               _securityManagerInstance = new SqlSecurityManager();
               break;
         }
      }

      #endregion
   }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Rest;
using Finsa.Caravan.DataAccess.Sql;
using Finsa.Caravan.DataAccess.Sql.Oracle;
using Finsa.Caravan.DataAccess.Sql.SqlServerCe;

namespace Finsa.Caravan.DataAccess
{
   public static class Db
   {
      private static LogManagerBase LogManagerInstance;
      private static QueryManagerBase QueryManagerInstance;
      private static ISecurityManager SecurityManagerInstance;
      private static DbManagerBase DbManagerInstance;
      private static Func<DbContextBase> DbContextGenerator;

      static Db()
      {
         SetDataAccessKind(Configuration.Instance.DataAccessKind);
      }

      #region Public Properties

      public static IDbManager Manager
      {
         get { return DbManagerInstance; }
      }

      public static ILogManager Logger
      {
         get { return LogManagerInstance; }
      }

      public static IQueryManager Query
      {
         get { return QueryManagerInstance; }
      }

      public static ISecurityManager Security
      {
         get { return SecurityManagerInstance; }
      }

      #endregion

      #region Context Generators

      private static OracleDbContext OracleDbContextGenerator()
      {
         return new OracleDbContext();
      }

      private static SqlServerCeDbContext SqlServerCeDbContextGenerator()
      {
         return new SqlServerCeDbContext();
      }

      #endregion

      public static List<T> ToLogAndList<T>(this IQueryable<T> queryable)
      {
         var stopwatch = new Stopwatch();
         stopwatch.Start();
         var list = queryable.ToList();
         stopwatch.Stop();

         Task.Factory.StartNew(() =>
         {
            var logEntry = queryable.ToString();
            var milliseconds = stopwatch.ElapsedMilliseconds;
            Logger.LogDebug<IDbManager>("EF generated query", logEntry, "Logging and timing the query", new[]
            {
               CKeyValuePair.Create("milliseconds", milliseconds.ToString(CultureInfo.InvariantCulture))
            });
         });

         return list;
      }

      internal static DbContextBase CreateReadContext()
      {
         var ctx = DbContextGenerator();
         ctx.Database.Initialize(false);
         ctx.Database.Connection.Open();
         ctx.Configuration.ProxyCreationEnabled = false;
         return ctx;
      }

      internal static DbContextBase CreateWriteContext()
      {
         var ctx = DbContextGenerator();
         ctx.Database.Initialize(false);
         ctx.Database.Connection.Open();
         return ctx;
      }

      #region Methods that must be used _ONLY_ inside (or for) Unit Tests

      internal static void ChangeDataAccessKindUseOnlyForUnitTestsPlease()
      {
         SetDataAccessKind(DataAccessKind.SqlServerCe);
      }

      internal static void ClearAllTablesUseOnlyInsideUnitTestsPlease()
      {
         using (var ctx = CreateWriteContext())
         {
            ctx.BeginTransaction();
            ctx.LogEntries.RemoveRange(ctx.LogEntries.ToList());
            ctx.LogSettings.RemoveRange(ctx.LogSettings.ToList());
            ctx.SecEntries.RemoveRange(ctx.SecEntries.ToList());
            ctx.SecObjects.RemoveRange(ctx.SecObjects.ToList());
            ctx.SecContexts.RemoveRange(ctx.SecContexts.ToList());
            ctx.SecUsers.RemoveRange(ctx.SecUsers.ToList());
            ctx.SecGroups.RemoveRange(ctx.SecGroups.ToList());
            ctx.SecApps.RemoveRange(ctx.SecApps.ToList());
            ctx.SaveChanges();
         }
      }

      #endregion

      #region Private Methods

      private static void SetDataAccessKind(DataAccessKind kind)
      {
         switch (kind)
         {
            case DataAccessKind.Dummy:
               LogManagerInstance = new DummyLogManager();
               QueryManagerInstance = new DummyQueryManager();
               SecurityManagerInstance = new DummySecurityManager();
               break;
            case DataAccessKind.Oracle:
               DbManagerInstance = new OracleDbManager();
               DbContextGenerator = OracleDbContextGenerator;
               LogManagerInstance = new SqlLogManager();
               QueryManagerInstance = new SqlQueryManager();
               SecurityManagerInstance = new SqlSecurityManager();
               break;
            case DataAccessKind.Postgres:
               LogManagerInstance = new SqlLogManager();
               QueryManagerInstance = new SqlQueryManager();
               SecurityManagerInstance = new SqlSecurityManager();
               break;
            case DataAccessKind.Rest:
               LogManagerInstance = new RestLogManager();
               QueryManagerInstance = new RestQueryManager();
               SecurityManagerInstance = new RestSecurityManager();
               break;
            case DataAccessKind.SqlServer:
               LogManagerInstance = new SqlLogManager();
               QueryManagerInstance = new SqlQueryManager();
               SecurityManagerInstance = new SqlSecurityManager();
               break;
            case DataAccessKind.SqlServerCe:
               DbManagerInstance = new SqlServerCeDbManager();
               DbContextGenerator = SqlServerCeDbContextGenerator;
               LogManagerInstance = new SqlLogManager();
               QueryManagerInstance = new SqlQueryManager();
               SecurityManagerInstance = new SqlSecurityManager();
               break;
         }
      }

      #endregion
   }
}
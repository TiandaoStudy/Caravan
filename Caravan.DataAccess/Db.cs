using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Dummy;
using Finsa.Caravan.DataAccess.Rest;
using Finsa.Caravan.DataAccess.Sql;
using Finsa.Caravan.DataAccess.Sql.Oracle;
using Finsa.Caravan.DataAccess.Sql.SqlServerCe;

namespace Finsa.Caravan.DataAccess
{
   public static class Db
   {
      private static readonly LogManagerBase LogManagerInstance;
      private static readonly QueryManagerBase QueryManagerInstance;
      private static readonly ISecurityManager SecurityManagerInstance;
      private static readonly DbManagerBase DbManagerInstance;
      private static readonly Func<DbContextBase> DbContextGenerator;

      static Db()
      {
         switch (Configuration.Instance.DataAccessKind)
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

      internal static void ClearAllTablesUseOnlyInsideUnitTestsPlease()
      {
         using (var ctx = CreateWriteContext())
         {
            var trx = ctx.BeginTransaction();
            try
            {
               ctx.LogEntries.RemoveRange(ctx.LogEntries.ToList());
               ctx.LogSettings.RemoveRange(ctx.LogSettings.ToList());
               ctx.SaveChanges();
            }
            catch
            {
               trx.Rollback();
            }
         }
      }
   }
}
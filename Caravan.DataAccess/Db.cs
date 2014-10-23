using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Sql;
using Finsa.Caravan.DataAccess.Sql.Oracle;
using Finsa.Caravan.DataAccess.Sql.SqlServerCe;

namespace Finsa.Caravan.DataAccess
{
   public static class Db
   {
      private static readonly ILogManager LogManagerInstance = new SqlLogManager();
      private static readonly ISecurityManager SecurityManagerInstance = new SqlSecurityManager();
      private static readonly IDbManager DbManagerInstance;
      private static readonly Func<DbContextBase> DbContextGenerator;

      static Db()
      {
         switch (Configuration.Instance.DataAccessKind)
         {
            case DataAccessKind.Dummy:
               break;
            case DataAccessKind.Oracle:
               DbManagerInstance = new OracleDbManager();
               DbContextGenerator = OracleDbContextGenerator;
               break;
            case DataAccessKind.SqlServer:
               break;
            case DataAccessKind.SqlServerCe:
               DbManagerInstance = new SqlServerCeDbManager();
               DbContextGenerator = SqlServerCeDbContextGenerator;
               break;
         }
      }

      public static IDbManager Manager
      {
         get { return DbManagerInstance; }
      }

      public static ILogManager Logger
      {
         get { return LogManagerInstance; }
      }

      public static ISecurityManager Security
      {
         get { return SecurityManagerInstance; }
      }

      internal static DbContextBase CreateContext()
      {
         return DbContextGenerator();
      }

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
   }
}
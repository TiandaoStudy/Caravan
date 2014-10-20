using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Direct;
using Finsa.Caravan.DataAccess.Direct.Oracle;
using Finsa.Caravan.DataAccess.Direct.SqlServerCe;

namespace Finsa.Caravan.DataAccess
{
   public static class Db
   {
      private static readonly ILogManager LogManagerInstance = new DirectLogManager();
      private static readonly ISecurityManager SecurityManagerInstance = new DirectSecurityManager();
      private static readonly IDbManager DbManagerInstance;
      private static readonly Func<DbContextBase> DbContextGenerator;

      static Db()
      {
         switch (Configuration.Instance.DatabaseKind)
         {
            case DatabaseKind.Dummy:
               break;
            case DatabaseKind.Oracle:
               DbManagerInstance = new OracleDbManager();
               DbContextGenerator = OracleDbContextGenerator;
               break;
            case DatabaseKind.SqlServer:
               break;
            case DatabaseKind.SqlServerCe:
               DbManagerInstance = new SqlServerCeDbManager();
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
               GKeyValuePair.Create("milliseconds", milliseconds.ToString(CultureInfo.InvariantCulture))
            });
         });

         return list;
      }
   }
}
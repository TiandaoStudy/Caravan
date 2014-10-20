using System;
using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Direct;
using Finsa.Caravan.DataAccess.Direct.Oracle;
using Finsa.Caravan.DataAccess.Oracle;

namespace Finsa.Caravan.DataAccess
{
   public static class Db
   {
      private static readonly ILogger LoggerInstance = new DirectLogger();
      private static readonly ISecurityManager SecurityManagerInstance = new DirectSecurityManager();
      private static readonly IQueryExecutor QueryExecutorInstance;
      private static readonly Func<DbContextBase> DbContextGenerator;

      static Db()
      {
         switch (Configuration.Instance.DatabaseKind)
         {
            case DatabaseKind.Dummy:
               break;
            case DatabaseKind.Oracle:
               QueryExecutorInstance = new OracleQueryExecutor();
               DbContextGenerator = OracleDbContextGenerator;
               break;
            case DatabaseKind.SqlServer:
               break;
            case DatabaseKind.SqlServerCe:
               break;
         }
      }

      public static IQueryExecutor QueryExecutor
      {
         get { return QueryExecutorInstance; }
      }

      public static ILogger Logger
      {
         get { return LoggerInstance; }
      }

      public static ISecurityManager SecurityManager
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
         var logEntry = queryable.ToString();
         // Tenere traccia del tempo
         // Loggare in modo asincrono
         return queryable.ToList();
      }
   }
}
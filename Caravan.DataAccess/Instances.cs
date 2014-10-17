using System;
using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Oracle;

namespace Finsa.Caravan.DataAccess
{
   public static class Db
   {
      private static readonly ILogger LoggerInstance = new OracleLogger();
      private static readonly ISecurityManager SecurityManagerInstance = new OracleSecurityManager();
      private static readonly IQueryExecutor QueryExecutorInstance;
      private static readonly Func<DbContextBase> DbContextGenerator;

      static Db()
      {
         switch (Configuration.Instance.DatabaseVendor)
         {
            case DatabaseVendor.Dummy:
               break;
            case DatabaseVendor.Oracle:
               QueryExecutorInstance = new OracleQueryExecutor();
               DbContextGenerator = OracleDbContextGenerator;
               break;
            case DatabaseVendor.SqlServer:
               break;
            case DatabaseVendor.SqlServerCe:
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

      internal static DbContextBase CreateDbContext()
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
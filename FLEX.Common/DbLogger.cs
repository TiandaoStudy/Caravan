using FLEX.Common.Data;

namespace FLEX.Common
{
   public static class DbLogger
   {
      private static readonly IDbLogger CachedInstance = ServiceLocator.Load<IDbLogger>(Configuration.Instance.DbLoggerTypeInfo);

      public static IDbLogger Instance
      {
         get { return CachedInstance; }
      }
   }

   public static class QueryExecutor
   {
      private static readonly IQueryExecutor CachedInstance = ServiceLocator.Load<IQueryExecutor>(Configuration.Instance.QueryExecutorTypeInfo);

      public static IQueryExecutor Instance
      {
         get { return CachedInstance; }
      }
   }
}

using System;
using System.Configuration;
using Finsa.Caravan.Reflection;

namespace Finsa.Caravan.DataAccess
{
   /// <summary>
   ///   TODO
   /// </summary>
   public static class Logger
   {
      private static readonly ILogger CachedInstance;

      static Logger()
      {
         try
         {
            CachedInstance = ServiceLocator.Load<ILogger>(Configuration.Instance.LoggerTypeInfo);
         }
         catch (Exception ex)
         {
            throw new ConfigurationErrorsException("Could not load the ILogger implementation. See inner exception for more details.", ex);
         }
      }

      public static ILogger Instance
      {
         get { return CachedInstance; }
      }
   }

   /// <summary>
   ///   TODO
   /// </summary>
   public static class QueryExecutor
   {
      private static readonly IQueryExecutor CachedInstance;

      static QueryExecutor()
      {
         try
         {
            CachedInstance = ServiceLocator.Load<IQueryExecutor>(Configuration.Instance.QueryExecutorTypeInfo);
         }
         catch (Exception ex)
         {
            Logger.Instance.LogFatal<IQueryExecutor>(ex, "Loading IQueryExecutor");
            throw new ConfigurationErrorsException("Could not load the IQueryExecutor implementation. See inner exception for more details.", ex);
         }
      }

      public static IQueryExecutor Instance
      {
         get { return CachedInstance; }
      }

      internal static string OracleQuery(string query)
      {
         return String.Format(query, Configuration.Instance.OracleRunner);
      }
   }

   /// <summary>
   ///   TODO
   /// </summary>
   public static class SecurityManager
   {
      private static readonly ISecurityManager CachedInstance;

      static SecurityManager()
      {
         try
         {
            CachedInstance = ServiceLocator.Load<ISecurityManager>(Configuration.Instance.SecurityManagerTypeInfo);
         }
         catch (Exception ex)
         {
            Logger.Instance.LogFatal<ISecurityManager>(ex, "Loading ISecurityManager");
            throw new ConfigurationErrorsException("Could not load the ISecurityManager implementation. See inner exception for more details.", ex);
         }
      }

      public static ISecurityManager Instance
      {
         get { return CachedInstance; }
      }
   }
}
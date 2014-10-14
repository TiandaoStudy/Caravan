﻿using System;
using System.Configuration;
using Finsa.Caravan.Reflection;

namespace FLEX.DataAccess
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
   }
}
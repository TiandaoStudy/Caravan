using System;
using System.Configuration;
using PommaLabs.GRAMPA.Reflection;

namespace FLEX.Common.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbLogger
    {
        private static readonly IDbLogger CachedInstance;

        static DbLogger()
        {
            try {
                CachedInstance = ServiceLocator.Load<IDbLogger>(Configuration.Instance.DbLoggerTypeInfo);
            } catch (Exception ex) {
                throw new ConfigurationErrorsException("Could not load the IDbLogger implementation. See inner exception for more details.", ex);
            }
        }

        public static IDbLogger Instance
        {
            get { return CachedInstance; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class QueryExecutor
    {
        private static readonly IQueryExecutor CachedInstance;

        static QueryExecutor()
        {
            try {
                CachedInstance = ServiceLocator.Load<IQueryExecutor>(Configuration.Instance.QueryExecutorTypeInfo);
            } catch (Exception ex) {
                DbLogger.Instance.LogFatal<IQueryExecutor>(ex, "Loading IQueryExecutor");
                throw new ConfigurationErrorsException("Could not load the IQueryExecutor implementation. See inner exception for more details.", ex);
            }
        }

        public static IQueryExecutor Instance
        {
            get { return CachedInstance; }
        }
    }
}
using System;
using System.Configuration;
using PommaLabs.KVLite;

namespace Finsa.Caravan.DataAccess
{
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "finsa.caravan.dataaccess";
      private const string CachePartitionName = "Caravan.DataAccess";
      private const string ConnectionStringKey = "ConnectionString";
      private const string LoggerTypeInfoKey = "LoggerTypeInfo";
      private const string OracleRunnerKey = "OracleRunner";
      private const string OracleStatementCacheSizeKey = "OracleStatementCacheSize";
      private const string QueryExecutorTypeInfoKey = "QueryExecutorTypeInfo";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      public static Configuration Instance
      {
         get { return CachedInstance; }
      }

      [ConfigurationProperty(LoggerTypeInfoKey, IsRequired = true)]
      public string LoggerTypeInfo
      {
         get { return (string) this[LoggerTypeInfoKey]; }
      }

      [ConfigurationProperty(OracleRunnerKey, IsRequired = false, DefaultValue = "")]
      public string OracleRunner
      {
         get
         {
            var runner = this[OracleRunnerKey] as string;
            return string.IsNullOrWhiteSpace(runner) ? runner : runner + ".";
         }
      }

      [ConfigurationProperty(OracleStatementCacheSizeKey, IsRequired = false, DefaultValue = 10)]
      public int OracleStatementCacheSize
      {
         get { return Convert.ToInt32(this[OracleStatementCacheSizeKey]); }
      }

      [ConfigurationProperty(QueryExecutorTypeInfoKey, IsRequired = true)]
      public string QueryExecutorTypeInfo
      {
         get { return (string) this[QueryExecutorTypeInfoKey]; }
      }
      
      [ConfigurationProperty(ConnectionStringKey, IsRequired = false)]
      public string ConnectionString
      {
         get
         {
            var cachedConnectionString = PersistentCache.DefaultInstance.Get(CachePartitionName, ConnectionStringKey);
            if (cachedConnectionString != null)
            {
               return (string) cachedConnectionString;
            }
            var configConnectionString = (string) this[ConnectionStringKey];
            ConnectionString = configConnectionString;
            return ConnectionString;
         }
         set
         {
            QueryExecutor.Instance.ElaborateConnectionString(ref value);
            PersistentCache.DefaultInstance.AddStatic(CachePartitionName, ConnectionStringKey, value);
         }
      }
   }
}
using System;
using System.Configuration;
using PommaLabs.KVLite;

namespace FLEX.DataAccess
{
    public sealed class Configuration : ConfigurationSection
    {
        private const string SectionName = "FlexDataConfiguration";
        private const string CachePartitionName = "Caravan.DataAccess";
        private const string ConnectionStringKey = "ConnectionString";
        private const string DbLoggerTypeInfoKey = "DbLoggerTypeInfo";
        private const string OracleRunnerKey = "OracleRunner";
        private const string OracleStatementCacheSizeKey = "OracleStatementCacheSize";
        private const string QueryExecutorTypeInfoKey = "QueryExecutorTypeInfo";

        private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

        public static Configuration Instance
        {
            get { return CachedInstance; }
        }

        [ConfigurationProperty(DbLoggerTypeInfoKey, IsRequired = true)]
        public string DbLoggerTypeInfo
        {
            get { return (string)this[DbLoggerTypeInfoKey]; }
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
            get { return (string)this[QueryExecutorTypeInfoKey]; }
        }

        public string ConnectionString
        {
            get { return (string) PersistentCache.DefaultInstance.Get(CachePartitionName, ConnectionStringKey); }
            set
            {
                QueryExecutor.Instance.ElaborateConnectionString(ref value);
                PersistentCache.DefaultInstance.AddStatic(CachePartitionName, ConnectionStringKey, value);
            }
        }
    }
}
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
      private const string DatabaseVendorKey = "DatabaseKind";
      private const string OracleRunnerKey = "OracleRunner";
      private const string OracleStatementCacheSizeKey = "OracleStatementCacheSize";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      public static Configuration Instance
      {
         get { return CachedInstance; }
      }
      
      [ConfigurationProperty(ConnectionStringKey, IsRequired = false)]
      public string ConnectionString
      {
         get
         {
            var cachedConnectionString = PersistentCache.DefaultInstance.Get(CachePartitionName, ConnectionStringKey) as string;
            if (cachedConnectionString != null)
            {
               return cachedConnectionString;
            }
            var configConnectionString = this[ConnectionStringKey] as string;
            ConnectionString = configConnectionString;
            return ConnectionString;
         }
         set
         {
            Db.Manager.ElaborateConnectionString(ref value);
            PersistentCache.DefaultInstance.AddStatic(CachePartitionName, ConnectionStringKey, value);
         }
      }

      [ConfigurationProperty(DatabaseVendorKey, IsRequired = true)]
      public DatabaseKind DatabaseKind
      {
         get { return (DatabaseKind) this[DatabaseVendorKey]; }
      }

      [ConfigurationProperty(OracleRunnerKey, IsRequired = false, DefaultValue = "")]
      public string OracleRunner
      {
         get { return this[OracleRunnerKey] as string; }
      }

      [ConfigurationProperty(OracleStatementCacheSizeKey, IsRequired = false, DefaultValue = 10)]
      public int OracleStatementCacheSize
      {
         get { return Convert.ToInt32(this[OracleStatementCacheSizeKey]); }
      }
   }
}
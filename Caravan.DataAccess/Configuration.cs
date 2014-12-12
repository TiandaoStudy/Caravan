using System;
using System.Configuration;
using PommaLabs.KVLite;

namespace Finsa.Caravan.DataAccess
{
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "Finsa.Caravan.DataAccess";
      private const string CachePartitionName = "Caravan.DataAccess";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      public static Configuration Instance
      {
         get { return CachedInstance; }
      }

      #region Common Settings

      private const string ConnectionStringKey = "ConnectionString";
      private const string DataAccessKindKey = "DataAccessKind";

      [ConfigurationProperty(ConnectionStringKey, IsRequired = false)]
      public string ConnectionString
      {
         get
         {
            var cachedConnectionString = PersistentCache.DefaultInstance.Get(CachePartitionName, ConnectionStringKey) as string;
            var configConnectionString = this[ConnectionStringKey] as string;
            if (String.IsNullOrWhiteSpace(configConnectionString))
            {
               // If connection string is not in the configuration file, then return the cached one, even if empty.
               return cachedConnectionString;
            }
            Db.Manager.ElaborateConnectionString(ref configConnectionString);
            if (configConnectionString == cachedConnectionString)
            {
               // Connection string has _not_ changed, return the cached one.
               return cachedConnectionString;
            }
            // Connection string _has_ changed, update the cached one.
            PersistentCache.DefaultInstance.AddStatic(CachePartitionName, ConnectionStringKey, configConnectionString);
            return configConnectionString;
         }
         set
         {
            Db.Manager.ElaborateConnectionString(ref value);
            PersistentCache.DefaultInstance.AddStatic(CachePartitionName, ConnectionStringKey, value);
         }
      }

      [ConfigurationProperty(DataAccessKindKey, IsRequired = true)]
      public DataAccessKind DataAccessKind
      {
         get { return (DataAccessKind) this[DataAccessKindKey]; }
      }

      #endregion

      #region Oracle Specific

      private const string OracleStatementCacheSizeKey = "OracleStatementCacheSize";
      private const string OracleUserKey = "OracleUser";

      [ConfigurationProperty(OracleStatementCacheSizeKey, IsRequired = false, DefaultValue = 10)]
      public int OracleStatementCacheSize
      {
         get { return Convert.ToInt32(this[OracleStatementCacheSizeKey]); }
      }

      [ConfigurationProperty(OracleUserKey, IsRequired = false, DefaultValue = "")]
      public string OracleUser
      {
         get { return this[OracleUserKey] as string; }
      }

      #endregion

      #region Postgres Specific

      #endregion

      #region Rest Specific

      private const string CaravanRestServiceUrlKey = "CaravanRestServiceUrl";

      [ConfigurationProperty(CaravanRestServiceUrlKey, IsRequired = false, DefaultValue = "")]
      public string CaravanRestServiceUrl
      {
         get
         {
            var url = this[CaravanRestServiceUrlKey] as string;
            if (!String.IsNullOrWhiteSpace(url) && !url.EndsWith("/"))
            {
               return url + "/";
            }
            return url;
         }
      }

      #endregion

      #region Internal Settings

      private string _currentAppName;

      /// <summary>
      ///   Needed because the REST service has to "act" as a proxy for apps.
      /// </summary>
      internal string CurrentAppName
      {
         get { return _currentAppName ?? Common.Configuration.Instance.ApplicationName; }
         set { _currentAppName = value.ToLower(); }
      }

      #endregion
   }
}
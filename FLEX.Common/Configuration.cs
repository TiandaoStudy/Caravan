using System.Configuration;
using PommaLabs.KVLite;

namespace FLEX.Common
{
   /// <summary>
   ///   TODO
   /// </summary>
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "FlexCommonConfiguration";
      private const string CachePartitionName = "FLEX.Common";
      private const string ApplicationNameKey = "ApplicationName";
      private const string ConnectionStringKey = "ConnectionString";
      private const string DbLoggerTypeInfoKey = "DbLoggerTypeInfo";
      private const string ErrorManagerTypeInfoKey = "ErrorManagerTypeInfo";
      private const string QueryExecutorTypeInfoKey = "QueryExecutorTypeInfo";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      /// <summary>
      ///   TODO
      /// </summary>
      public static Configuration Instance 
      {
         get { return CachedInstance; }
      }

      [ConfigurationProperty(ApplicationNameKey, IsRequired = true)]
      public string ApplicationName
      {
         get { return (string) this[ApplicationNameKey]; }
      }

      [ConfigurationProperty(DbLoggerTypeInfoKey, IsRequired = true)]
      public string DbLoggerTypeInfo
      {
         get { return (string) this[DbLoggerTypeInfoKey]; }
      }

      [ConfigurationProperty(ErrorManagerTypeInfoKey, IsRequired = true)]
      public string ErrorManagerTypeInfo
      {
          get { return (string) this[ErrorManagerTypeInfoKey]; }
      }

      [ConfigurationProperty(QueryExecutorTypeInfoKey, IsRequired = true)]
      public string QueryExecutorTypeInfo
      {
         get { return (string) this[QueryExecutorTypeInfoKey]; }
      }

       public string ConnectionString
       {
           get { return (string) PersistentCache.DefaultInstance.Get(CachePartitionName, ConnectionStringKey); }
           set { PersistentCache.DefaultInstance.AddStatic(CachePartitionName, ConnectionStringKey, value); }
       }
   }
}

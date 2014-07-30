using System.Configuration;

namespace FLEX.Common
{
   /// <summary>
   ///   TODO
   /// </summary>
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "FlexCommonConfiguration";
      private const string ApplicationNameKey = "ApplicationName";
      private const string DbLoggerTypeInfoKey = "DbLoggerTypeInfo";
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

      [ConfigurationProperty(QueryExecutorTypeInfoKey, IsRequired = true)]
      public string QueryExecutorTypeInfo
      {
         get { return (string) this[QueryExecutorTypeInfoKey]; }
      }
   }
}

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

      public string ConnectionString { get; set; }

      [ConfigurationProperty(DbLoggerTypeInfoKey, IsRequired = true)]
      public string DbLoggerTypeInfo
      {
         get { return (string) this[DbLoggerTypeInfoKey]; }
      }
   }
}

using System.Configuration;

namespace FLEX.Web
{
   /// <summary>
   /// 
   /// </summary>
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "FlexWebConfiguration";
      private const string CheckSecurityKey = "CheckSecurity";
      private const string PageManagerTypeInfoKey = "PageManagerTypeInfo";
      private const string SessionExpiredPageUrlKey = "SessionExpiredPageUrl";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      public static Configuration Instance 
      {
         get { return CachedInstance; }
      }

      [ConfigurationProperty(CheckSecurityKey, IsRequired = false, DefaultValue = true)]
      public bool CheckSecurity
      {
         get { return (bool) this[CheckSecurityKey]; }
      }

      [ConfigurationProperty(PageManagerTypeInfoKey, IsRequired = true)]
      public string PageManagerTypeInfo
      {
         get { return (string) this[PageManagerTypeInfoKey]; }
      }

      [ConfigurationProperty(SessionExpiredPageUrlKey, IsRequired = true)]
      public string SessionExpiredPageUrl
      {
         get { return (string) this[SessionExpiredPageUrlKey]; }
      }
   }
}

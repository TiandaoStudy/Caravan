using System.Configuration;

namespace FLEX.Web
{
   /// <summary>
   /// 
   /// </summary>
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "FlexWebConfiguration";
      private const string PageManagerTypeInfoKey = "PageManagerTypeInfo";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      public static Configuration Instance 
      {
         get { return CachedInstance; }
      }

      [ConfigurationProperty(PageManagerTypeInfoKey, IsRequired = true)]
      public string PageManagerTypeInfo
      {
         get { return (string) this[PageManagerTypeInfoKey]; }
      }
   }
}

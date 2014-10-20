using System.Configuration;
using System.Globalization;

namespace FLEX.WebForms
{
   /// <summary>
   /// 
   /// </summary>
   public sealed class Configuration : ConfigurationSection
   {
      private const string SectionName = "FlexWebConfiguration";
      private const string AjaxLookupsXmlPathKey = "AjaxLookupsXmlPath";
      private const string CheckSecurityKey = "CheckSecurity";
      private const string ControlExtendersFolderKey = "ControlExtendersFolder";
      private const string DynamicReportsFolderKey = "DynamicReportsFolder";
      private const string EnableOutputCompressionKey = "EnableOutputCompression";
      private const string EnableOutputMinificationKey = "EnableOutputMinification";
      private const string ErrorManagerTypeInfoKey = "ErrorManagerTypeInfo";
      private const string LookupsXmlPathKey = "AjaxLookupsXmlPath";
      private const string MenuBarXmlPathKey = "MenuBarXmlPath";
      private const string PageManagerTypeInfoKey = "PageManagerTypeInfo";
      private const string SecurityManagerTypeInfoKey = "SecurityManagerTypeInfo";
      private const string SessionExpiredPageUrlKey = "SessionExpiredPageUrl";

      private static readonly Configuration CachedInstance = ConfigurationManager.GetSection(SectionName) as Configuration;

      public static Configuration Instance 
      {
         get { return CachedInstance; }
      }

      [ConfigurationProperty(AjaxLookupsXmlPathKey, IsRequired = false)]
      public string AjaxLookupsXmlPath
      {
         get { return this[AjaxLookupsXmlPathKey] as string; }
      }

      [ConfigurationProperty(CheckSecurityKey, IsRequired = false, DefaultValue = true)]
      public bool CheckSecurity
      {
         get { return (bool) this[CheckSecurityKey]; }
      }

      [ConfigurationProperty(ControlExtendersFolderKey, IsRequired = true)]
      public string ControlExtendersFolder
      {
         get { return this[ControlExtendersFolderKey] as string; }
      }

      [ConfigurationProperty(DynamicReportsFolderKey, IsRequired = true)]
      public string DynamicReportsFolder
      {
         get { return (string) this[DynamicReportsFolderKey]; }
      }

      [ConfigurationProperty(EnableOutputCompressionKey, IsRequired = false, DefaultValue = true)]
      public bool EnableOutputCompression
      {
         get { return (bool) this[EnableOutputCompressionKey]; }
      }

      [ConfigurationProperty(EnableOutputMinificationKey, IsRequired = false, DefaultValue = true)]
      public bool EnableOutputMinification
      {
         get { return (bool) this[EnableOutputMinificationKey]; }
      }

      [ConfigurationProperty(ErrorManagerTypeInfoKey, IsRequired = true)]
      public string ErrorManagerTypeInfo
      {
         get { return (string) this[ErrorManagerTypeInfoKey]; }
      }

      [ConfigurationProperty(LookupsXmlPathKey, IsRequired = false)]
      public string LookupsXmlPath
      {
         get { return this[LookupsXmlPathKey] as string; }
      }

      [ConfigurationProperty(MenuBarXmlPathKey, IsRequired = true)]
      public string MenuBarXmlPath
      {
         get { return this[MenuBarXmlPathKey] as string; }
      }

      [ConfigurationProperty(PageManagerTypeInfoKey, IsRequired = true)]
      public string PageManagerTypeInfo
      {
         get { return (string) this[PageManagerTypeInfoKey]; }
      }

      [ConfigurationProperty(SecurityManagerTypeInfoKey, IsRequired = true)]
      public string SecurityManagerTypeInfo
      {
         get { return (string) this[SecurityManagerTypeInfoKey]; }
      }

      [ConfigurationProperty(SessionExpiredPageUrlKey, IsRequired = true)]
      public string SessionExpiredPageUrl
      {
         get { return (string) this[SessionExpiredPageUrlKey]; }
      }

      #region Internal Settings

      internal static CultureInfo CurrentUserCulture
      {
         get { return CultureInfo.CreateSpecificCulture("it-IT"); }
      }

      internal static string ExceptionSessionKey
      {
         get { return "C895F535-DA46-478c-ACD3-9E21B69A76D6"; }
      }

      #endregion
   }
}

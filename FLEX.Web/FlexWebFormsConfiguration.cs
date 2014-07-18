using System.Configuration;

namespace FLEX.Web.WebForms
{
   /// <summary>
   /// 
   /// </summary>
   public sealed class FlexWebFormsConfiguration : ConfigurationSection
   {
      private static readonly FlexWebFormsConfiguration _instance = (FlexWebFormsConfiguration) ConfigurationManager.GetSection("WebConfiguration");

      public static FlexWebFormsConfiguration Instance 
      {
         get { return _instance; }
      }

      [ConfigurationProperty("SSOReturnUrl", IsRequired = true)]
      public string SSOReturnUrl
      {
         get { return (string)this["SSOReturnUrl"]; }
      }
   }
}

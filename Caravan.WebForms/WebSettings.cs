using System;
using System.Collections.Specialized;
using System.Configuration;

namespace FLEX.Web
{
   /// <summary>
   ///   
   /// </summary>
   public static class WebSettings
   {
      private static readonly NameValueCollection AppSettings = ConfigurationManager.AppSettings;

      public static int CacheManager_DefaultMinutes
      {
         get { return Convert.ToInt32(AppSettings["FLEX.Web.CacheManager.DefaultMinutes"]); }
      }
   }
}
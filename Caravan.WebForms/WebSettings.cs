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

      #region UserControls.MenuBar

      public static string UserControls_MenuBar_BrandClick
      {
         get { return AppSettings["FLEX.Web.UserControls.MenuBar.BrandClick"]; }
      }

      public static string UserControls_MenuBar_HomeClick
      {
         get { return AppSettings["FLEX.Web.UserControls.MenuBar.HomeClick"]; }
      }

      public static string UserControls_MenuBar_InfoClick
      {
         get { return AppSettings["FLEX.Web.UserControls.MenuBar.InfoClick"]; }
      }

      public static string UserControls_MenuBar_LogoutClick
      {
         get { return AppSettings["FLEX.Web.UserControls.MenuBar.LogoutClick"]; }
      }

      #endregion
   }
}
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;

namespace FLEX.Web
{
   /// <summary>
   ///   
   /// </summary>
   public static class WebSettings
   {
      private static readonly NameValueCollection AppSettings = ConfigurationManager.AppSettings;

      public static string AjaxLookup_DefaultResultCount
      {
         get { return AppSettings["AjaxLookup_DefaultResultCount"]; }
      }

      public static string AjaxLookup_QueryFilterToken
      {
         get { return AppSettings["AjaxLookup_QueryFilterToken"]; }
      }

      public static string AjaxLookup_ResultCountToken
      {
         get { return AppSettings["AjaxLookup_ResultCountToken"]; }
      }

      public static string AjaxLookup_TokenEnd
      {
         get { return AppSettings["AjaxLookup_TokenEnd"]; }
      }

      public static string AjaxLookup_TokenStart
      {
         get { return AppSettings["AjaxLookup_TokenStart"]; }
      }

      public static string AjaxLookup_UserQueryToken
      {
         get { return AppSettings["AjaxLookup_UserQueryToken"]; }
      }

      public static string AjaxLookup_XmlPath
      {
         get { return AppSettings["AjaxLookup_XmlPath"]; }
      }

      public static string Lookup_XmlPath
      {
         get { return AppSettings["Lookup_XmlPath"]; }
      }

      public static string MenuBar_XmlPath
      {
         get { return AppSettings["MenuBar_XmlPath"]; }
      }

      public static CultureInfo CurrentUserCulture
      {
         get { return CultureInfo.CreateSpecificCulture("it-IT"); }
      }

      public static string QueryExecutors
      {
         get { return AppSettings["QueryExecutors"]; }
      }

      public static string Templates_ImagesPath
      {
         get { return AppSettings["Templates_ImagesPath"]; }
      }

      public static int CacheManager_DefaultMinutes
      {
         get { return Convert.ToInt32(AppSettings["FLEX.Web.CacheManager.DefaultMinutes"]); }
      }

      public static string ProjectName
      {
         get { return AppSettings["FLEX.Web.ProjectName"]; }
      }

      public static string UserControls_Ajax_ErrorHandler_ErrorManagerInfo
      {
         get { return AppSettings["FLEX.Web.UserControls.Ajax.ErrorHandler.ErrorManagerInfo"]; }
      }

      public static string UserControls_Ajax_ErrorHandler_ExceptionSessionKey
      {
         get { return AppSettings["FLEX.Web.UserControls.Ajax.ErrorHandler.ExceptionSessionKey"]; }
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
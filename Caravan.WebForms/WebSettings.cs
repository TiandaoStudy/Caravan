﻿using System;
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

      public static int CacheManager_DefaultMinutes
      {
         get { return Convert.ToInt32(AppSettings["FLEX.Web.CacheManager.DefaultMinutes"]); }
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
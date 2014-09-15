﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;

namespace FLEX.Common
{
   public static class CommonSettings
   {
      private static readonly NameValueCollection AppSettings = ConfigurationManager.AppSettings;
      private static readonly List<string> EmptyStringListInstance = new List<string>();

      public static string AjaxLookup_TokenStart
      {
         get { return AppSettings["AjaxLookup_TokenStart"]; }
      }

      #region Utilities

      public static List<string> EmptyStringList
      {
         get { return EmptyStringListInstance; }
      }

      public static string MapPath(params string[] hints)
      {
         return hints.Aggregate(AppDomain.CurrentDomain.BaseDirectory, Path.Combine);
      }

      #endregion
   }
}
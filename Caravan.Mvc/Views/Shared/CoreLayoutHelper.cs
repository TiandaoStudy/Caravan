﻿using System;
using System.Web;

namespace Finsa.Caravan.Mvc.Views.Shared
{
   public static class CoreLayoutHelper
   {
      private static readonly string CachedRootPath;
      private static readonly string CachedCaravanPath;
      private static readonly string CachedMyCaravanPath;
      private static readonly Random Random = new Random();

      static CoreLayoutHelper()
      {
         CachedRootPath = FullyQualifiedApplicationPath;
         CachedCaravanPath = CachedRootPath + "Caravan.Mvc";
         CachedMyCaravanPath = CachedCaravanPath + "/My";
      }

      #region Public Properties

      public static string RootPath
      {
         get { return CachedRootPath; }
      }

      public static string CaravanPath
      {
         get { return CachedCaravanPath; }
      }

      public static string MyCaravanPath
      {
         get { return CachedMyCaravanPath; }
      }

      public static string HeadSection
      {
         get { return "HeadSection"; }
      }

      public static string DoNotCacheTag
      {
         get
         {
            const int maxInt = 10000000;
            const string tagFmt = "?__DO_NOT_CACHE__={0:0000000}"; // Same zeroes as maxInt
            var randInt = Random.Next(0, maxInt);
            return String.Format(tagFmt, randInt);
         }
      }

      #endregion

      #region Internal Properties

      /// <summary>
      ///   Taken here: http://devio.wordpress.com/2009/10/19/get-absolut-url-of-asp-net-application/
      /// </summary>
      internal static string FullyQualifiedApplicationPath
      {
         get
         {
            //Return variable declaration
            var appPath = String.Empty;

            //Getting the current context of HTTP request
            var context = HttpContext.Current;

            //Checking the current context content
            if (context != null)
            {
               //Formatting the fully qualified website url/name
               appPath = string.Format("{0}://{1}{2}{3}",
                  context.Request.Url.Scheme,
                  context.Request.Url.Host,
                  context.Request.Url.Port == 80
                     ? string.Empty
                     : ":" + context.Request.Url.Port,
                  context.Request.ApplicationPath);
            }

            if (!appPath.EndsWith("/"))
            {
               appPath += "/";
            }
            return appPath;
         }
      }

      #endregion
   }
}
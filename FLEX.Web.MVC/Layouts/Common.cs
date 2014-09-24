﻿using System;
using System.Web;

namespace FLEX.Web.MVC.Layouts
{
   public static class Common
   {
      private static readonly string CachedRootPath;
      private static readonly string CachedFlexPath;
      private static readonly string CachedMyFlexPath;
      private static readonly Random Random = new Random();

      static Common()
      {
         CachedRootPath = FullyQualifiedApplicationPath;
         CachedFlexPath = CachedRootPath + "FLEX";
         CachedMyFlexPath = CachedRootPath + "MyFLEX";
      }

      public static string RootPath
      {
         get { return CachedRootPath; }
      }

      public static string FlexPath
      {
         get { return CachedFlexPath; }
      }

      public static string MyFlexPath
      {
         get { return CachedMyFlexPath; }
      }

      public static string NoCacheTag
      {
         get
         {
            const int maxInt = 10000000;
            const string tagFmt = "?__NOCACHE__={0:0000000}"; // Same zeroes as maxInt
            var randInt = Random.Next(0, maxInt);
            return String.Format(tagFmt, randInt);
         }
      }

      /// <summary>
      ///   Taken here: http://devio.wordpress.com/2009/10/19/get-absolut-url-of-asp-net-application/
      /// </summary>
      private static string FullyQualifiedApplicationPath
      {
         get
         {
            //Return variable declaration
            string appPath = null;

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
               appPath += "/";
            return appPath;
         }
      }
   }
}
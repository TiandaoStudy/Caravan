﻿using System;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Finsa.Caravan.DataAccess;
using PommaLabs.KVLite;

namespace Finsa.Caravan.Mvc
{
   public sealed class GlobalHelper
   {
      private GlobalHelper()
      {
         throw new InvalidOperationException();
      }
      
      public static void Application_Start(string connectionString = null)
      {
         // Sets the default connection string, if variable has a value.
         if (!String.IsNullOrWhiteSpace(connectionString))
         {
            DataAccess.Configuration.Instance.ConnectionString = connectionString;
         }

         // Run vacuum on the persistent cache. It should be put AFTER the connection string is set,
         // since that string it stored on the cache itself and we do not want conflicts, right?
         PersistentCache.DefaultInstance.VacuumAsync();

         // Allow looks views up in ~/Caravan.Mvc/Views directory.
         var razorEngine = ViewEngines.Engines.OfType<RazorViewEngine>().First();
         razorEngine.ViewLocationFormats = razorEngine.ViewLocationFormats.Concat(new [] 
         { 
            "~/Caravan.Mvc/Views/{1}/{0}.cshtml"
         }).ToArray();
      }

      public static void Application_PreSendRequestHeaders(object sender, EventArgs args)
      {
         // Ensures that, if GZip/Deflate Encoding is applied, then that headers are set.
         // Also works when error occurs if filters are still active.
         if (HttpContext.Current != null && HttpContext.Current.Response != null)
         {
            var response = HttpContext.Current.Response;
            if (response.Filter is GZipStream && response.Headers["Content-encoding"] != "gzip")
            {
               response.AppendHeader("Content-encoding", "gzip");
            }  
            else if (response.Filter is DeflateStream && response.Headers["Content-encoding"] != "deflate")
            {
               response.AppendHeader("Content-encoding", "deflate");
            }
         }
      }

      public static void Application_Error(object sender, EventArgs args)
      {
         if (HttpContext.Current == null)
         {
            return;
         }

         // Removes any special filtering, especially GZip filtering.
         // Assigning the response filter to a dummy variable is required
         // in order to avoid a .NET issue which is triggered by setting
         // the Filter property, without reading it first.
         var dummy = HttpContext.Current.Response.Filter;
         HttpContext.Current.Response.Filter = null;

         // Logs the error into the DB.
         Db.Logger.LogFatal<GlobalHelper>(HttpContext.Current.Server.GetLastError());
      }
   }
}
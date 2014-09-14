using System;
using System.IO.Compression;
using System.Web;
using FLEX.Common.Data;
using PommaLabs.KVLite;

namespace FLEX.Web
{
   public sealed class GlobalHelper
   {
      private GlobalHelper()
      {
         throw new InvalidOperationException();
      }
      
      public static void Application_Start(object sender, EventArgs args, string connectionString)
      {
         // Sets the default connection string.
         Common.Configuration.Instance.ConnectionString = connectionString;

         // Run vacuum on the persistent cache. It should be put AFTER the connection string is set,
         // since that string it stored on the cache itself and we do not want conflicts, right?
         PersistentCache.DefaultInstance.VacuumAsync();
      }

      public static void Application_PreSendRequestHeaders(object sender, EventArgs args)
      {
         // Ensures that if GZip/Deflate Encoding is applied that headers are set.
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
         // Removes any special filtering, especially GZip filtering.
          // Assigning the response filter to a dummy variable is required
          // in order to avoid a .NET issue which is triggered by setting
          // the Filter property, without reading it first.
          var dummy = HttpContext.Current.Response.Filter;
         HttpContext.Current.Response.Filter = null;

         // Logs the error into the DB.
         DbLogger.Instance.LogFatal<GlobalHelper>(HttpContext.Current.Server.GetLastError());
      }
   }
}

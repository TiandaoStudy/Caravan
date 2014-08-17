using System;
using System.Web;
using FLEX.Common.Data;
using KVLite;

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

      public static void Application_Error(object sender, EventArgs args)
      {
         // Removes any special filtering, especially GZip filtering.
         HttpContext.Current.Response.Filter = null;

         // Logs the error into the DB.
         DbLogger.Instance.LogFatal<GlobalHelper>("Application_Error", HttpContext.Current.Server.GetLastError());
      }
   }
}

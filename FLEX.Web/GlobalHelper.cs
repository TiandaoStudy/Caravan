using System;
using System.Web;
using FLEX.Common.Data;

namespace FLEX.Web
{
   public sealed class GlobalHelper
   {
      private GlobalHelper()
      {
         throw new InvalidOperationException();
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

using System;
using System.Web;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.RestService.Properties;

namespace Finsa.Caravan.RestService
{
   public class Global : HttpApplication
   {

      protected void Application_Start(object sender, EventArgs e)
      {
         Db.Logger.LogInfoAsync<Global>("Service started", userName: Settings.Default.LoggerIdentity);
      }

      protected void Session_Start(object sender, EventArgs e)
      {

      }

      protected void Application_BeginRequest(object sender, EventArgs e)
      {

      }

      protected void Application_AuthenticateRequest(object sender, EventArgs e)
      {

      }

      protected void Application_Error(object sender, EventArgs e)
      {

      }

      protected void Session_End(object sender, EventArgs e)
      {

      }

      protected void Application_End(object sender, EventArgs e)
      {
         Db.Logger.LogInfoAsync<Global>("Service stopped", userName: Settings.Default.LoggerIdentity);
      }
   }
}
using System;
using System.Configuration;
using System.Web;
using Finsa.Caravan.WebForms;
using System.Collections.Generic;

namespace FLEX.Sample.WebUI
{
   public class Global : HttpApplication
   {
      protected void Application_Start(object sender, EventArgs args)
      {
         //var connectionStringFmt = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
         //var northwindPath = HttpContext.Current.Server.MapPath("~/bin/Northwind.sdf");
         //GlobalHelper.Application_Start(sender, args, String.Format(connectionStringFmt, northwindPath));
         GlobalHelper.Application_Start(sender, args, ConfigurationManager.ConnectionStrings["ASCESI"].ConnectionString, Application);

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

      protected void Application_PreSendRequestHeaders(object sender, EventArgs args)
      {
         // Required by FLEX, please keep it here.
         GlobalHelper.Application_PreSendRequestHeaders(sender, args);
      }

      protected void Application_Error(object sender, EventArgs args)
      {
         // Required by FLEX, please keep it here.
         GlobalHelper.Application_Error(sender, args);
      }

      protected void Session_End(object sender, EventArgs e)
      {
         
      }

      protected void Application_End(object sender, EventArgs e)
      {
      }
   }
}
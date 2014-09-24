﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using RazorGenerator.Mvc;
using PreApplicationStartCode = System.Web.Mvc.PreApplicationStartCode;

namespace Sample.WebUI.MVC
{
   // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
   // visit http://go.microsoft.com/?LinkId=9394801
   public class MvcApplication : System.Web.HttpApplication
   {
      protected void PreApplication_Start()
      {
         var engine = new PrecompiledMvcEngine(typeof(PreApplicationStartCode).Assembly);

    ViewEngines.Engines.Add(engine);

    VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
      }

      protected void Application_Start()
      {
         AreaRegistration.RegisterAllAreas();

         GlobalConfiguration.Configure(WebApiConfig.Register);
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
      }
   }
}
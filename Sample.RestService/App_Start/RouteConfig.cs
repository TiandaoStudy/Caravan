using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RestService.Controllers;

namespace RestService
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "CustomersById",
                 url: "customers/{customerId}",
                 defaults: new { controller = "Customers", action = "GetById" },
                 constraints: new { customerId = @"^[a-zA-Z]+$" }
             );

            routes.MapRoute(
                 name: "EmployeesById",
                 url: "employees/{employeeId}",
                 defaults: new { controller = "Employees", action = "GetById" },
                 constraints: new { employeeId = @"\d+" }
             );

            routes.MapRoute(
                 name: "ShippersById",
                 url: "shippers/{shipperId}",
                 defaults: new { controller = "Shippers", action = "GetById" },
                 constraints: new { shipperId = @"\d+" }
             );
        }
    }
}

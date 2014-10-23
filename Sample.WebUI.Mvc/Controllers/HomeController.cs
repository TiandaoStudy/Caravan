using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Finsa.Caravan.Mvc.Controllers;
using Finsa.Caravan.Mvc.Controls.DataVisualization;
using Newtonsoft.Json;
using RestSharp;
using Sample.DataModel.Entities;

namespace Sample.WebUI.Mvc.Controllers
{
   public class HomeController : FlexController
   {
      public ActionResult Index()
      {
         return View();
      }

      public PartialViewResult Search()
      {
         return PartialView();
      }

      public PartialViewResult InitializeSearchGrid(string searchCriteriaJson, int? pageIndex)
      {
         searchCriteriaJson = Encoding.Default.GetString(Convert.FromBase64String(searchCriteriaJson ?? ""));
         var searchCriteria = JsonConvert.DeserializeObject<dynamic>(searchCriteriaJson);

         // QUERY
         var client = new RestClient("http://localhost/testrestservice/employees");
         var request = new RestRequest("query", Method.POST);
         if (searchCriteria != null && searchCriteria.Count > 0)
         {
            request.AddParameter("q", String.Format("$filter=EmployeeID eq {0}", searchCriteria[0].employeeId));
         }
         else
         {
            request.AddParameter("q", String.Empty);
         }

         IRestResponse response = client.Execute(request);
         var content = response.Content; // raw content as string
         var employees = JsonConvert.DeserializeObject<IEnumerable<Employee>>(content);

         return PartialView("~/Controls/DataVisualization/FlexDataGrid_.cshtml", new FlexDataGridOptions
         {
            ID = "fdtg-customers",
            //PagedItems = (employees).ToPagedList(pageIndex ?? 1, 5),
            PagerAction = page => Url.Action("InitializeSearchGrid", new {pageIndex = page}),
            Columns = new List<FlexDataGridColumnOptions>
            {
               new FlexDataGridColumnOptions {Header = "LastName", Control = (r) => r.LastName},
               new FlexDataGridColumnOptions {Header = "FirstName", Control = (r) => r.FirstName},
               new FlexDataGridColumnOptions {Header = "Title", Control = (r) => r.Title}
            }
         });
      }

      public PartialViewResult CV()
      {
         return PartialView();
      }

      public PartialViewResult PQLista()
      {
         return PartialView();
      }
   }
}
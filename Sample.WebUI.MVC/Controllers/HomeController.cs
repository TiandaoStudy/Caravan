using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FLEX.Web.MVC.Controllers;
using FLEX.Web.MVC.Controls.DataVisualization;
using PagedList;

namespace Sample.WebUI.MVC.Controllers
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

      public PartialViewResult InitializeSearchGrid(int? pageIndex)
      {
         // QUERY
         return PartialView("~/Controls/DataVisualization/FlexDataGrid_.cshtml", new FlexDataGridOptions
         {
            ID = "fdtg-customers",
            PagedItems = (new List<Tuple<int, int>> {Tuple.Create(1, 2), Tuple.Create(2, 3)}).ToPagedList(pageIndex ?? 1, 1),
            PagerAction = page => Url.Action("InitializeSearchGrid", new {pageIndex = page}),
            Columns = new List<FlexDataGridColumnOptions>
            {
               new FlexDataGridColumnOptions {Header = "PROVA1"},
               new FlexDataGridColumnOptions {Header = "PROVA2"},
               new FlexDataGridColumnOptions {Header = "PROVA3"}
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
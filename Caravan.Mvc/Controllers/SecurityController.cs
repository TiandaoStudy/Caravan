﻿using System.Web.Mvc;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.Mvc.ViewModels.Security;

namespace Finsa.Caravan.Mvc.Controllers
{
   public sealed class SecurityController : Controller
   {
      public static string CaravanQueryService
      {
         get { return DataAccess.Configuration.Instance.CaravanRestServiceUrl + "query/" + Common.Configuration.Instance.ApplicationName + "/"; }
      }

      public ActionResult SecGroupList()
      {
         var viewModel = new SecGroupList
         {
            Groups = Db.Security.Groups(Common.Configuration.Instance.ApplicationName)
         };
         return View(viewModel);
      }

      public ActionResult SecGroupList_Table()
      {
         var viewModel = new SecGroupList
         {
            Groups = Db.Security.Groups(Common.Configuration.Instance.ApplicationName)
         };
         return PartialView();
      }

      public ActionResult SecUserList()
      {
          var viewModel = new SecUserList
          {
              Users = Db.Security.Users(Common.Configuration.Instance.ApplicationName)
          };
          return View(viewModel);
      }

      public ActionResult SecUserList_Table()
      {
          var viewModel = new SecUserList
          {
              Users = Db.Security.Users(Common.Configuration.Instance.ApplicationName)
          };
          return PartialView();
      }
   }
}
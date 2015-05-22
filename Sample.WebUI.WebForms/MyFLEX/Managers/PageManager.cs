using System;
using System.Collections.Generic;
using System.Web;
using Finsa.Caravan.Common;
using FLEX.Sample.WebUI.Reports;
using FLEX.WebForms;
using Finsa.CodeServices.Common;

namespace FLEX.Sample.WebUI.MyFLEX.Managers
{
   public sealed class PageManager : IPageManager
   {
      public IList<KeyValuePair<string, string>> GetFooterInfo()
      {
         return new List<KeyValuePair<string, string>>
         {
            KeyValuePair.Create("Host", HttpContext.Current.Server.MachineName)
         };
      }

      public IReportInitializer GetReportInitializer(string reportName)
      {
         switch (reportName)
         {
            case "SimpleReport":
               return new SimpleReport();
         }
         throw new ArgumentException(@"Missing report initializer", reportName);
      }
   }
}
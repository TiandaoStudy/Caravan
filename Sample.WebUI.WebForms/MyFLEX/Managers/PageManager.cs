using System;
using System.Collections.Generic;
using System.Web;
using Finsa.Caravan;
using FLEX.Sample.WebUI.Reports;
using FLEX.WebForms;

namespace FLEX.Sample.WebUI.MyFLEX.Managers
{
   public sealed class PageManager : IPageManager
   {
      public IList<GKeyValuePair<string, string>> GetFooterInfo()
      {
         return new List<GKeyValuePair<string, string>>
         {
            GKeyValuePair.Create("Host", HttpContext.Current.Server.MachineName)
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
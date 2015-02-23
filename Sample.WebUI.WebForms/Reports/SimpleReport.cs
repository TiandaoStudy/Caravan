using System.Collections.Generic;
using System.Web;
using FLEX.Web;
using FLEX.WebForms;
using Microsoft.Reporting.WebForms;

namespace FLEX.Sample.WebUI.Reports
{
   public sealed class SimpleReport : IReportInitializer
   {
      public void InitializeReport(ReportViewer reportViewer, IDictionary<string, object> reportParameters)
      {
         reportViewer.ProcessingMode = ProcessingMode.Local;
         reportViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Reports/SimpleReport.rdlc");
      }
   }
}
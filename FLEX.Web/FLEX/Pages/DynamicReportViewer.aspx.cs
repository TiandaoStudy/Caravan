using System;
using System.IO;
using FLEX.Common.XML;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class DynamicReportViewer : PageBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
            var reportXmlPath = Server.MapPath(Path.Combine(Configuration.Instance.DynamicReportsFolder, "SampleReport.xml"));
            dynamic reportXml = DynamicXml.Load(reportXmlPath);
         }
         catch (Exception)
         {
            
            throw;
         }
      }
   }
}
using System;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class ReportViewer : PopupBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
            var reportName = Request["reportName"];
            var reportInit = PageManager.Instance.GetReportInitializer(reportName);
            reportInit.InitializeReport(myReportViewer);
         }      
      }

      #region Public Properties

      public Microsoft.Reporting.WebForms.ReportViewer Viewer
      {
         get { return myReportViewer; }
      }

      #endregion
   }
}
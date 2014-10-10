using System;
using System.Collections.Generic;
using System.Text;
using FLEX.Web.UserControls.Ajax;
using FLEX.WebForms;
using Newtonsoft.Json;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class ReportViewer : PopupBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
            if (!IsPostBack)
            {
               var reportName = Request["reportName"];
               var decodedParameters = Encoding.UTF8.GetString(Convert.FromBase64String(Request["encodedParameters"]));
               var reportParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(decodedParameters);
               var reportInit = PageManager.Instance.GetReportInitializer(reportName);
               reportInit.InitializeReport(myReportViewer, reportParameters);
            } 
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex, ErrorLocation.PageEvent | ErrorLocation.ModalWindow); 
            throw;
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
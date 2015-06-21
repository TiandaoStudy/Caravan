using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Finsa.Caravan.Common.Models.Logging;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using WebMarkupMin.WebForms.Pages;

namespace Finsa.Caravan.ReportingService
{
    public partial class ReportViewer : MinifiedAndCompressedHtmlPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                EnableCompression = true;
                EnableMinification = true;

                theReport.ProcessingMode = ProcessingMode.Local;
                theReport.LocalReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");
                
                ReportDataSource datasource = new ReportDataSource("LogEntries", new List<LogMessage> {new LogMessage {ShortMessage = "X"}});

                theReport.LocalReport.DataSources.Clear();
                theReport.LocalReport.DataSources.Add(datasource);
            }

            //theReport.LocalReport = LocalReport.
            //var reportName = Request["reportName"];
            //var decodedParameters = Encoding.UTF8.GetString(Convert.FromBase64String(Request["encodedParameters"]));
            //var reportParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(decodedParameters);
        }
    }
}
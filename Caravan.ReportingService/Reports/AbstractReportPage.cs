using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.UI;

namespace Finsa.Caravan.ReportingService
{
    public abstract class AbstractReportPage : Page
    {
        private static readonly string MasterReportPath = HttpContext.Current.Server.MapPath("~/Reports/ReportMasterV.rdlc");

        private ReportViewer _cachedReportViewer;
        private readonly string _mappedReportPath;

        protected AbstractReportPage(string reportTitle, string reportPath)
        {
            ReportTitle = reportTitle;
            _mappedReportPath = Server.MapPath(reportPath);
        }

        protected string ReportTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnLoad(Request.QueryString);
            }
        }

        protected virtual void OnLoad(NameValueCollection parameters)
        {
            try
            {
                ReportViewer.Reset();

                var report = ReportViewer.LocalReport;
                report.LoadReportDefinition(File.OpenRead(MasterReportPath));

                report.SubreportProcessing += (s, a) => OnReportProcessing(s as ReportViewer, a);

                report.LoadSubreportDefinition("subreport", File.OpenRead(_mappedReportPath));

                var reportParameters = new List<ReportParameter>(4)
                {
                    new ReportParameter("pTitle", ReportTitle)
                };
                SetReportParameters(reportParameters);
                report.SetParameters(reportParameters);

                report.Refresh();

                var export = parameters.Get("export");               
                var fileName = "sample";

                switch (export)
                {
                    case "pdf":
                        {
                            CreateFile(fileName,"PDF");
                            break;
                        }

                    case "xls":
                        {
                            CreateFile(fileName, "EXCEL");
                            break;
                        }

                    case "xlsx":
                        {
                            CreateFile(fileName, "EXCELOPENXML");
                            break;
                        }

                    case "doc":
                        {
                            CreateFile(fileName, "WORD");
                            break;
                        }

                    case "docx":
                        {
                            CreateFile(fileName, "WORDOPENXML");
                            break;
                        }
                    case "tif":
                        {
                            CreateFile(fileName, "IMAGE");
                            break;
                        }
                    case "png":
                        {
                            CreateFile(fileName, "IMAGE", export);
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                (Master as Finsa.Caravan.ReportingService.Report).OnError(ex);
            }
        }

        protected abstract void SetReportParameters(IList<ReportParameter> parameters);

        protected abstract void OnReportProcessing(ReportViewer reportViewer, SubreportProcessingEventArgs args);

        public ReportViewer ReportViewer
        {
            get
            {
                if (_cachedReportViewer == null)
                {
                    var master = Master as Report;
                    if (master == null)
                    {
                        throw new InvalidOperationException("Please set the MasterType clause to <%@ MasterType VirtualPath=\"~/Reports/Report.Master\" %>");
                    }
                    _cachedReportViewer = master.ReportViewer;
                }
                return _cachedReportViewer;
            }
        }

        public void CreateFile(string fileName, string format, string deviceInfo = null)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            

            if (deviceInfo != null)
            {
                deviceInfo = "<DeviceInfo>" +
               "<OutputFormat>" + deviceInfo + "</OutputFormat>" + "</DeviceInfo>";               
            }

            byte[] bytes = ReportViewer.LocalReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            //Response.Buffer = true;
            Response.ClearHeaders();
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
            Response.End();
        }

       
    }
}
using System;
using System.Collections.Specialized;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.IO;
using System.Web;

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

                if(export == "pdf")
                {
                    CreatePDF("sample");
                }

                if (export == "xls")
                {
                    CreateExcel_xls("sample");
                }

                if(export == "xlsx")
                {
                    CreateExcel_xlsx("sample");
                }

                if (export == "doc")
                {
                    CreateDoc_doc("sample");
                }

                if (export == "docx")
                {
                    CreateDoc_docx("sample");
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

        public void CreatePDF(string fileName)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


                       byte[] bytes = ReportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        public void CreateExcel_xls(string fileName)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            byte[] bytes = ReportViewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        public void CreateExcel_xlsx(string fileName)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            byte[] bytes = ReportViewer.LocalReport.Render("EXCELOPENXML", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            //Response.Charset = "";
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        public void CreateDoc_doc(string fileName)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            byte[] bytes = ReportViewer.LocalReport.Render("WORD", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        public void CreateDoc_docx(string fileName)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            byte[] bytes = ReportViewer.LocalReport.Render("WORDOPENXML", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            //Response.Charset = "";
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }


    }
}
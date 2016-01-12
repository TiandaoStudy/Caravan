// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.UI;
using Finsa.Caravan.ReportingService.Models;

namespace Finsa.Caravan.ReportingService
{
    public abstract class AbstractReportPage : Page
    {
        private static readonly string MasterReportPathV = HttpContext.Current.Server.MapPath("~/Reports/ReportMasterV.rdlc");
        private static readonly string MasterReportPathH = HttpContext.Current.Server.MapPath("~/Reports/ReportMasterH.rdlc");

        private ReportViewer _cachedReportViewer;
        private readonly string _mappedReportPath;
        private readonly PageOrientation _pageOrientation;

        protected AbstractReportPage(string reportTitle, string reportPath, PageOrientation pageOrientation = PageOrientation.Vertical, string exportName = "Report")
        {
            ReportTitle = reportTitle;
            _mappedReportPath = Server.MapPath(reportPath);
            _pageOrientation = pageOrientation;
            ExportName = exportName;
        }

        //nome del file di export
        protected string ExportName { get; set; }

        protected string ReportTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnLoad(Request.QueryString);
            }
        }

        protected virtual void OnLoad(NameValueCollection requestParameters)
        {
            try
            {
                ReportViewer.Reset();

                var report = ReportViewer.LocalReport;

                if (_pageOrientation == PageOrientation.Vertical)
                {
                    report.LoadReportDefinition(File.OpenRead(MasterReportPathV));
                }
                else
                {
                    report.LoadReportDefinition(File.OpenRead(MasterReportPathH));
                }

                GetRequestParameters(requestParameters);

                report.SubreportProcessing += (s, a) => SetReportDataSources(a.DataSources);

                report.LoadSubreportDefinition("subreport", File.OpenRead(_mappedReportPath));

                var reportParameters = new List<ReportParameter>(4);
                SetReportParameters(reportParameters);
                reportParameters.Add(new ReportParameter("pTitle", ReportTitle));
                report.SetParameters(reportParameters);

                report.Refresh();

                var export = requestParameters.Get("export");

                switch (export)
                {
                    case "pdf":
                        {
                            CreateFile(ExportName, "PDF");
                            break;
                        }

                    case "xls":
                        {
                            CreateFile(ExportName, "EXCEL");
                            break;
                        }

                    case "xlsx":
                        {
                            CreateFile(ExportName, "EXCELOPENXML");
                            break;
                        }

                    case "doc":
                        {
                            CreateFile(ExportName, "WORD");
                            break;
                        }

                    case "docx":
                        {
                            CreateFile(ExportName, "WORDOPENXML");
                            break;
                        }
                    case "tif":
                        {
                            CreateFile(ExportName, "IMAGE");
                            break;
                        }
                    case "png":
                        {
                            CreateFile(ExportName, "IMAGE", export);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                (Master as Report).OnError(ex);
            }
        }

        protected abstract void GetRequestParameters(NameValueCollection requestParameters);

        protected abstract void SetReportParameters(IList<ReportParameter> parameters);

        protected abstract void SetReportDataSources(ReportDataSourceCollection dataSources);

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

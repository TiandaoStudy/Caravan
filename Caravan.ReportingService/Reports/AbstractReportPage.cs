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
                        throw new InvalidOperationException("Please set the MasterType clause to <%@ MasterType VirtualPath=\"~/Caravan/Report.Master\" %>");
                    }
                    _cachedReportViewer = master.ReportViewer;
                }
                return _cachedReportViewer;
            }
        }
    }
}
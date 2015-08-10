using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

namespace Finsa.Caravan.ReportingService.Reports.Sample
{
    public partial class Sample : AbstractReportPage
    {
       
        public Sample() : base("Sample Report", "~/Reports/Sample/Sample.rdlc")
        {
        }

        protected override void SetReportParameters(IList<ReportParameter> parameters)
        {            
        }

        protected override void OnReportProcessing(ReportViewer reportViewer, SubreportProcessingEventArgs args)
        {           
        }
    }
}

using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System;
using System.Collections.Specialized;

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

        protected override void SetReportDataSources(ReportDataSourceCollection dataSources)
        {           
        }

        protected override void GetRequestParameters(NameValueCollection requestParameters)
        {            
        }
    }
}

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

using System;
using Microsoft.Reporting.WebForms;

namespace Finsa.Caravan.ReportingService
{
    public partial class Report : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["stub"] = true;
        }

        public void OnError(Exception exception)
        {
            // Recupera l'eccezione più interna.
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            reportViewer.Reset();
            reportPanel.Visible = false;

            errorPanel.Visible = true;

            txtErrorMessage.Text = exception.Message;
            txtErrorStackTrace.Text = exception.StackTrace;
        }

        public ReportViewer ReportViewer => reportViewer;
    }
}
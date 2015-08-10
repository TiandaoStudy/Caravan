using System;
using System.Web.UI.WebControls;
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

            _reportViewer.Reset();
            reportPanel.Visible = false;

            errorPanel.Visible = true;
           
            txtErrorMessage.Text =  exception.Message;
            txtErrorStackTrace.Text = exception.StackTrace;
            
        }

        public ReportViewer ReportViewer
        {
            get { return _reportViewer; }
        }
    }
}
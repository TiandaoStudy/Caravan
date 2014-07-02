using System;
using System.Web.UI;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
    public partial class ErrorHandler : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Empty
           Exception ex;
           string[] ErrorArray= new string[3];
           btnPrint.Attributes["onclick"]= "return openReportViewer('REPORT=StampaErrorHandler');";

           txtTitle.Text =WebSettings.ProjectName;

           //Per errore potrei chiamare la pagina e la sessione è vuota
           if (Session[WebSettings.UserControls_Ajax_ErrorHandler_ExceptionSessionKey] == null) 
           {
              //Chiudo la finestra
              Response.Write("<script language='javascript'>window.close();</script>");
           }
           else 
           {
              ex = ((Exception)Session[WebSettings.UserControls_Ajax_ErrorHandler_ExceptionSessionKey]);
              if (ex != null) 
              {
                 txtDetail.Text = ex.ToString();
                 while (ex.InnerException != null)
                    ex = ex.InnerException;

                 if (ex.Source == "System.Data.OracleClient")
                 {
                    //Chiamare componente dataAccess.Handle Error OK 
                    //dento la componente cast in oracleexception OK 
                    //case con errori OK
                    //FK_padre_figlio
                    txtMessage.Text = ex.Message;
                    //txtMessage.Text = Get_Error_Message(ex);
                    txtSource.Text = ex.Source;
                    if (ex.TargetSite != null)
                    {
                       txtSource.Text += "." + ex.TargetSite.Name;
                    }
                 }
                 else
                 {
                    txtMessage.Text = ex.Message;
                    txtSource.Text = ex.Source;
                    if (ex.TargetSite != null)
                    {
                       txtSource.Text += "." + ex.TargetSite.Name;
                    }
                 }
                    
              }
               ErrorArray[0] = txtMessage.Text;
               ErrorArray[1] = txtSource.Text;
               ErrorArray[2] = txtDetail.Text;

               //prima la svuoto e poi la valorizzo con i dati del messaggio d'errore 
               Session["STAMPA_ERRORE"] = null;
               Session["STAMPA_ERRORE"] = ErrorArray;

           }

     
        }
    }
}
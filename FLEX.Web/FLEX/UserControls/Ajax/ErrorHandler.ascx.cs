using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using FLEX.Common.Data;
using FLEX.Common.Web;
using FLEX.Web.WebForms;
using PommaLabs.GRAMPA.Diagnostics;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class ErrorHandler : UserControl
   {
      public void CatchException(Exception ex, ErrorLocation location = ErrorLocation.EventHandler)
      {
         Raise<ArgumentNullException>.IfIsNull(ex);
         var locationByte = (byte) location;
         Raise<ArgumentException>.IfAreEqual(locationByte, 0);

         if (ex is ThreadAbortException)
         {
            // Le eccezioni di questo tipo non vanno catturate, perché servono per il corretto funzionamento di ASP.
            throw ex;
         }

         try
         {
            txtSystemErrorCode.Text = locationByte.ToString(CultureInfo.InvariantCulture);
            ex = ElaborateException(ex);
            Session[WebSettings.UserControls_Ajax_ErrorHandler_ExceptionSessionKey] = ex;
            ErrorManager.Instance.LogException(ex, Page);
         }
         catch (Exception inner)
         {
            DbLogger.Instance.LogError<ErrorHandler>(inner);
            throw;
         }
      }

      private Exception ElaborateException(Exception ex)
      {
         while (ex.InnerException != null)
         {
            ex = ex.InnerException;
         }
         return ErrorManager.Instance.ElaborateException(ex, Page);
      }
   }

   [Flags]
   public enum ErrorLocation : byte
   {
      EventHandler = 1,
      PageEvent = 2,
      ModalWindow = 4
   }
}
using System;
using System.Globalization;
using System.Web.UI;
using FLEX.Common;
using FLEX.Common.Web;
using Thrower;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class ErrorHandler : UserControl
   {
      private static readonly IErrorManager ErrorManager;

      static ErrorHandler()
      {
         try
         {
            var info = WebSettings.UserControls_Ajax_ErrorHandler_ErrorManagerInfo;
            ErrorManager = ServiceLocator.Load<IErrorManager>(info);
         }
         catch (Exception ex)
         {
            DbLogger.Instance.LogError<ErrorHandler>("ErrorHandler()", ex);
            // It's better NOT to rethrow in static constructors, 
            // otherwise the application will silently die. 
         }
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         // Empty, for now
      }

      public void CatchException(Exception ex, ErrorLocation location = ErrorLocation.EventHandler)
      {
         Raise<ArgumentNullException>.IfIsNull(ex);
         var locationByte = (byte) location;
         Raise<ArgumentException>.IfAreEqual(locationByte, 0);

         try
         {
            txtSystemErrorCode.Text = locationByte.ToString(CultureInfo.InvariantCulture);
            ex = ElaborateException(ex);
            Session[WebSettings.UserControls_Ajax_ErrorHandler_ExceptionSessionKey] = ex;
            ErrorManager.LogException(ex, Page);
         }
         catch (Exception inner)
         {
            DbLogger.Instance.LogError<ErrorHandler>("CatchException(ex, location)", inner);
            throw;
         }
      }

      private Exception ElaborateException(Exception ex)
      {
         while (ex.InnerException != null)
         {
            ex = ex.InnerException;
         }
         return ErrorManager.ElaborateException(ex, Page);
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
using System;
using System.Web.UI;
using FLEX.Web;

namespace FLEX.Sample.WebUI.MyFLEX.Managers
{
   public sealed class ErrorManager : IErrorManager
   {
      public Exception ElaborateException(Exception ex, Page onPage)
      {
         return ex;
      }

      public void LogException(Exception ex, Page onPage)
      {
         // Custom actions, like logging into the DB...
      }
   }
}
using System;
using System.IO;
using System.Text;

namespace FLEX.Common
{
   public static class QuickLogger
   {
      private const string ErrorTag = "ERR";
      private const string LogExtension = ".log";

      public static void LogError<T>(Exception ex)
      {
         var assembly = typeof (T).Assembly.GetName().Name;
         var logFile = CommonSettings.MapPath(CommonSettings.QuickLogger_LogsPath, assembly + LogExtension);
         var builder = new StringBuilder();
         builder.AppendFormat("[{0} - {1}] {2} {3}", ErrorTag, DateTime.Now, ex.GetType().Name, ex.Message);
         builder.AppendLine(ex.StackTrace);
         builder.AppendLine();
         File.AppendAllText(logFile, builder.ToString());
      }
   }
}

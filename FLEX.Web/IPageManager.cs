using System.Collections.Generic;
using FLEX.Common;

namespace FLEX.Web
{
   /// <summary>
   /// 
   /// </summary>
   public interface IPageManager
   {
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      IList<Pair<string, string>> GetFooterInfo();

      /// <summary>
      /// 
      /// </summary>
      /// <param name="reportName"></param>
      /// <returns></returns>
      IReportInitializer GetReportInitializer(string reportName);
   }

   /// <summary>
   /// 
   /// </summary>
   public interface IReportInitializer
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="reportViewer"></param>
      /// <param name="reportParameters"></param>
      void InitializeReport(Microsoft.Reporting.WebForms.ReportViewer reportViewer, IDictionary<string, string> reportParameters);
   }
}

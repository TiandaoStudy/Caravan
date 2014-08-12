using System.Collections.Generic;
using FLEX.Common;
using Microsoft.Reporting.WebForms;

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
      void InitializeReport(ReportViewer reportViewer);
   }
}

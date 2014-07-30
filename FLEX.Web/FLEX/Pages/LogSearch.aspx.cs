using System;
using FLEX.Common;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class LogSearch : PageBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
            fdtgLogs.UpdateDataSource();
         }
         
         btnRefresh.Click += (s, a) => fdtgLogs.UpdateDataSource();
      }

      protected void fdtgLogs_DataSourceUpdating(object sender, EventArgs args)
      {
         fdtgLogs.DataSource = DbLogger.Instance.RetrieveCurrentApplicationLogsTable();
      }
   }
}
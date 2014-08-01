﻿using System;
using FLEX.Common;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class LogSearch : PageBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
            if (!IsPostBack)
            {
               fdtgLogs.UpdateDataSource();
            }
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex, ErrorLocation.PageEvent);
         }
      }

      protected void fdtgLogs_DataSourceUpdating(object sender, EventArgs args)
      {
         // This should not catch any exception, others will do.
         fdtgLogs.DataSource = DbLogger.Instance.RetrieveCurrentApplicationLogsTable();
      }

      protected void btnRefresh_Click(object sender, EventArgs args)
      {
         try
         {
            fdtgLogs.UpdateDataSource();
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }
   }
}
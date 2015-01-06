﻿using System;
using System.Linq;
using Finsa.Caravan.DataAccess;
using FLEX.Web.Pages;
using FLEX.Web.UserControls.Ajax;
using PommaLabs.Extensions;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.WebForms.Pages
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
         fdtgLogs.DataSource = (
            from l in Db.Logger.Logs(Finsa.Caravan.Common.Configuration.Instance.ApplicationName)
            select new
            {
               l.Id,
               l.Date,
               l.UserLogin,
               Type = l.Type.ToString(),
               l.CodeUnit,
               l.Function,
               l.ShortMessage,
               l.LongMessage
            }).ToDataTable();
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
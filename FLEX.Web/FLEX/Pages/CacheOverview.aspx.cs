using System;
using System.Collections;
using System.Data;
using System.Linq;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class CacheOverview : PageBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
            if (!IsPostBack)
            {
               fdtgCache.UpdateDataSource();
            }
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex, ErrorLocation.PageEvent);
         }
      }

      protected void btnClear_Click(object sender, EventArgs args)
      {
         try
         {
            CacheManager.ClearCache();
            fdtgCache.UpdateDataSource();
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      protected void btnRefresh_Click(object sender, EventArgs args)
      {
         try
         {
            fdtgCache.UpdateDataSource();
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      protected void fdtgCache_DataSourceUpdating(object sender, EventArgs args)
      {
         // This should not catch any exception, others will do.
         var dt = new DataTable();
         dt.Columns.Add("KEY");
         dt.Columns.Add("VALUE");
         foreach (var row in Cache.Cast<DictionaryEntry>())
         {
            dt.Rows.Add(row.Key, row.Value);
         }
         fdtgCache.DataSource = dt;
      }
   }
}
using System;
using System.Collections;
using System.Data;
using System.Linq;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class CacheOverview : PageBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
            fdtgCache.UpdateDataSource();
         }
         
         btnRefresh.Click += (s, a) => fdtgCache.UpdateDataSource();
         btnClear.Click += btnClear_Click;
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
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void fdtgCache_DataSourceUpdating(object sender, EventArgs args)
      {
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
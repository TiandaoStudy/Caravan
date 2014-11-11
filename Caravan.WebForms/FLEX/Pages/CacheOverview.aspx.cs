using System;
using System.Collections.Generic;
using System.Linq;
using Finsa.Caravan.Extensions;
using FLEX.Web.Pages;
using FLEX.Web.UserControls.Ajax;
using PommaLabs.KVLite;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.WebForms.Pages
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
            // Applico una pulizia sicura della cache persistente, per evitare che le voci importanti vadano perse.
            PersistentCache.DefaultInstance.Clear(CacheReadMode.ConsiderExpirationDate);
            VolatileCache.DefaultInstance.Clear(CacheReadMode.IgnoreExpirationDate);
            // Aggiorno la fonte dati sottostante la griglia.
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
         fdtgCache.DataSource = GetVolatileCacheItems().Union(GetPersistentCacheItems()).ToDataTable();
      }

      #region Private Methods

      private static IEnumerable<CacheItem> GetVolatileCacheItems()
      {
         return VolatileCache.DefaultInstance.GetAllItems().Select(x => new CacheItem
         {
            Partition = x.Partition,
            Key = x.Key,
            Value = x.Value.ToString(),
            UtcCreation = x.UtcCreation.ToLocalTime(),
            UtcExpiry = x.UtcExpiry.HasValue ? x.UtcExpiry.Value.ToLocalTime() : x.UtcExpiry,
            Interval = x.Interval
         });
      }

      private static IEnumerable<CacheItem> GetPersistentCacheItems()
      {
         return PersistentCache.DefaultInstance.GetAllItems().Select(x => new CacheItem
         {
            Partition = x.Partition,
            Key = x.Key,
            Value = x.Value.ToString(),
            UtcCreation = x.UtcCreation.ToLocalTime(),
            UtcExpiry = x.UtcExpiry.HasValue ? x.UtcExpiry.Value.ToLocalTime() : x.UtcExpiry,
            Interval = x.Interval
         });
      }

      #endregion
   }
}
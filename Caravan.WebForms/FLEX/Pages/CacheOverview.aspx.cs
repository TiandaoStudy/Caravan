﻿using FLEX.Web.Pages;
using FLEX.Web.UserControls.Ajax;
using PommaLabs.Extensions;
using PommaLabs.KVLite;
using System;
using System.Linq;

// ReSharper disable CheckNamespace This is the correct namespace, despite the file physical position.

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
                // Applico una pulizia sicura della cache persistente, per evitare che le voci
                // importanti vadano perse.
                PersistentCache.DefaultInstance.Clear(PersistentCacheReadMode.ConsiderExpiryDate);
                VolatileCache.DefaultInstance.Clear();
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
            fdtgCache.DataSource = VolatileCache.DefaultInstance.GetManyItems()
                .Union(PersistentCache.DefaultInstance.PeekManyItems())
                .Where(x => x.Key != "ConnectionString") // Do not show connection strings...
                .Select(x => new CacheItem
                {
                    Partition = x.Partition,
                    Key = x.Key,
                    Value = x.Value.ToString(),
                    UtcCreation = x.UtcCreation.ToLocalTime(),
                    UtcExpiry = x.UtcExpiry.HasValue ? x.UtcExpiry.Value.ToLocalTime() : x.UtcExpiry,
                    Interval = x.Interval
                })
                .ToDataTable();
        }
    }
}
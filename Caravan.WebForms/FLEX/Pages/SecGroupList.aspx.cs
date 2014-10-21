using System;
using System.Linq;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.Extensions;
using FLEX.Common.Web;
using FLEX.Web.Pages;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace Finsa.Caravan.WebForms.Pages
// ReSharper restore CheckNamespace
{
   public partial class SecGroupList : PageBaseListAndSearch
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
            fdtgGroups.UpdateDataSource();
         }
      }

      #region Search Criteria

      protected override void FillSearchCriteria()
      {
      }

      protected override void RegisterSearchCriteria(SearchCriteria criteria)
      {
      }

      #endregion

      #region Grid Events

      protected void fdtgGroups_DataSourceUpdating(object sender, EventArgs args)
      {
         // This should not catch any exception, others will do.
         var groups = (from g in Db.Security.Groups(Common.Configuration.Instance.ApplicationName)
                       select new SecGroup {Id = g.Id, Name = g.Name, Description = g.Description, IsAdmin = g.IsAdmin})
                       .ToDataTable();
         fdtgGroups.DataSource = groups;
      }

      #endregion
   }
}
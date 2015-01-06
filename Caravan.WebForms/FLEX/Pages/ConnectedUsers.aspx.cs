using System;
using System.Linq;
using System.Web.UI.WebControls;
using Finsa.Caravan.DataModel.Security;
using FLEX.Common.Web;
using FLEX.Web.Pages;
using System.Data;
using System.Collections.Generic;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace Finsa.Caravan.WebForms.Pages
// ReSharper restore CheckNamespace
{
    public partial class ConnectedUsers : PageBaseListAndSearch
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
             fdtgConnectedUsers.UpdateDataSource();
         }
      }

      #region Search Criteria

      protected override void FillSearchCriteria()
      {
      }

      protected override void RegisterSearchCriteria(SearchCriteria criteria)
      {
          SearchCriteria.RegisterControl(crvnUsersLkp, "CUSR_LOGIN");
          SearchCriteria.CriteriaChanged += SearchCriteria_CriteriaChanged;
      }

      private void SearchCriteria_CriteriaChanged(SearchCriteria searchCriteria, SearchCriteriaChangedArgs args)
      {
          fdtgConnectedUsers.UpdateDataSource();
      }

      #endregion

      #region Grid Events

      protected void fdtgConnectedUsers_DataSourceUpdating(object sender, EventArgs args)
      {
   
          var userLogin = "";

          Application.Lock();
          Dictionary<string, SessionTracker> _userList = (Dictionary<string, SessionTracker>)Application.Get("TRACK_USER_LIST");
          Application.UnLock();


          //Users
          DataTable _tableUsers = new DataTable();
          _tableUsers.Columns.Add("Login", typeof(string));
          _tableUsers.Columns.Add("UserHostName", typeof(string));
          _tableUsers.Columns.Add("UserHostAddress", typeof(string));
          _tableUsers.Columns.Add("LastVisit", typeof(DateTime));


         if (SearchCriteria["CUSR_LOGIN"].Count > 0)
         {
             userLogin = SearchCriteria["CUSR_LOGIN"][0];

             var users = _userList.Where(x => ((SessionTracker)x.Value).Login == userLogin.ToString());


             foreach (var item in users)
             {
                 _tableUsers.Rows.Add(item.Value.Login, item.Value.UserHostName, item.Value.UserHostAddress, item.Value.LastVisit);
             }


             fdtgConnectedUsers.DataSource = _tableUsers;
             return;
         }
         else
         {

            foreach (var item in _userList.Where(u => u.Value.Login != null))
             {
                 _tableUsers.Rows.Add(item.Value.Login, item.Value.UserHostName, item.Value.UserHostAddress, item.Value.LastVisit);
             }

             fdtgConnectedUsers.DataSource = _tableUsers;
             return;
         }
      }

      protected void fdtgConnectedUsers_OnRowDataBound(object sender, GridViewRowEventArgs e)
      {
          if (e.Row.RowType != DataControlRowType.DataRow)
          {
              return;
          }
      }

      #endregion

      #region Buttons
      #endregion
   }
}
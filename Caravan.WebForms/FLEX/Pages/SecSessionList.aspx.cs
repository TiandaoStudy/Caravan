using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.WebForms;
using FLEX.Web.Pages;

// ReSharper disable CheckNamespace This is the correct namespace, despite the file physical position.

namespace Finsa.Caravan.WebForms.Pages
// ReSharper restore CheckNamespace
{
    public partial class SecSessionList : PageBaseListAndSearch
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

        #endregion Search Criteria

        #region Grid Events

        protected void fdtgConnectedUsers_DataSourceUpdating(object sender, EventArgs args)
        {
            var userLogin = "";

            Application.Lock();
            Dictionary<string, SecSession> _userList = (Dictionary<string, SecSession>) Application.Get("TRACK_USER_LIST");
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

                var users = _userList.Where(x => ((SecSession) x.Value).UserLogin == userLogin.ToString());

                foreach (var item in users)
                {
                    _tableUsers.Rows.Add(item.Value.UserLogin, item.Value.UserHostName, item.Value.UserHostAddress, item.Value.LastVisit);
                }

                fdtgConnectedUsers.DataSource = _tableUsers;
                return;
            }
            else
            {
                foreach (var item in _userList.Where(u => u.Value.UserLogin != null))
                {
                    _tableUsers.Rows.Add(item.Value.UserLogin, item.Value.UserHostName, item.Value.UserHostAddress, item.Value.LastVisit);
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

        #endregion Grid Events
    }
}
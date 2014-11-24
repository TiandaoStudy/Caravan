using System;
using FLEX.Common.Web;
using FLEX.Extensions.TestDataAccess;
using FLEX.Web.Pages;

namespace FLEX.TestWebsite
{
    public partial class Default : PageBaseListAndSearch
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCriteria.RegisterControl(autosuggestFullName, "Cand_Id");
            SearchCriteria.RegisterControl(autosuggestEmail, "Cand_Email");
            SearchCriteria.RegisterControl(ctrlGendId, "Gend_Id");
            SearchCriteria.RegisterControl(ctrlSchlId, "Schl_Id");
            SearchCriteria.CriteriaChanged += SearchCriteria_CriteriaChanged;

            autosuggestFullName.AttachToUpdatePanel(ricercaUpdatePanel);
            autosuggestEmail.AttachToUpdatePanel(ricercaUpdatePanel);
            ctrlGendId.AttachToUpdatePanel(ricercaUpdatePanel);
            ctrlSchlId.AttachToUpdatePanel(ricercaUpdatePanel);

            if (!IsPostBack)
            {
                FillSearchCriteria();
                fdtgCandidates.DataBind();
            }
        }

        #region Data Grid for Candidates

        protected void fdtgCandidates_DataBinding(object sender, EventArgs args)
        {
            using (var candidates = Candidate.RetrieveByCriteria(SearchCriteria))
            {
                candidates.DefaultView.Sort = fdtgCandidates.SortExpression;
                candidates.AcceptChanges();
                fdtgCandidates.DataSource = candidates;
                if (candidates.Rows.Count > 0 && fdtgCandidates.SelectedIndex == -1)
                {
                    fdtgCandidates.SelectedIndex = 0;
                }
            }
        }

        #endregion

        #region Private Members

        private void FillSearchCriteria()
        {
            ctrlGendId.MaxVisibleItemCount = 2;
            var genders = Lookup.RetrieveData("Gender");
            ctrlGendId.SetDataSource(genders, "Gend_Id", "Gend_Description");

            ctrlSchlId.MaxVisibleItemCount = 3;
            var schools = Lookup.RetrieveData("School");
            ctrlSchlId.SetDataSource(schools, "Schl_Id", "Schl_Description");
        }

        private void SearchCriteria_CriteriaChanged(SearchCriteria searchCriteria, SearchCriteriaChangedArgs args)
        {
            fdtgCandidates.DataBind();
        }

        #endregion
    }
}
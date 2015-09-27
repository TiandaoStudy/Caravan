using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.Common.WebForms;
using Finsa.Caravan.DataAccess;
using FLEX.Web.Pages;
using System;

namespace FLEX.Sample.WebUI
{
    public partial class CandidateList : PageBaseListAndSearch
    {
        private readonly ICaravanLog _log = CaravanServiceProvider.FetchLog<CandidateList>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fdtgCandidates.UpdateDataSource();
                //var candidates = Candidate.RetrieveByCriteria(SearchCriteria);
                //DataGrid1.DataSource = candidates;
                //DataGrid1.DataBind();
            }
        }

        public void OnClick_Error(object sender, EventArgs args)
        {
            try
            {
                throw new ArgumentException("OKKK");
            }
            catch (Exception ex)
            {
                Master.ErrorHandler.CatchException(ex);
            }
        }

        protected void OnClick_Log(object sender, EventArgs e)
        {
            try
            {
                _log.Error(new LogMessage
                {
                    ShortMessage = "Short msg",
                    LongMessage = CaravanServiceProvider.Clock.Now.ToLongDateString(),
                    Context = "A test"
                });
            }
            catch (Exception ex)
            {
                Master.ErrorHandler.CatchException(ex);
            }
        }

        protected void fdtgCandidates_DataSourceUpdating(object sender, EventArgs e)
        {
            //var candidates = Candidate.RetrieveByCriteria(SearchCriteria);
            //candidates.DefaultView.Sort = fdtgCandidates.SortExpression;
            //candidates.AcceptChanges();
            //fdtgCandidates.DataSource = candidates;

            //if (candidates.Rows.Count > 0 && fdtgCandidates.SelectedIndex == -1)
            //{
            //   fdtgCandidates.SelectedIndex = 0;
            //}

            //fdtgCandidates.DataSource = candidates;
        }

        #region Search Criteria

        protected override void FillSearchCriteria()
        {
            //ctrlGendId.MaxVisibleItemCount = 2;
            //ctrlGendId.SetDataSource(Lookup.RetrieveData("Gender"), "Gend_Id", "Gend_Description");

            //ctrlSchlId.MaxVisibleItemCount = 3;
            //ctrlSchlId.SetDataSource(Lookup.RetrieveData("School"), "Schl_Id", "Schl_Description");
        }

        protected override void RegisterSearchCriteria(SearchCriteria criteria)
        {
            SearchCriteria.RegisterControl(autosuggestFullName, "Cand_Id");
            SearchCriteria.RegisterControl(autosuggestEmail, "Cand_Email");
            SearchCriteria.RegisterControl(ctrlGendId, "Gend_Id");
            SearchCriteria.RegisterControl(ctrlSchlId, "Schl_Id");
            SearchCriteria.CriteriaChanged += SearchCriteria_CriteriaChanged;
            // btnClear.Clicked += SearchCriteria.ClearCriteria;
        }

        private void SearchCriteria_CriteriaChanged(SearchCriteria searchCriteria, SearchCriteriaChangedArgs args)
        {
            fdtgCandidates.UpdateDataSource();
        }

        public void flexExportList_DataSourceNeeded(object sender, EventArgs e)
        {
            //var candidates = Candidate.RetrieveByCriteria(SearchCriteria);
            //flexExportList.DataSource = candidates;
            // fdtgCandidates.DataBind();
            flexExportList.SetDataSource(fdtgCandidates, "CandidateList", new[] { 0 });
        }

        #endregion Search Criteria
    }
}

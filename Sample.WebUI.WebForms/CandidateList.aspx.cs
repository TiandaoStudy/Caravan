using System;
using Finsa.Caravan.DataAccess;
using FLEX.Common.Web;
using FLEX.Web.Pages;

namespace FLEX.Sample.WebUI
{
   public partial class CandidateList : PageBaseListAndSearch
   {
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
            Db.Logger.LogError<CandidateList>("Short msg", DateTime.Now.ToLongDateString(), "A test");
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
         flexExportList.SetDataSource(fdtgCandidates, "CandidateList", new []{0});
      }

      #endregion
   }
}
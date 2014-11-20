using System;
using System.Text;
using System.Web.UI;
using FLEX.Common.Web;
using FLEX.Extensions.TestDataAccess;
using FLEX.Web.Pages;

namespace FLEX.TestWebsite
{
  public partial class DataManagement : Page
  {
    private readonly SearchCriteria _searchCriteria = new SearchCriteria();

    protected void Page_Init(object sender, EventArgs e)
    {
      Master.Page_Visible += Page_Visible;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_Visible(object sender, EventArgs e)
    {
      UpdateData();
    }

    protected void ClearDatabase_Click(object sender, EventArgs e)
    {
      Manager.ClearDatabase();
      UpdateData();
    }

    protected void ResetDatabase_Click(object sender, EventArgs e)
    {
      Manager.ClearDatabase();
      Manager.ResetDatabase();
      UpdateData();
    }

    private void UpdateData()
    {
      try
      {
        var text = new StringBuilder();
        using (var ctx = new DataContext())
        {
          foreach (var s in ctx.Schools)
          {
            text.AppendFormat("ID: {0} - Descr: {1}</br>", s.Schl_Id, s.Schl_Description);
          }
        }
        TestLabel.Text = text.ToString();

        fdtgGender.DataBind();
        fdtgSchools.DataBind();
        fdtgCandidates.DataBind();
      }
      catch
      {
        TestLabel.Text = "ERRORE NEL DB";
      }
    }

    #region Data Grid for Genders

    protected void fdtgGender_DataBinding(object sender, EventArgs args)
    {
      using (var genders = Lookup.RetrieveData("Gender"))
      {
        genders.DefaultView.Sort = fdtgGender.SortExpression;
        genders.AcceptChanges();
        fdtgGender.DataSource = genders;

        if (genders.Rows.Count > 0 && fdtgGender.SelectedIndex == -1)
        {
          fdtgGender.SelectedIndex = 0;
        }
      }
    }

    #endregion

    #region Data Grid for Schools

    protected void fdtgSchools_DataBinding(object sender, EventArgs args)
    {
      using (var schools = Lookup.RetrieveData("School"))
      {
        schools.DefaultView.Sort = fdtgSchools.SortExpression;
        schools.AcceptChanges();
        fdtgSchools.DataSource = schools;

        if (schools.Rows.Count > 0 && fdtgSchools.SelectedIndex == -1)
        {
          fdtgSchools.SelectedIndex = 0;
        }
      }
    }

    #endregion

    #region Data Grid for Candidates

    protected void fdtgCandidates_DataBinding(object sender, EventArgs args)
    {
      using (var candidates = Candidate.RetrieveAll())
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
  }
}
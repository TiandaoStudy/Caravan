using System;
using FLEX.Common.Web;

namespace FLEX.Sample.WebUI
{
    public partial class TestAutoSuggest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            autoSuggestSchl.RegisterAsAsyncPostBackTrigger(labelUpdatePanel);
            autoSuggestEmail.RegisterAsAsyncPostBackTrigger(labelUpdatePanel);

            autoSuggestSchl.ValueSelected += ItemSelected;
            autoSuggestEmail.ValueSelected += ItemSelected;
        }

        private void ItemSelected(ISearchControl sender, SearchCriteriaSelectedArgs e)
        {
            try
            {
                labelSelected.Text = sender.SelectedValues[0].ToString();
            }
            catch
            {
                labelSelected.Text = string.Empty;
            }
        }

    }
}
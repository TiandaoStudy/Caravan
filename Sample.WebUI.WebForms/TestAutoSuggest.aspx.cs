using System;
using Finsa.Caravan.Common.WebForms;

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
                labelSelected.Text = sender.SelectedValues[0];
            }
            catch
            {
                labelSelected.Text = string.Empty;
            }
        }

    }
}
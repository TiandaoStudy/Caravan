﻿using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.Common.WebForms;
using Finsa.Caravan.DataAccess;
using Finsa.CodeServices.Common;
using FLEX.Web.Pages;
using FLEX.Web.UserControls.Ajax;
using System;
using System.Collections.Generic;
using System.Data;

namespace FLEX.Sample.WebUI
{
    public partial class ControlsOverview : PageBase
    {
        private readonly ICaravanLog _log = CaravanServiceProvider.FetchLog<CandidateList>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                flexABC.SetDataSource(new List<string> { "A", "B", "C" });

                var _listLeft = new List<string> { "A", "B", "C" };
                DataTable tableLeft = new DataTable("TableLeft");
                tableLeft.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
                            new DataColumn("A1", typeof(string)),
                            new DataColumn("A2",typeof(string)) });
                tableLeft.Rows.Add(1, "1dataA1", "1dataA2");
                tableLeft.Rows.Add(2, "2dataA1", "2dataA2");
                tableLeft.Rows.Add(3, "3dataA1", "3dataA2");

                DataTable tableRight = new DataTable("TableRight");
                tableRight.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
                            new DataColumn("B1", typeof(string)),
                            new DataColumn("B2",typeof(string)) });
                tableRight.Rows.Add(1, "1dataB1", "1dataB2");
                tableRight.Rows.Add(2, "2data1B1", "2dataB2");

                flexMultipleSelect.SetLeftDataSource(tableLeft);
                flexMultipleSelect.SetRightDataSource(tableRight);
                flexMultipleSelect.LeftPanelTitle = "Left";
                flexMultipleSelect.RightPanelTitle = "Right";
            }
        }

        protected void flexEnabledSwitch_OnValueSelected(ISearchControl sender, SearchCriteriaSelectedArgs args)
        {
            var enabledSwitch = sender as OnOffSwitch;
            EnableOrDisableControls(enabledSwitch.Switched);
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

        public void OnClick_Log(object sender, EventArgs args)
        {
            try
            {
                _log.Info(new LogMessage
                {
                    ShortMessage = "Click!",
                    LongMessage = "Clicked on the log button",
                    Context = "A test",
                    Arguments = new[]
                    {
                        KeyValuePair.Create<string, object>("arg1", "1"),
                        KeyValuePair.Create<string, object>("arg2", "two")
                    }
                });
            }
            catch (Exception ex)
            {
                Master.ErrorHandler.CatchException(ex);
            }
        }

        private void EnableOrDisableControls(bool enabled)
        {
            flexEmplByName.Enabled = enabled;
            flexYearPicker.Enabled = enabled;
            flexNumSpinner.Enabled = enabled;
            flexABC.Enabled = enabled;
        }
    }
}

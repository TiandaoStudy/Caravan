using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class MultiSelect : AjaxControlBase
   {
      protected const string FlagCrudTag = "flag_crud";

      #region Public Properties

      public static string FlagCrud
      {
         get { return FlagCrudTag; }
      }

      public string LeftPanelTitle
      {
         set { lbTitlePanelLeft.Text = value; }
      }

      public string RightPanelTitle
      {
         set { lbTitlePanelRight.Text = value; }
      }

      public DataTable RightDataTable
      {
         get { return (DataTable) ViewState["DataGridRight"]; }
      }

      public DataTable LeftDataTable
      {
         get { return (DataTable) ViewState["DataGridLeft"]; }
      }

      public LinkButton MoveRight
      {
         get { return btnMoveRight; }
      }

      public LinkButton MoveAllRight
      {
         get { return btnMoveAllRight; }
      }

      public LinkButton MoveLeft
      {
         get { return btnMoveLeft; }
      }

      public LinkButton MoveAllLeft
      {
         get { return btnMoveAllLeft; }
      }

      #endregion

      #region Public Methods

      public void SetLeftDataSource(DataTable dataLeft)
      {
         var newColumn = new DataColumn(FlagCrudTag, typeof (String));
         newColumn.DefaultValue = "L";
         dataLeft.Columns.Add(newColumn);

         ViewState["DataGridLeft"] = dataLeft.Copy();
         ViewState["DataGridLeft_Originale"] = dataLeft.Copy();

         fdtgLeft.DefaultSortDirection = SortDirection.Ascending;
         fdtgLeft.DefaultSortExpression = dataLeft.Columns[0].ColumnName;
         fdtgLeft.UpdateDataSource();
         lbCountDataLeft.Text = fdtgLeft.Rows.Count.ToString();
      }

      public void SetRightDataSource(DataTable dataRight)
      {
         var newColumn = new DataColumn(FlagCrudTag, typeof (String));
         newColumn.DefaultValue = "R";
         dataRight.Columns.Add(newColumn);

         ViewState["DataGridRight"] = dataRight.Copy();
         ViewState["DataGridRight_Originale"] = dataRight.Copy();

         fdtgRight.DefaultSortDirection = SortDirection.Ascending;
         fdtgRight.DefaultSortExpression = dataRight.Columns[0].ColumnName;
         fdtgRight.UpdateDataSource();
         lbCountDataRight.Text = fdtgRight.Rows.Count.ToString();
      }

      protected void RebindPage()
      {
         fdtgRight.UpdateDataSource();
         lbCountDataRight.Text = fdtgRight.Rows.Count.ToString();

         fdtgLeft.UpdateDataSource();
         lbCountDataLeft.Text = fdtgLeft.Rows.Count.ToString();
      }

      #endregion

      #region AjaxControlBase Members

      protected override void SetDefaultValues()
      {
      }

      protected override void OnDoPostBackChanged(bool doPostBack)
      {
         base.OnDoPostBackChanged(doPostBack);
      }

      protected override void OnEnabledChanged(bool enabled)
      {
         base.OnEnabledChanged(enabled);
         fdtgLeft.Enabled = enabled;
         fdtgRight.Enabled = enabled;
      }

      #endregion

      #region GridView

      protected void fdtgLeft_RowDataBound(object sender, GridViewRowEventArgs e)
      {
         var rowType = e.Row.RowType;
         try
         {
            e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;

            if (rowType == DataControlRowType.Header)
            {
            }
            if (rowType == DataControlRowType.DataRow)
            {
               var _colsNoVisible = fdtgLeft.DataKeys[e.Row.RowIndex].Values;
               var _flag_cod = _colsNoVisible[0].ToString();

               if (_flag_cod == "R")
               {
                  e.Row.Attributes.Add("class", "text-danger");
               }
            }
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void fdtgRight_RowDataBound(object sender, GridViewRowEventArgs e)
      {
         var rowType = e.Row.RowType;
         try
         {
            e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            if (rowType == DataControlRowType.Header)
            {
            }
            if (rowType == DataControlRowType.DataRow)
            {
               var _colsNoVisible = fdtgRight.DataKeys[e.Row.RowIndex].Values;
               var _flag_cod = _colsNoVisible[0].ToString();

               if (_flag_cod == "L")
               {
                  e.Row.Attributes.Add("class", "text-success");
               }
            }
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void fdtgLeft_RowCommand(object sender, GridViewCommandEventArgs e)
      {
         try
         {
            switch (e.CommandName.ToLower())
            {
               case "moveright":
                  var index = Convert.ToInt32(e.CommandArgument);
                  moveToRight(fdtgLeft.Rows[index]);
                  RebindPage();
                  break;
               default:
                  break;
            }
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void fdtgRight_RowCommand(object sender, GridViewCommandEventArgs e)
      {
         try
         {
            switch (e.CommandName.ToLower())
            {
               case "moveleft":
                  var index = Convert.ToInt32(e.CommandArgument);
                  moveToLeft(fdtgRight.Rows[index]);
                  RebindPage();
                  return;
               default:
                  break;
            }
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void fdtgLeft_DataSourceUpdating(object sender, EventArgs e)
      {
         fdtgLeft.DataSource = ViewState["DataGridLeft"];
         fdtgLeft.DataKeyNames = new[] {FlagCrudTag};
      }

      protected void fdtgRight_DataSourceUpdating(object sender, EventArgs e)
      {
         fdtgRight.DataSource = ViewState["DataGridRight"];
         fdtgRight.DataKeyNames = new[] {FlagCrudTag};
      }

      #endregion

      #region Buttons Centro

      protected void btnMoveAllRight_Click(object sender, EventArgs e)
      {
         try
         {
            foreach (GridViewRow grLeft in fdtgLeft.Rows)
            {
               moveToRight(grLeft);
            }
            RebindPage();
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void btnMoveRight_Click(object sender, EventArgs e)
      {
         try
         {
            foreach (GridViewRow grLeft in fdtgLeft.Rows)
            {
               var _chkSel = (CheckBox) grLeft.Cells[1].FindControl("chkSelectLeft");
               if (_chkSel.Checked)
               {
                  moveToRight(grLeft);
               }
            }
            RebindPage();
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void moveToRight(GridViewRow aLeftRow)
      {
         var _tableRightOriginale = (DataTable) ViewState["DataGridRight_Originale"];
         var _tableLeftOriginale = (DataTable) ViewState["DataGridLeft_Originale"];

         var _tableRight = (DataTable) ViewState["DataGridRight"];
         var _tableLeft = (DataTable) ViewState["DataGridLeft"];

         var _dataRigthToAddRow = _tableRight.NewRow();
         var _dataLeftRowToRemove = _tableLeft.NewRow();

         var _dataRigthOriginaleToAddRow = _tableRightOriginale.NewRow();
         var _dataLeftOriginaleRowToRemove = _tableLeftOriginale.NewRow();

         for (var i = 2; i < aLeftRow.Cells.Count; i++)
         {
            _dataRigthToAddRow[i - 2] = aLeftRow.Cells[i].Text;
            _dataRigthOriginaleToAddRow[i - 2] = aLeftRow.Cells[i].Text;
            _dataLeftRowToRemove[i - 2] = aLeftRow.Cells[i].Text;
            _dataLeftOriginaleRowToRemove[i - 2] = aLeftRow.Cells[i].Text;
         }

         _tableRight.Rows.Add(_dataRigthToAddRow);
         _tableRightOriginale.Rows.Add(_dataRigthOriginaleToAddRow);

         //Remove to row _tableLeft

         var _expr_filter = "";

         if (_dataLeftRowToRemove.ItemArray.Length == 1)
         {
            _expr_filter += _dataLeftRowToRemove.Table.Columns[0].ColumnName + " = '" +
                            _dataLeftRowToRemove.ItemArray[0] + "'";
         }
         else
         {
            for (var i = 0; i < _dataLeftRowToRemove.ItemArray.Length; i++)
            {
               if (i == _dataLeftRowToRemove.ItemArray.Length - 1)
               {
                  _expr_filter += _dataLeftRowToRemove.Table.Columns[i].ColumnName + " = '" +
                                  _dataLeftRowToRemove.ItemArray[i] + "'";
               }
               else
               {
                  _expr_filter += _dataLeftRowToRemove.Table.Columns[i].ColumnName + " = '" +
                                  _dataLeftRowToRemove.ItemArray[i] + "' and ";
               }
            }
         }


         var _rowRemove = _tableLeft.Select(_expr_filter);
         var _rowRemoveOrginale = _tableLeftOriginale.Select(_expr_filter);

         _tableLeft.Rows.Remove(_rowRemove[0]);
         _tableLeftOriginale.Rows.Remove(_rowRemoveOrginale[0]);

         ViewState["DataGridRight"] = _tableRight;
         ViewState["DataGridLeft"] = _tableLeft;
      }

      protected void btnMoveAllLeft_Click(object sender, EventArgs e)
      {
         try
         {
            foreach (GridViewRow grRight in fdtgRight.Rows)
            {
               moveToLeft(grRight);
            }
            RebindPage();
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void btnMoveLeft_Click(object sender, EventArgs e)
      {
         try
         {
            foreach (GridViewRow grRight in fdtgRight.Rows)
            {
               var _chkSel = (CheckBox) grRight.Cells[1].FindControl("chkSelectRight");
               if (_chkSel.Checked)
               {
                  moveToLeft(grRight);
               }
            }
            RebindPage();
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void moveToLeft(GridViewRow aRightRow)
      {
         var _tableRight = (DataTable) ViewState["DataGridRight"];
         var _tableLeft = (DataTable) ViewState["DataGridLeft"];

         var _tableRightOriginale = (DataTable) ViewState["DataGridRight_Originale"];
         var _tableLeftOriginale = (DataTable) ViewState["DataGridLeft_Originale"];

         var _dataRigthRowToRemove = _tableRight.NewRow();
         var _dataLeftRowToAdd = _tableLeft.NewRow();

         var _dataRigthOriginaleRowToRemove = _tableRightOriginale.NewRow();
         var _dataLeftOriginaleRowToAdd = _tableLeftOriginale.NewRow();

         for (var i = 2; i < aRightRow.Cells.Count; i++)
         {
            _dataRigthRowToRemove[i - 2] = aRightRow.Cells[i].Text;
            _dataRigthOriginaleRowToRemove[i - 2] = aRightRow.Cells[i].Text;
            _dataLeftRowToAdd[i - 2] = aRightRow.Cells[i].Text;
            _dataLeftOriginaleRowToAdd[i - 2] = aRightRow.Cells[i].Text;
         }

         _tableLeft.Rows.Add(_dataLeftRowToAdd);
         _tableLeftOriginale.Rows.Add(_dataLeftOriginaleRowToAdd);

         //Remove to row _tableRight

         var _expr_filter = "";

         if (_dataRigthRowToRemove.ItemArray.Length == 1)
         {
            _expr_filter += _dataRigthRowToRemove.Table.Columns[0].ColumnName + " = '" +
                            _dataRigthRowToRemove.ItemArray[0] + "'";
         }
         else
         {
            for (var i = 0; i < _dataRigthRowToRemove.ItemArray.Length; i++)
            {
               if (i == _dataRigthRowToRemove.ItemArray.Length - 1)
               {
                  _expr_filter += _dataRigthRowToRemove.Table.Columns[i].ColumnName + " = '" +
                                  _dataRigthRowToRemove.ItemArray[i] + "'";
               }
               else
               {
                  _expr_filter += _dataRigthRowToRemove.Table.Columns[i].ColumnName + " = '" +
                                  _dataRigthRowToRemove.ItemArray[i] + "' and ";
               }
            }
         }


         var _rowRemove = _tableRight.Select(_expr_filter);
         var _rowRemoveOriginale = _tableRightOriginale.Select(_expr_filter);

         _tableRight.Rows.Remove(_rowRemove[0]);
         _tableRightOriginale.Rows.Remove(_rowRemoveOriginale[0]);

         ViewState["DataGridRight"] = _tableRight;
         ViewState["DataGridLeft"] = _tableLeft;
      }

      #endregion

      #region Buttons Filter

      protected void btnApplyLeft_Click(object sender, EventArgs e)
      {
         var _filterLeft = txtApplyLeft.Text;

         var _tableLeftOriginale = (DataTable) ViewState["DataGridLeft_Originale"];


         var _rowResults = new List<DataRow>();
         for (var i = 0; i < _tableLeftOriginale.Rows.Count; i++)
         {
            var _addRow = false;
            for (var j = 0; j < _tableLeftOriginale.Rows[i].Table.Columns.Count; j++)
            {
               if (_tableLeftOriginale.Rows[i].ItemArray[j].ToString().ToLower().Contains(_filterLeft.ToLower()))
               {
                  _addRow = true;
               }
            }
            if (_addRow)
            {
               _rowResults.Add(_tableLeftOriginale.Rows[i]);
            }
         }

         //Aplicar il filter

         var _tableLeft = (DataTable) ViewState["DataGridLeft"];
         _tableLeft.DefaultView.Table.Clear();

         for (var i = 0; i < _rowResults.Count; i++)
         {
            _tableLeft.ImportRow(_rowResults[i]);
         }

         fdtgLeft.UpdateDataSource();
         lbCountDataLeft.Text = fdtgLeft.Rows.Count.ToString();
      }

      protected void btnApplyRight_Click(object sender, EventArgs e)
      {
         var _filterRight = txtApplyRight.Text;

         var _tableRightOriginale = (DataTable) ViewState["DataGridRight_Originale"];


         var _rowResults = new List<DataRow>();
         for (var i = 0; i < _tableRightOriginale.Rows.Count; i++)
         {
            var _addRow = false;
            for (var j = 0; j < _tableRightOriginale.Rows[i].Table.Columns.Count; j++)
            {
               if (_tableRightOriginale.Rows[i].ItemArray[j].ToString().ToLower().Contains(_filterRight.ToLower()))
               {
                  _addRow = true;
               }
            }
            if (_addRow)
            {
               _rowResults.Add(_tableRightOriginale.Rows[i]);
            }
         }

         //Aplicar il filter

         var _tableRight = (DataTable) ViewState["DataGridRight"];
         _tableRight.DefaultView.Table.Clear();

         for (var i = 0; i < _rowResults.Count; i++)
         {
            _tableRight.ImportRow(_rowResults[i]);
         }

         fdtgRight.UpdateDataSource();
         lbCountDataRight.Text = fdtgRight.Rows.Count.ToString();
      }

      protected void btnClearLeft_Click(object sender, EventArgs e)
      {
         txtApplyLeft.Text = "";
         var _filterLeft = txtApplyLeft.Text;

         var _tableLeftOriginale = (DataTable) ViewState["DataGridLeft_Originale"];


         var _rowResults = new List<DataRow>();
         for (var i = 0; i < _tableLeftOriginale.Rows.Count; i++)
         {
            var _addRow = false;
            for (var j = 0; j < _tableLeftOriginale.Rows[i].Table.Columns.Count; j++)
            {
               if (_tableLeftOriginale.Rows[i].ItemArray[j].ToString().ToLower().Contains(_filterLeft.ToLower()))
               {
                  _addRow = true;
               }
            }
            if (_addRow)
            {
               _rowResults.Add(_tableLeftOriginale.Rows[i]);
            }
         }

         //Aplicar il filter

         var _tableLeft = (DataTable) ViewState["DataGridLeft"];
         _tableLeft.DefaultView.Table.Clear();

         for (var i = 0; i < _rowResults.Count; i++)
         {
            _tableLeft.ImportRow(_rowResults[i]);
         }

         fdtgLeft.UpdateDataSource();
         lbCountDataLeft.Text = fdtgLeft.Rows.Count.ToString();
      }

      protected void btnClearRight_Click(object sender, EventArgs e)
      {
         txtApplyRight.Text = "";
         var _filterRight = txtApplyRight.Text;

         var _tableRightOriginale = (DataTable) ViewState["DataGridRight_Originale"];


         var _rowResults = new List<DataRow>();
         for (var i = 0; i < _tableRightOriginale.Rows.Count; i++)
         {
            var _addRow = false;
            for (var j = 0; j < _tableRightOriginale.Rows[i].Table.Columns.Count; j++)
            {
               if (_tableRightOriginale.Rows[i].ItemArray[j].ToString().ToLower().Contains(_filterRight.ToLower()))
               {
                  _addRow = true;
               }
            }
            if (_addRow)
            {
               _rowResults.Add(_tableRightOriginale.Rows[i]);
            }
         }

         //Aplicar il filter

         var _tableRight = (DataTable) ViewState["DataGridRight"];
         _tableRight.DefaultView.Table.Clear();

         for (var i = 0; i < _rowResults.Count; i++)
         {
            _tableRight.ImportRow(_rowResults[i]);
         }

         fdtgRight.UpdateDataSource();
         lbCountDataRight.Text = fdtgRight.Rows.Count.ToString();
      }

      #endregion
   }
}
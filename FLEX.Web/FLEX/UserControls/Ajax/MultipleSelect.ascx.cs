using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FLEX.Common;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using PommaLabs.GRAMPA;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class MultipleSelect : AjaxControlBase
   {
      protected const string flagCod = "flag_cod";

      protected void Page_Load(object sender, EventArgs e)
      {


      }

      #region Public Properties
      
      public static string FlagCod
      {
         get { return flagCod; }
      }

      public string TitlePanelLeft
      {
         set { lbTitlePanelLeft .Text= value; }
      }

      public string TitlePanelRight
      {
         set { lbTitlePanelRight.Text = value; }
      }


      public DataTable DataTableRight
      {
         get { return (DataTable)ViewState["DataGridRight"]; }
      }


      public DataTable DataTableLeft
      {
         get { return (DataTable)ViewState["DataGridLeft"]; }
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
      public void SetDataSourceLeft(DataTable dataLeft)
      {
         System.Data.DataColumn newColumn = new System.Data.DataColumn(flagCod, typeof(System.String));
         newColumn.DefaultValue = "L";
         dataLeft.Columns.Add(newColumn);

         ViewState["DataGridLeft"] = dataLeft.Copy();
         ViewState["DataGridLeft_Originale"] = dataLeft.Copy();

         fdtgLeft.DefaultSortDirection = SortDirection.Ascending;
         fdtgLeft.DefaultSortExpression = dataLeft.Columns[0].ColumnName;
         fdtgLeft.UpdateDataSource();
         lbCountDataLeft.Text = fdtgLeft.Rows.Count.ToString();
      }

      public void SetDataSourceRight(DataTable dataRight)
      {
         System.Data.DataColumn newColumn = new System.Data.DataColumn(flagCod, typeof(System.String));
         newColumn.DefaultValue = "R";
         dataRight.Columns.Add(newColumn);

         ViewState["DataGridRight"] = dataRight.Copy();
         ViewState["DataGridRight_Originale"] = dataRight.Copy();

         fdtgRight.DefaultSortDirection = SortDirection.Ascending;
         fdtgRight.DefaultSortExpression = dataRight.Columns[0].ColumnName;
         fdtgRight.UpdateDataSource();
         lbCountDataRight.Text = fdtgRight.Rows.Count.ToString(); 
      }

      protected void RebinPage()
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
                 
                 return;
              }
              else if (rowType == DataControlRowType.DataRow)
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
                 return;
              }
              else if (rowType == DataControlRowType.DataRow)
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
                    int index = Convert.ToInt32(e.CommandArgument);
                    moveToRight(fdtgLeft.Rows[index]);
                    RebinPage();
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
                    int index = Convert.ToInt32(e.CommandArgument);
                    moveToLeft(fdtgRight.Rows[index]);
                    RebinPage();
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
            fdtgLeft.DataKeyNames = new string[] { flagCod };
        }

        protected void fdtgRight_DataSourceUpdating(object sender, EventArgs e)
        {
           fdtgRight.DataSource = ViewState["DataGridRight"];
           fdtgRight.DataKeyNames = new string[] { flagCod };
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
              RebinPage();
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
                 CheckBox _chkSel = (CheckBox)grLeft.Cells[1].FindControl("chkSelect");
                 if (_chkSel.Checked)
                 {
                    moveToRight(grLeft);
                 }
              }
              RebinPage();
           }
           catch (Exception ex)
           {

              Master.ErrorHandler.CatchException(ex);
           }

        }

        protected void moveToRight(GridViewRow aLeftRow)
        {
           DataTable _tableRightOriginale = (DataTable)ViewState["DataGridRight_Originale"];
           DataTable _tableLeftOriginale = (DataTable)ViewState["DataGridLeft_Originale"];

           DataTable _tableRight = (DataTable)ViewState["DataGridRight"];
           DataTable _tableLeft = (DataTable)ViewState["DataGridLeft"];

           DataRow _dataRigthToAddRow = _tableRight.NewRow();
           DataRow _dataLeftRowToRemove = _tableLeft.NewRow();

           DataRow _dataRigthOriginaleToAddRow = _tableRightOriginale.NewRow();
           DataRow _dataLeftOriginaleRowToRemove = _tableLeftOriginale.NewRow();
           
           for (int i = 2; i < aLeftRow.Cells.Count; i++)
           {

              _dataRigthToAddRow[i - 2] = aLeftRow.Cells[i].Text;
              _dataRigthOriginaleToAddRow[i - 2] = aLeftRow.Cells[i].Text;
              _dataLeftRowToRemove[i - 2] = aLeftRow.Cells[i].Text;
              _dataLeftOriginaleRowToRemove[i - 2] = aLeftRow.Cells[i].Text;
           }

           _tableRight.Rows.Add(_dataRigthToAddRow);
           _tableRightOriginale.Rows.Add(_dataRigthOriginaleToAddRow);

           //Remove to row _tableLeft

             string _expr_filter = "";

             if (_dataLeftRowToRemove.ItemArray.Length == 1)
             {
                _expr_filter += _dataLeftRowToRemove.Table.Columns[0].ColumnName + " = '" + _dataLeftRowToRemove.ItemArray[0].ToString() +"'";
             }
             else 
             {
                for (int i = 0; i < _dataLeftRowToRemove.ItemArray.Length; i++)
                {
                   if (i == _dataLeftRowToRemove.ItemArray.Length - 1)
                   {
                      _expr_filter += _dataLeftRowToRemove.Table.Columns[i].ColumnName + " = '" + _dataLeftRowToRemove.ItemArray[i].ToString() + "'";
                   }
                   else
                   {
                      _expr_filter += _dataLeftRowToRemove.Table.Columns[i].ColumnName + " = '" + _dataLeftRowToRemove.ItemArray[i].ToString() + "' and ";
                   }
                }
             }


             DataRow[] _rowRemove = _tableLeft.Select(_expr_filter);
             DataRow[] _rowRemoveOrginale = _tableLeftOriginale.Select(_expr_filter);

             _tableLeft.Rows.Remove(_rowRemove[0]);
             _tableLeftOriginale.Rows.Remove(_rowRemoveOrginale[0]);

              ViewState["DataGridRight"]=_tableRight;
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
              RebinPage();
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
                 CheckBox _chkSel = (CheckBox)grRight.Cells[1].FindControl("chkSelect");
                 if (_chkSel.Checked)
                 {
                    moveToLeft(grRight);
                 }
              }
              RebinPage();
           }
           catch (Exception ex)
           {

              Master.ErrorHandler.CatchException(ex);
           }

        }
      
        protected void moveToLeft(GridViewRow aRightRow)
        {

           DataTable _tableRight = (DataTable)ViewState["DataGridRight"];
           DataTable _tableLeft = (DataTable)ViewState["DataGridLeft"];

           DataTable _tableRightOriginale = (DataTable)ViewState["DataGridRight_Originale"];
           DataTable _tableLeftOriginale = (DataTable)ViewState["DataGridLeft_Originale"];

           DataRow _dataRigthRowToRemove = _tableRight.NewRow();
           DataRow _dataLeftRowToAdd = _tableLeft.NewRow();

           DataRow _dataRigthOriginaleRowToRemove = _tableRightOriginale.NewRow();
           DataRow _dataLeftOriginaleRowToAdd = _tableLeftOriginale.NewRow();

           for (int i = 2; i < aRightRow.Cells.Count; i++)
           {

              _dataRigthRowToRemove[i - 2] = aRightRow.Cells[i].Text;
              _dataRigthOriginaleRowToRemove[i - 2] = aRightRow.Cells[i].Text;
              _dataLeftRowToAdd[i - 2] = aRightRow.Cells[i].Text;
              _dataLeftOriginaleRowToAdd[i - 2] = aRightRow.Cells[i].Text;
           }

           _tableLeft.Rows.Add(_dataLeftRowToAdd);
           _tableLeftOriginale.Rows.Add(_dataLeftOriginaleRowToAdd);

           //Remove to row _tableRight

           string _expr_filter = "";

           if (_dataRigthRowToRemove.ItemArray.Length == 1)
           {
              _expr_filter += _dataRigthRowToRemove.Table.Columns[0].ColumnName + " = '" + _dataRigthRowToRemove.ItemArray[0].ToString() + "'";
           }
           else
           {
              for (int i = 0; i < _dataRigthRowToRemove.ItemArray.Length; i++)
              {
                 if (i == _dataRigthRowToRemove.ItemArray.Length - 1)
                 {
                    _expr_filter += _dataRigthRowToRemove.Table.Columns[i].ColumnName + " = '" + _dataRigthRowToRemove.ItemArray[i].ToString() + "'";
                 }
                 else
                 {
                    _expr_filter += _dataRigthRowToRemove.Table.Columns[i].ColumnName + " = '" + _dataRigthRowToRemove.ItemArray[i].ToString() + "' and ";
                 }
              }
           }


           DataRow[] _rowRemove = _tableRight.Select(_expr_filter);
           DataRow[] _rowRemoveOriginale = _tableRightOriginale.Select(_expr_filter);

           _tableRight.Rows.Remove(_rowRemove[0]);
           _tableRightOriginale.Rows.Remove(_rowRemoveOriginale[0]);

           ViewState["DataGridRight"] = _tableRight;
           ViewState["DataGridLeft"] = _tableLeft;
        }

      #endregion

      #region Buttons Filter
        protected void btnApplyLeft_Click(object sender, EventArgs e)
        {
           string _filterLeft= txtApplyLeft.Text;

           DataTable _tableLeftOriginale = (DataTable)ViewState["DataGridLeft_Originale"];


           List<DataRow> _rowResults = new List<DataRow>();
           for (int i = 0; i < _tableLeftOriginale.Rows.Count; i++)
           {
              bool _addRow= false;
              for (int j = 0; j < _tableLeftOriginale.Rows[i].Table.Columns.Count; j++)
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

           DataTable _tableLeft = (DataTable)ViewState["DataGridLeft"];
           _tableLeft.DefaultView.Table.Clear();

           for (int i = 0; i < _rowResults.Count; i++)
           {
              _tableLeft.ImportRow(_rowResults[i]);
           }

           fdtgLeft.UpdateDataSource();
           lbCountDataLeft.Text = fdtgLeft.Rows.Count.ToString();
        }

        protected void btnApplyRight_Click(object sender, EventArgs e)
        {
           string _filterRight = txtApplyRight.Text;
           
           DataTable _tableRightOriginale = (DataTable)ViewState["DataGridRight_Originale"];


           List<DataRow> _rowResults = new List<DataRow>();
           for (int i = 0; i < _tableRightOriginale.Rows.Count; i++)
           {
              bool _addRow = false;
              for (int j = 0; j < _tableRightOriginale.Rows[i].Table.Columns.Count; j++)
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

           DataTable _tableRight = (DataTable)ViewState["DataGridRight"];
           _tableRight.DefaultView.Table.Clear();

           for (int i = 0; i < _rowResults.Count; i++)
           {
              _tableRight.ImportRow(_rowResults[i]);
           }

           fdtgRight.UpdateDataSource();
           lbCountDataRight.Text = fdtgRight.Rows.Count.ToString();
        }

        protected void btnClearLeft_Click(object sender, EventArgs e)
        {
           txtApplyLeft.Text = "";
           string _filterLeft = txtApplyLeft.Text;

           DataTable _tableLeftOriginale = (DataTable)ViewState["DataGridLeft_Originale"];


           List<DataRow> _rowResults = new List<DataRow>();
           for (int i = 0; i < _tableLeftOriginale.Rows.Count; i++)
           {
              bool _addRow = false;
              for (int j = 0; j < _tableLeftOriginale.Rows[i].Table.Columns.Count; j++)
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

           DataTable _tableLeft = (DataTable)ViewState["DataGridLeft"];
           _tableLeft.DefaultView.Table.Clear();

           for (int i = 0; i < _rowResults.Count; i++)
           {
              _tableLeft.ImportRow(_rowResults[i]);
           }

           fdtgLeft.UpdateDataSource();
           lbCountDataLeft.Text = fdtgLeft.Rows.Count.ToString();

        }

        protected void btnClearRight_Click(object sender, EventArgs e)
        {
           txtApplyRight.Text = "";
           string _filterRight = txtApplyRight.Text;

           DataTable _tableRightOriginale = (DataTable)ViewState["DataGridRight_Originale"];


           List<DataRow> _rowResults = new List<DataRow>();
           for (int i = 0; i < _tableRightOriginale.Rows.Count; i++)
           {
              bool _addRow = false;
              for (int j = 0; j < _tableRightOriginale.Rows[i].Table.Columns.Count; j++)
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

           DataTable _tableRight = (DataTable)ViewState["DataGridRight"];
           _tableRight.DefaultView.Table.Clear();

           for (int i = 0; i < _rowResults.Count; i++)
           {
              _tableRight.ImportRow(_rowResults[i]);
           }

           fdtgRight.UpdateDataSource();
           lbCountDataRight.Text = fdtgRight.Rows.Count.ToString();
        }
      #endregion
   }
}
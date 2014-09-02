using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public partial class ExportList : ControlBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         Master.ScriptManager.RegisterPostBackControl(lnkbExcel);
         Master.ScriptManager.RegisterPostBackControl(lnkbCSV);
         Master.ScriptManager.RegisterPostBackControl(lnkbPDF);
      }

      #region Public Properties

      public bool ExcelEnabled
      {
         set { lnkbExcel.Enabled = value; }
      }

      public bool PdfEnabled
      {
         set { lnkbPDF.Enabled = value; }
      }

      public bool CSVEnabled
      {
         set { lnkbCSV.Enabled = value; }
      }

      public LinkButton LnkExcel
      {
         get { return lnkbExcel; }
      }

      public LinkButton LnkPdf
      {
         get { return lnkbPDF; }
      }

      public LinkButton LnkCSV
      {
         get { return lnkbCSV; }
      }

      public DataTable DataSource { get; set; }

      public string ReportName { get; set; }

      #endregion

      #region Public Methods

      public void SetDataSource(GridView gridview, string nameDataTable, int[] arrColumn = null)
      {
         arrColumn = arrColumn ?? new int[0];
         var dtU = new DataTable();

         if (gridview.HeaderRow != null)
         {
            foreach (var t in arrColumn)
            {
               dtU.Columns.Add(gridview.Columns[t].HeaderText);
            }
         }

         foreach (GridViewRow row in gridview.Rows)
         {
            var dr = dtU.NewRow();

            for (var i = 0; i < arrColumn.Length; i++)
            {
               //Colonna BounField 
               if (row.Cells[arrColumn[i]].Controls.Count == 0)
                  dr[i] = row.Cells[arrColumn[i]].Text;
               else //Colonna TemplateField
               {
                  dr[i] = "";
                  for (var j = 0; j < row.Cells[arrColumn[i]].Controls.Count; j++)
                  {
                     var control = row.Cells[arrColumn[i]].Controls[j];

                     var checkBox = control as CheckBox;
                     if (checkBox != null)
                     {
                        var _chckb = checkBox;
                        if (_chckb.Checked)
                        {
                           dr[i] += _chckb.Text + " ";
                        }
                        continue;
                     }

                     var checkBoxList = control as CheckBoxList;
                     if (checkBoxList != null)
                     {
                        var _chckbList = checkBoxList;
                        for (var index = 0; index < _chckbList.Items.Count; index++)
                        {
                           if (_chckbList.Items[index].Selected)
                           {
                              dr[i] += _chckbList.Items[index].Text + " ";
                           }
                        }
                        continue;
                     }

                     var longTextContainer = control as LongTextContainer;
                     if (longTextContainer != null)
                     {
                        dr[i] += longTextContainer.Text + " ";
                        continue;
                     }

                     var label = control as Label;
                     if (label != null)
                     {
                        dr[i] += label.Text + " ";
                        continue;
                     }

                     var linkButton = control as LinkButton;
                     if (linkButton != null)
                     {
                        var text = linkButton.Text;
                        if (!text.Contains("<span"))
                           dr[i] += text + " ";
                     }
                  }
               }
            }
            dtU.Rows.Add(dr);
         }
         dtU.TableName = nameDataTable;
         DataSource = dtU;
      }

      public void SetDataSource(GridView gridview, string nameDataTable, params ColumnData[] columnData)
      {
         var oldPageSize = gridview.PageSize;
         gridview.PageSize = (gridview.DataSource as DataTable).Rows.Count;
         gridview.DataBind();

         columnData = columnData ?? new ColumnData[0];
         var dtU = new DataTable();

         if (gridview.HeaderRow != null)
         {
            foreach (var c in columnData)
            {
               dtU.Columns.Add(c.Name);
            }
         }

       
         foreach (GridViewRow row in gridview.Rows)
         {
            var dr = dtU.NewRow();

            int di = 0;
            foreach (var c in columnData)
            {
               switch (c.Type)
               {
                  case ColumnType.DataKey:
                     dr[di] = gridview.DataKeys[row.RowIndex].Values[c.Index];
                     break;

                  case ColumnType.Column:
                     //Colonna BounField 
                     if (row.Cells[c.Index].Controls.Count == 0)
                        dr[di] = row.Cells[c.Index].Text;
                     else //Colonna TemplateField
                     {
                        dr[di] = "";
                        for (var j = 0; j < row.Cells[c.Index].Controls.Count; j++)
                        {
                           var control = row.Cells[c.Index].Controls[j];

                           var checkBox = control as CheckBox;
                           if (checkBox != null)
                           {
                              if (checkBox.Checked)
                              {
                                 dr[di] += checkBox.Text + " ";
                              }
                              continue;
                           }

                           var checkBoxList = control as CheckBoxList;
                           if (checkBoxList != null)
                           {
                              for (var index = 0; index < checkBoxList.Items.Count; index++)
                              {
                                 if (checkBoxList.Items[index].Selected)
                                 {
                                    dr[di] += checkBoxList.Items[index].Text + " ";
                                 }
                              }
                              continue;
                           }

                           var longTextContainer = control as LongTextContainer;
                           if (longTextContainer != null)
                           {
                              dr[di] += longTextContainer.Text + " ";
                              continue;
                           }

                           var label = control as Label;
                           if (label != null)
                           {
                              dr[di] += label.Text + " ";
                              continue;
                           }

                           var linkButton = control as LinkButton;
                           if (linkButton != null)
                           {
                              var text = linkButton.Text;
                              if (!text.Contains("<span"))
                                 dr[di] += text + " ";
                           }
                        }
                     }
                     break;
               }
               di++;
            }

            dtU.Rows.Add(dr);
           
         }

         dtU.TableName = nameDataTable;
         DataSource = dtU;

         gridview.PageSize = oldPageSize;
         gridview.DataBind();
      }

      protected void ExportList_OnClickExcel(object sender, EventArgs e)
      {
         try
         {
            DataSourceNeeded(sender, e);
            if (DataSource != null)
            {
               var wb = new XLWorkbook();
               wb.AddWorksheet(DataSource);
               //wb.Worksheets.Add(DataSource);

               var response = HttpContext.Current.Response;

               response.AddHeader("content-disposition", "attachment;filename=" + ReportName + ".xlsx");
               response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

               using (var memoryStream = new MemoryStream())
               {
                  wb.SaveAs(memoryStream);
                  memoryStream.WriteTo(response.OutputStream);
               }
               response.OutputStream.Flush();
               response.End();
            }
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void ExportList_OnClickPdf(object sender, EventArgs e)
      {
         try
         {
            DataSourceNeeded(sender, e);
            if (DataSource != null)
            {
               var pdfDoc = new Document(PageSize.A4.Rotate(), 5, 10, 20, 20);

               PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
               var pdfTable = new PdfPTable(DataSource.Columns.Count) {WidthPercentage = 100};

               var headerFont = new Font(Font.HELVETICA, 8, Font.BOLD);
               foreach (DataColumn c in DataSource.Columns)
               {
                  pdfTable.AddCell(new Phrase(c.ColumnName, headerFont));
               }

               var bodyFont = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);
               for (var row = 0; row < DataSource.Rows.Count; row++)
               {
                  for (var col = 0; col < DataSource.Columns.Count; col++)
                  {
                     var cellValue = DataSource.Rows[row][col].ToString();
                     var pdfPCell = new PdfPCell(new Phrase(cellValue, bodyFont)) {HorizontalAlignment = Element.ALIGN_LEFT};
                     pdfTable.AddCell(pdfPCell);
                  }
               }

               pdfDoc.Open();
               pdfTable.SpacingBefore = 15.0F; //Give some space after the text or it may overlap the table            
               pdfDoc.Add(pdfTable); //add pdf table to the document   
               pdfDoc.Close();

               var response = HttpContext.Current.Response;
               response.AddHeader("content-disposition", "attachment;filename=" + ReportName + ".pdf");
               response.ContentType = "pdf/application";

               response.Write(pdfDoc);
               response.OutputStream.Flush();
               response.End();
            }
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      protected void ExportList_OnClickCSV(object sender, EventArgs e)
      {
         try
         {
            DataSourceNeeded(sender, e);
            if (DataSource != null)
            {
               var response = HttpContext.Current.Response;

               response.AddHeader("content-disposition", "attachment;filename=" + ReportName + ".csv");
               response.ContentType = "application/csv";

               using (var memoryStream = new MemoryStream())
               {
                  var csvWriter = new StreamWriter(memoryStream, Encoding.UTF8);
                  WriteDataTable(DataSource, csvWriter, true);
                  memoryStream.WriteTo(response.OutputStream);
               }

               response.OutputStream.Flush();
               response.End();
            }
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      private void WriteDataTable(DataTable sourceTable, StreamWriter writer, bool includeHeaders)
      {
         try
         {
            if (includeHeaders)
            {
               var headerValues = new List<string>();
               foreach (DataColumn column in DataSource.Columns)
               {
                  headerValues.Add(QuoteValue(column.ColumnName));
               }
               writer.WriteLine(string.Join(";", headerValues.ToArray()));
            }

            string[] items = null;
            foreach (DataRow row in DataSource.Rows)
            {
               items = row.ItemArray.Select(x => QuoteValue(x.ToString())).ToArray();
               writer.WriteLine(String.Join(";", items));
            }
            writer.Flush();
         }

         catch (ThreadAbortException)
         {
            throw;
         }
         catch (Exception ex)
         {
            Master.ErrorHandler.CatchException(ex);
         }
      }

      private static string QuoteValue(string value)
      {
         return string.Concat("\"", value.Replace("\"", "\"\""), "\"");
      }

      #endregion

      #region Public Members

      public event EventHandler DataSourceNeeded;

      #endregion

      public struct ColumnData
      {
         public string Name { get; set; }
         public int Index { get; set; }
         public ColumnType Type { get; set; }
      }

      public enum ColumnType
      {
         Column,
         DataKey
      }
   }
}
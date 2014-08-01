using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using FLEX.Common;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using Thrower;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using FLEX.Web.MasterPages;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public partial class ExportList : UserControl
   {
      protected ExportList()
      {
         // Empty, for now...
      }

      protected void Page_Load(object sender, EventArgs e)
      {
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

      protected void ExportList_OnClickExcel(object sender, EventArgs e)
      {
         try
         {
            DataSourceNeeded(sender, e);
            if (DataSource != null)
            {
               XLWorkbook wb = new XLWorkbook();
               wb.AddWorksheet(DataSource);
               //wb.Worksheets.Add(DataSource);

               var response = HttpContext.Current.Response;

               response.Clear();
               response.ClearContent();
               response.ClearHeaders();

               response.AddHeader("content-disposition", "attachment;filename=" + ReportName + "Excel.xlsx");
               response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

               using (var memoryStream = new MemoryStream())
               {
                  wb.SaveAs(memoryStream);
                  memoryStream.WriteTo(response.OutputStream);
               }
               response.OutputStream.Flush();
               response.OutputStream.Close();
               response.End();
            }
         }

         catch (System.Threading.ThreadAbortException)
         {
            throw;
         }

         catch (Exception ex)
         {
           (Page.Master as IPageBase).ErrorHandler.CatchException(ex);
         }

      }

      protected void ExportList_OnClickPdf(object sender, EventArgs e)
      {
         try
         {
            DataSourceNeeded(sender, e);
            if (DataSource != null)
            {
               Document pdfDoc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 10, 20, 20);
               //pdfDoc.SetPageSize(PageSize.A4.Rotate());



               PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
               PdfPTable PdfTable = new PdfPTable(DataSource.Columns.Count);
               PdfTable.WidthPercentage = 100;

               iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, Font.BOLD);
               PdfPCell pdfPCell = null;

               foreach (DataColumn c in DataSource.Columns)
               {
                  PdfTable.AddCell(new Phrase(c.ColumnName, font));
               }

               for (int row = 0; row < DataSource.Rows.Count; row++)
               {
                  for (int col = 0; col < DataSource.Columns.Count; col++)
                  {
                     pdfPCell = new PdfPCell(new Phrase(new Chunk(DataSource.Rows[row][col].ToString())));
                     pdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                     PdfTable.AddCell(pdfPCell);
                  }
               }
               pdfDoc.Open();
               PdfTable.SpacingBefore = 15.0F; //Give some space after the text or it may overlap the table            
               pdfDoc.Add(PdfTable); //add pdf table to the document   
               pdfDoc.Close();

               var response = HttpContext.Current.Response;
               response.Clear();
               response.ClearContent();
               response.ClearHeaders();
               response.AddHeader("content-disposition", "attachment;filename=" + ReportName + "PDF.pdf");
               response.ContentType = "pdf/application";
               
               response.Write(pdfDoc);
               response.OutputStream.Flush();
               response.OutputStream.Close();
               response.End();
            }
         }

         catch (System.Threading.ThreadAbortException)
         {
            throw;
         }
         catch (Exception ex)
         {
            (Page.Master as IPageBase).ErrorHandler.CatchException(ex);
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
               response.Clear();
               response.ClearContent();
               response.ClearHeaders();

               response.AddHeader("content-disposition", "attachment;filename=" + ReportName + "CSV.csv");
               response.ContentType = "application/csv";

               using (var memoryStream = new MemoryStream())
               {
                  StreamWriter csvWriter = new StreamWriter(memoryStream, Encoding.UTF8);
                  WriteDataTable(DataSource, csvWriter, true);
                  memoryStream.WriteTo(response.OutputStream);
               }

               response.OutputStream.Flush();
               response.OutputStream.Close();
               response.End();
            }
         }

         catch (System.Threading.ThreadAbortException) 
         {
            throw;
         }
         catch (Exception ex)
         {

            (Page.Master as IPageBase).ErrorHandler.CatchException(ex);
         }
      }

      private void WriteDataTable(DataTable sourceTable, StreamWriter writer, bool includeHeaders)
      {
         try
         {
            if (includeHeaders)
            {
               List<string> headerValues = new List<string>();
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

         catch (System.Threading.ThreadAbortException)
         {
            throw;
         }
         catch (Exception ex)
         {
            (Page.Master as IPageBase).ErrorHandler.CatchException(ex);
         }
      }

      private string QuoteValue(string value)
      {
          return string.Concat("\"", value.Replace("\"", "\"\""), "\"");
      }
  
      #endregion

      #region Public Members

      public event EventHandler DataSourceNeeded;

      #endregion
   }
}
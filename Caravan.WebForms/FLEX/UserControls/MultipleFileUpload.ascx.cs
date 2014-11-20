using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace Finsa.Caravan.WebForms.UserControls
// ReSharper restore CheckNamespace
{
   /// <summary>
   /// This control is to upload multiple files.
   /// </summary>
   public partial class MultipleFileUpload : UserControl
   {
      //This is Click event defenition for MultipleFileUpload control.

      /// <summary>
      /// The no of visible rows to display.
      /// </summary>
      private int _rows = 6;

      /// <summary>
      /// The no of maximukm files to upload.
      /// </summary>
      private int _upperLimit;

      public int Rows
      {
         get { return _rows; }
         set { _rows = value < 6 ? 6 : value; }
      }

      public int UpperLimit
      {
         get { return _upperLimit; }
         set { _upperLimit = value; }
      }

      public event MultipleFileUploadClick Click;

      /// <summary>
      /// Methos for page load event.
      /// </summary>
      /// <param name="sender">Reference of the object that raises this event.</param>
      /// <param name="e">Contains information regarding page load click event data.</param>
      protected void Page_Load(object sender, EventArgs e)
      {
         lblCaption.Text = _upperLimit == 0 ? "Maximum Files: No Limit" : string.Format("Maximum Files: {0}", _upperLimit);
         pnlListBox.Attributes["style"] = "overflow:auto;";
         pnlListBox.Height = Unit.Pixel(20*_rows - 1);
         Page.ClientScript.RegisterStartupScript(typeof (Page), "MyScript", GetJavaScript());
      }

      /// <summary>
      /// Methods for btnUpload Click event. 
      /// </summary>
      /// <param name="sender">Reference of the object that raises this event.</param>
      /// <param name="e">Contains information regarding button click event data.</param>
      protected void btnUpload_Click(object sender, EventArgs e)
      {
         // Fire the event.
         Click(this, new FileCollectionEventArgs(Request));
      }

      /// <summary>
      /// This method is used to generate javascript code for MultipleFileUpload control that execute at client side.
      /// </summary>
      /// <returns>Javascript as a string object.</returns>
      private string GetJavaScript()
      {
         var javaScript = new StringBuilder();

         javaScript.Append("<script type='text/javascript'>");
         javaScript.Append("var Id = 0;\n");
         javaScript.AppendFormat("var MAX = {0};\n", _upperLimit);
         javaScript.AppendFormat("var DivFiles = document.getElementById('{0}');\n", pnlFiles.ClientID);
         javaScript.AppendFormat("var DivListBox = document.getElementById('{0}');\n", pnlListBox.ClientID);
         javaScript.AppendFormat("var BtnAdd = document.getElementById('{0}');\n", btnAdd.ClientID);
         javaScript.Append("function Add()");
         javaScript.Append("{\n");
         javaScript.Append("var IpFile = GetTopFile();\n");
         javaScript.Append("if(IpFile == null || IpFile.value == null || IpFile.value.length == 0)\n");
         javaScript.Append("{\n");
         javaScript.Append("alert('Please select a file to add.');\n");
         javaScript.Append("return;\n");
         javaScript.Append("}\n");
         javaScript.Append("var NewIpFile = CreateFile();\n");
         javaScript.Append("DivFiles.insertBefore(NewIpFile,IpFile);\n");
         javaScript.Append("if(MAX != 0 && GetTotalFiles() - 1 == MAX)\n");
         javaScript.Append("{\n");
         javaScript.Append("NewIpFile.disabled = true;\n");
         javaScript.Append("BtnAdd.disabled = true;\n");
         javaScript.Append("}\n");
         javaScript.Append("IpFile.style.display = 'none';\n");
         javaScript.Append("DivListBox.appendChild(CreateItem(IpFile));\n");
         javaScript.Append("}\n");
         javaScript.Append("function CreateFile()");
         javaScript.Append("{\n");
         javaScript.Append("var IpFile = document.createElement('input');\n");
         javaScript.Append("IpFile.id = IpFile.name = 'IpFile_' + Id++;\n");
         javaScript.Append("IpFile.type = 'file';\n");
         javaScript.Append("return IpFile;\n");
         javaScript.Append("}\n");
         javaScript.Append("function CreateItem(IpFile)\n");
         javaScript.Append("{\n");
         javaScript.Append("var Item = document.createElement('div');\n");
         javaScript.Append("Item.style.backgroundColor = '#ffffff';\n");
         javaScript.Append("Item.style.fontWeight = 'normal';\n");
         javaScript.Append("Item.style.textAlign = 'left';\n");
         javaScript.Append("Item.style.verticalAlign = 'middle'; \n");
         javaScript.Append("Item.style.cursor = 'default';\n");
         javaScript.Append("Item.style.height = 20 + 'px';\n");
         javaScript.Append("var Splits = IpFile.value.split('\\\\');\n");
         javaScript.Append("Item.innerHTML = Splits[Splits.length - 1] + '&nbsp;';\n");
         javaScript.Append("Item.value = IpFile.id;\n");
         javaScript.Append("Item.title = IpFile.value;\n");
         javaScript.Append("var A = document.createElement('a');\n");
         javaScript.Append("A.innerHTML = 'Delete';\n");
         javaScript.Append("A.id = 'A_' + Id++;\n");
         javaScript.Append("A.href = '#';\n");
         javaScript.Append("A.style.color = 'blue';\n");
         javaScript.Append("A.onclick = function()\n");
         javaScript.Append("{\n");
         javaScript.Append("DivFiles.removeChild(document.getElementById(this.parentNode.value));\n");
         javaScript.Append("DivListBox.removeChild(this.parentNode);\n");
         javaScript.Append("if(MAX != 0 && GetTotalFiles() - 1 < MAX)\n");
         javaScript.Append("{\n");
         javaScript.Append("GetTopFile().disabled = false;\n");
         javaScript.Append("BtnAdd.disabled = false;\n");
         javaScript.Append("}\n");
         javaScript.Append("}\n");
         javaScript.Append("Item.appendChild(A);\n");
         javaScript.Append("Item.onmouseover = function()\n");
         javaScript.Append("{\n");
         javaScript.Append("Item.bgColor = Item.style.backgroundColor;\n");
         javaScript.Append("Item.fColor = Item.style.color;\n");
         javaScript.Append("Item.style.backgroundColor = '#C6790B';\n");
         javaScript.Append("Item.style.color = '#ffffff';\n");
         javaScript.Append("Item.style.fontWeight = 'bold';\n");
         javaScript.Append("}\n");
         javaScript.Append("Item.onmouseout = function()\n");
         javaScript.Append("{\n");
         javaScript.Append("Item.style.backgroundColor = Item.bgColor;\n");
         javaScript.Append("Item.style.color = Item.fColor;\n");
         javaScript.Append("Item.style.fontWeight = 'normal';\n");
         javaScript.Append("}\n");
         javaScript.Append("return Item;\n");
         javaScript.Append("}\n");
         javaScript.Append("function Clear()\n");
         javaScript.Append("{\n");
         javaScript.Append("DivListBox.innerHTML = '';\n");
         javaScript.Append("DivFiles.innerHTML = '';\n");
         javaScript.Append("DivFiles.appendChild(CreateFile());\n");
         javaScript.Append("BtnAdd.disabled = false;\n");
         javaScript.Append("}\n");
         javaScript.Append("function GetTopFile()\n");
         javaScript.Append("{\n");
         javaScript.Append("var Inputs = DivFiles.getElementsByTagName('input');\n");
         javaScript.Append("var IpFile = null;\n");
         javaScript.Append("for(var n = 0; n < Inputs.length && Inputs[n].type == 'file'; ++n)\n");
         javaScript.Append("{\n");
         javaScript.Append("IpFile = Inputs[n];\n");
         javaScript.Append("break;\n");
         javaScript.Append("}\n");
         javaScript.Append("return IpFile;\n");
         javaScript.Append("}\n");
         javaScript.Append("function GetTotalFiles()\n");
         javaScript.Append("{\n");
         javaScript.Append("var Inputs = DivFiles.getElementsByTagName('input');\n");
         javaScript.Append("var Counter = 0;\n");
         javaScript.Append("for(var n = 0; n < Inputs.length && Inputs[n].type == 'file'; ++n)\n");
         javaScript.Append("Counter++;\n");
         javaScript.Append("return Counter;\n");
         javaScript.Append("}\n");
         javaScript.Append("function GetTotalItems()\n");
         javaScript.Append("{\n");
         javaScript.Append("var Items = DivListBox.getElementsByTagName('div');\n");
         javaScript.Append("return Items.length;\n");
         javaScript.Append("}\n");
         javaScript.Append("function DisableTop()\n");
         javaScript.Append("{\n");
         javaScript.Append("if(GetTotalItems() == 0)\n");
         javaScript.Append("{\n");
         javaScript.Append("alert('Please browse at least one file to upload.');\n");
         javaScript.Append("return false;\n");
         javaScript.Append("}\n");
         javaScript.Append("GetTopFile().disabled = true;\n");
         javaScript.Append("return true;\n");
         javaScript.Append("}\n");
         javaScript.Append("</script>");

         return javaScript.ToString();
      }
   }

   /// <summary>
   /// EventArgs class that has some readonly properties regarding posted files corresponding to MultipleFileUpload control. 
   /// </summary>
   public class FileCollectionEventArgs : EventArgs
   {
      private readonly HttpRequest _httpRequest;

      public FileCollectionEventArgs(HttpRequest oHttpRequest)
      {
         _httpRequest = oHttpRequest;
      }

      public HttpFileCollection PostedFiles
      {
         get { return _httpRequest.Files; }
      }

      public int Count
      {
         get { return _httpRequest.Files.Count; }
      }

      public bool HasFiles
      {
         get { return _httpRequest.Files.Count > 0; }
      }

      public double TotalSize
      {
         get
         {
            var size = 0D;
            for (var n = 0; n < _httpRequest.Files.Count; ++n)
            {
               if (_httpRequest.Files[n].ContentLength < 0)
                  continue;
               size += _httpRequest.Files[n].ContentLength;
            }

            return Math.Round(size/1024D, 2);
         }
      }
   }

//Delegate that represents the Click event signature for MultipleFileUpload control.
   public delegate void MultipleFileUploadClick(object sender, FileCollectionEventArgs e);
}
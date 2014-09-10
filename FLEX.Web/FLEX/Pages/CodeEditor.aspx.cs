using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using FLEX.Common.Data;
using PommaLabs.GRAMPA.Text;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class CodeEditor : PageBase
   {
      readonly Dictionary<string, string> _filesPath = new Dictionary<string, string>();
      
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!Page.IsPostBack)
         {
            ViewState["dictFilesPath"] = _filesPath;
            var scriptsPath = Armando.Configuration.Instance.ScriptsPath;
            var xmlPath = Server.MapPath(FLEX.Web.WebSettings.AjaxLookup_XmlPath);
            var menuPath = Server.MapPath(FLEX.Web.WebSettings.MenuBar_XmlPath);
            var files = new Dictionary<string, string> {{"Data Scritps", scriptsPath}, {"Ajax Lookup", xmlPath}, {"Menu", menuPath}};
            BuildTreeView(files);
         }
      }


      protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
      {
         if (TreeView1.SelectedNode.Text == "Data Scritps" || TreeView1.SelectedNode.Text == "Ajax Lookup" || TreeView1.SelectedNode.Text == "Menu")
         {
            txtContentFiles.Text = "";
            lbtnSave.Visible = false;
            txtContentFiles.Visible = false;
            divContenFiles.Visible = false;
            return;
         }
         else
         {
            divContenFiles.Visible = true;
            lbtnSave.Visible = true;
            txtContentFiles.Visible = true;
            var filesPath = (Dictionary<string, string>) ViewState["dictFilesPath"];

            var _name_files = TreeView1.SelectedNode.Text;
            string _text;
            using (var reader = new StreamReader(filesPath[_name_files]))
            {
               _text = reader.ReadToEnd();
            }
            txtContentFiles.Text = _text;
            string _extension = Path.GetExtension(filesPath[_name_files]);
            if (_extension == ".cs")
               System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeModeEditor", "ChangeModeEditor('cs');", true);
            else if (_extension == ".xml")
               System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeModeEditor", "ChangeModeEditor('xml');", true);

         }
      }

      public void BuildTreeView(Dictionary<string, string> _Files)
      {
         int index = 0;
         foreach (var item in _Files)
         {
            TreeView1.Nodes.Add(new TreeNode(item.Key));
            if (Path.GetExtension(item.Value) == "")
            {
               string[] files = Directory.GetFiles(item.Value);
               for (int i = 0; i < files.Length; i++)
               {
                  TreeView1.Nodes[index].ChildNodes.Add(new TreeNode(Path.GetFileName(files[i])));
                  _filesPath.Add(Path.GetFileName(files[i]), files[i]);
               }
               index++;
            }
            else
            {
               TreeView1.Nodes[index].ChildNodes.Add(new TreeNode(Path.GetFileName(item.Value)));
               _filesPath.Add(Path.GetFileName(item.Value), item.Value);
            }
         }
         TreeView1.DataBind();
      }

      protected void lbtnSave_Click(object sender, EventArgs e)
      {
         try
         {
            divContenFiles.Visible = true;
            var filesPath = (Dictionary<string, string>) ViewState["dictFilesPath"];

            var nameFiles = TreeView1.SelectedNode.Text;

            var oldFile = File.ReadAllText(filesPath[nameFiles]);
            var newFile = txtContentFiles.Text;
            using (var writer = new StreamWriter(filesPath[nameFiles], false))
            {
               writer.Write(newFile);
            }

            // Indico nel log che qualcuno ha modificato il file
            var logMsg = String.Format("File {0} has been changed by {1}", Path.GetFileName(filesPath[nameFiles]), HttpContext.Current.User.Identity.Name);
            var diff = new diff_match_patch();
            var diffList = diff.diff_main(oldFile, newFile);
            DbLogger.Instance.LogWarning<CodeEditor>(logMsg, diff.diff_prettyHtml(diffList));

            var extension = Path.GetExtension(filesPath[nameFiles]);
            if (extension == ".cs")
               System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeModeEditor", "ChangeModeEditor('cs');", true);
            else if (extension == ".xml")
               System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeModeEditor", "ChangeModeEditor('xml');", true);
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
      {
         lbtnSave.Visible = false;
         divContenFiles.Visible = false;
      }

      protected void TreeView1_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
      {
         lbtnSave.Visible = false;
         divContenFiles.Visible = false;
      }
   }
}
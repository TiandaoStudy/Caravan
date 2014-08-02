using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using FLEX.Common;
using FLEX.Common.Data;
using FLEX.Web.Pages;

namespace FLEX.Web.Pages
{
   public partial class CodeEditor : PageBase
   {
      Dictionary<string, string> FilesPath = new Dictionary<string, string>();
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!Page.IsPostBack)
         {
            ViewState["dictFilesPath"] = FilesPath;
            var scriptsPath = Armando.Configuration.Instance.ScriptsPath;
            var xmlPath = Server.MapPath(FLEX.Web.WebSettings.AjaxLookup_XmlPath);
            var menuPath = Server.MapPath(FLEX.Web.WebSettings.MenuBar_XmlPath);
            Dictionary<string, string> _Files = new Dictionary<string, string>();
            _Files.Add("Data Scritps", scriptsPath);
            _Files.Add("Ajax Lookup", xmlPath);
            _Files.Add("Menu", menuPath);
            BuildTreeView(_Files);
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
            Dictionary<string, string> _FilesPath = (Dictionary<string, string>)ViewState["dictFilesPath"];

            var _name_files = TreeView1.SelectedNode.Text;
            string _text;
            using (StreamReader reader = new StreamReader(_FilesPath[_name_files]))
            {
               _text = reader.ReadToEnd();
            }
            txtContentFiles.Text = _text;
            string _extension = Path.GetExtension(_FilesPath[_name_files]);
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
                  FilesPath.Add(Path.GetFileName(files[i]), files[i]);
               }
               index++;
            }
            else
            {
               TreeView1.Nodes[index].ChildNodes.Add(new TreeNode(Path.GetFileName(item.Value)));
               FilesPath.Add(Path.GetFileName(item.Value), item.Value);
            }
         }
         TreeView1.DataBind();
      }

      protected void lbtnSave_Click(object sender, EventArgs e)
      {
         divContenFiles.Visible = true;
         Dictionary<string, string> _FilesPath = (Dictionary<string, string>)ViewState["dictFilesPath"];

         var _name_files = TreeView1.SelectedNode.Text;

         using (StreamWriter writer = new StreamWriter(_FilesPath[_name_files], false))
         {
            writer.Write(txtContentFiles.Text);
         }

         var logMsg = String.Format("Data script {0} has been recompiled", _FilesPath[_name_files]);
         DbLogger.Instance.LogWarning<CodeEditor>("lbtnSave_Click", logMsg);

         string _extension = Path.GetExtension(_FilesPath[_name_files]);
         if (_extension == ".cs")
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeModeEditor", "ChangeModeEditor('cs');", true);
         else if (_extension == ".xml")
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "ChangeModeEditor", "ChangeModeEditor('xml');", true);


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
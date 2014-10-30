using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Finsa.Caravan.DataAccess;
using System.IO;
using System.Xml;
using System.Xml.XPath;    
// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class SecMenu : PageBase
   {
      readonly Dictionary<string, string> _filesPath = new Dictionary<string, string>();
      private XmlDocument docXML = new XmlDocument();

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!Page.IsPostBack)
         { 
            InitControls();
           

         }
      }

      private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
      {
         XmlNode xNode;
         TreeNode tNode;
         XmlNodeList nodeList;

         if (inXmlNode.HasChildNodes)
         {
            nodeList = inXmlNode.ChildNodes;
            for (int i = 0; i <= nodeList.Count - 1; i++)
            {
               xNode = inXmlNode.ChildNodes[i];
               if (xNode.Name == "Item")
               {
                 if(xNode.Attributes.GetNamedItem("Caption").Value != "Separator") 
                 {
                  inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes.GetNamedItem("Caption").Value));
                  tNode = inTreeNode.ChildNodes[inTreeNode.ChildNodes.Count-1];
                  AddNode(xNode, tNode);
                 }
                 else
                 {
                    AddNode(xNode, inTreeNode);
                 }

               }
               else
               {
                  AddNode(xNode, inTreeNode);
               }
            }
         }
      }      

      protected void InitControls() 
      {
         //Users
         DataTable _tableLeft = new DataTable();
         _tableLeft.Columns.Add("Id", typeof(int));
         _tableLeft.Columns.Add("Login", typeof(string));
         _tableLeft.Columns.Add("FirstName", typeof(string));
         _tableLeft.Columns.Add("LastName", typeof(string));

         DataTable _tableRight = new DataTable();
         _tableRight.Columns.Add("Id", typeof(int));
         _tableRight.Columns.Add("Login", typeof(string));
         _tableRight.Columns.Add("FirstName", typeof(string));
         _tableRight.Columns.Add("LastName", typeof(string));
        

    
         var  _users = Db.Security.Users(Finsa.Caravan.Common.Configuration.Instance.ApplicationName);
         foreach (var item in _users)
         {
            _tableLeft.Rows.Add(item.Id, item.Login, item.FirstName, item.LastName);
         }

         flexMultipleSelectUsers.SetLeftDataSource(_tableLeft);
         flexMultipleSelectUsers.SetRightDataSource(_tableRight);
         flexMultipleSelectUsers.LeftPanelTitle = "Available Users";
         flexMultipleSelectUsers.RightPanelTitle = "Chosen Users";

         //Groups
         DataTable _tableLeftGroups = new DataTable();
         _tableLeftGroups.Columns.Add("Id", typeof(int));
         _tableLeftGroups.Columns.Add("Name", typeof(string));

         DataTable _tableRightGroups = new DataTable();
         _tableRightGroups.Columns.Add("Id", typeof(int));
         _tableRightGroups.Columns.Add("Name", typeof(string));

         var _groups = Db.Security.Groups(Finsa.Caravan.Common.Configuration.Instance.ApplicationName);
         foreach (var item in _groups)
         {
            _tableLeftGroups.Rows.Add(item.Id, item.Name);
         }

         flexMultiSelectGroups.SetLeftDataSource(_tableLeftGroups);
         flexMultiSelectGroups.SetRightDataSource(_tableRightGroups);
         flexMultiSelectGroups.LeftPanelTitle = "Available Groups";
         flexMultiSelectGroups.RightPanelTitle = "Chosen Groups";

         // Legge il file XML
         docXML.Load(Server.MapPath(WebForms.Configuration.Instance.MenuBarXmlPath));

         TreeView1.Nodes.Clear();
         TreeView1.Nodes.Add(new TreeNode(docXML.DocumentElement.Name));
         TreeNode tNode = new TreeNode();
         tNode = TreeView1.Nodes[0];

         AddNode(docXML.DocumentElement, tNode);
         var _listChildNodes = TreeView1.Nodes[0].ChildNodes;
         TreeView1.Nodes.Clear();
         for (int i = 0; i < _listChildNodes.Count; i++)
         {
            TreeView1.Nodes.Add(_listChildNodes[i]);
         }

         TreeView1.ExpandAll();
      }

      protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
      {
      }

      protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
      {
      }

      protected void TreeView1_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
      {
      }
   }
}
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

            Db.Security.Users(Finsa.Caravan.Common.Configuration.Instance.ApplicationName);
         }
      }

      private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
      {
         XmlNode xNode;
         TreeNode tNode;
         XmlNodeList nodeList;
         int i;

         // Loop through the XML nodes until the leaf is reached.
         // Add the nodes to the TreeView during the looping process.
         if (inXmlNode.HasChildNodes)
         {
            nodeList = inXmlNode.ChildNodes;
            for (i = 0; i <= nodeList.Count - 1; i++)
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
         //b.usr_id, b.usr_login as Login, b.usr_name as Name, b.usr_surname as Surname
        	DataTable _tableLeft = new DataTable();
	      _tableLeft.Columns.Add("Id", typeof(int));
	      _tableLeft.Columns.Add("Login", typeof(string));
	      _tableLeft.Columns.Add("Name", typeof(string));
	      _tableLeft.Columns.Add("Surname", typeof(string));
         
         DataTable _tableRight = new DataTable();
	      _tableRight.Columns.Add("Id", typeof(int));
	      _tableRight.Columns.Add("Login", typeof(string));
	      _tableRight.Columns.Add("Name", typeof(string));
	      _tableRight.Columns.Add("Surname", typeof(string));

         flexMultipleSelectUsers.SetLeftDataSource(_tableLeft);
         flexMultipleSelectUsers.SetRightDataSource(_tableRight);
         flexMultipleSelectUsers.LeftPanelTitle = "Available Users";
         flexMultipleSelectUsers.RightPanelTitle = "Chosen Users";

         //Groups
         DataTable _tableLeftGroups = new DataTable();
         _tableLeftGroups.Columns.Add("Id", typeof(int));
         _tableLeftGroups.Columns.Add("Login", typeof(string));
         _tableLeftGroups.Columns.Add("Name", typeof(string));
         _tableLeftGroups.Columns.Add("Surname", typeof(string));

         DataTable _tableRightGroups = new DataTable();
         _tableRightGroups.Columns.Add("Id", typeof(int));
         _tableRightGroups.Columns.Add("Login", typeof(string));
         _tableRightGroups.Columns.Add("Name", typeof(string));
         _tableRightGroups.Columns.Add("Surname", typeof(string));

         flexMultiSelectGroups.SetLeftDataSource(_tableLeftGroups);
         flexMultiSelectGroups.SetRightDataSource(_tableRightGroups);
         flexMultiSelectGroups.LeftPanelTitle = "Available Groups";
         flexMultiSelectGroups.RightPanelTitle = "Chosen Groups";
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
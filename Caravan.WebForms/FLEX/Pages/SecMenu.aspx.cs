using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Finsa.Caravan.DataAccess;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Finsa.Caravan.Extensions;
using FLEX.Web.UserControls.Ajax;
using Finsa.Caravan.DataModel.Security;
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
                  inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes.GetNamedItem("Caption").Value, xNode.Attributes.GetNamedItem("ID").Value));
                  tNode = inTreeNode.ChildNodes[inTreeNode.ChildNodes.Count-1];
                  AddNode(xNode, tNode);
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
         

         // Legge il file XML
         docXML.Load(Server.MapPath(WebForms.Configuration.Instance.MenuBarXmlPath));

         TreeView1.Nodes.Clear();
         TreeView1.Nodes.Add(new TreeNode(docXML.DocumentElement.Name));
         TreeNode tNode = new TreeNode();
         tNode = TreeView1.Nodes[0];

         AddNode(docXML.DocumentElement, tNode);
         
         TreeNode[] _listChildNodes = new TreeNode[TreeView1.Nodes[0].ChildNodes.Count];
         TreeView1.Nodes[0].ChildNodes.CopyTo(_listChildNodes, 0);
         TreeView1.Nodes.Clear();


         for (int i = 0; i < _listChildNodes.Length; i++)
         {
            TreeView1.Nodes.Add(_listChildNodes[i]);
         }

         TreeView1.ExpandAll();
      }

      protected void TreeView1_SelectedNodeChanged(object sender, EventArgs args)
      {
         var entries = Db.Security.EntriesForObject(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, "menu", TreeView1.SelectedValue);
         var blockedUsers = entries.Where(e => e.User != null).Select(e => e.User).ToList();
         var blockedGroups = entries.Where(e => e.Group != null).Select(e => e.Group).ToList();
         var allowedUsers = Db.Security.Users(Finsa.Caravan.Common.Configuration.Instance.ApplicationName).Except(blockedUsers).ToList();
         var allowedGroups = Db.Security.Groups(Finsa.Caravan.Common.Configuration.Instance.ApplicationName).Except(blockedGroups).ToList();

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
        
         foreach (var item in allowedUsers)
         {
            _tableLeft.Rows.Add(item.Id, item.Login, item.FirstName, item.LastName);
         }
        
         foreach (var item in blockedUsers)
         {
            _tableRight.Rows.Add(item.Id, item.Login, item.FirstName, item.LastName);
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

         foreach (var item in allowedGroups)
         {
            _tableLeftGroups.Rows.Add(item.Id, item.Name);
         }

         foreach (var item in blockedGroups)
         {
            _tableRightGroups.Rows.Add(item.Id, item.Name);
         }

         flexMultiSelectGroups.SetLeftDataSource(_tableLeftGroups);
         flexMultiSelectGroups.SetRightDataSource(_tableRightGroups);
         flexMultiSelectGroups.LeftPanelTitle = "Available Groups";
         flexMultiSelectGroups.RightPanelTitle = "Chosen Groups";
      }

      protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
      {
      }

      protected void TreeView1_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
      {
      }

      protected void Unnamed_Click(object sender, EventArgs e)
      {

      }

      protected void btnSave_Click(object sender, EventArgs e)
      {
         //Users

         var secContext = new SecContext { Name = "menu", Description = "Menu Entries" };
         var secObject = new SecObject { Name = TreeView1.SelectedValue, Description = TreeView1.SelectedNode.Text, Type= "menu" };

         foreach (DataRow oDrR in flexMultipleSelectUsers.RightDataTable.Rows)
         {
            if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
            {
               Db.Security.AddEntry(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, secContext, secObject, oDrR["Login"].ToString(), null);
            }
         }

         foreach (DataRow oDrL in flexMultipleSelectUsers.LeftDataTable.Rows)
         {
            if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
            {
               Db.Security.RemoveEntry(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, secContext.Name, secObject.Name, oDrL["Login"].ToString(), null);
            }
         }
        
         //Groups
         foreach (DataRow oDrR in flexMultiSelectGroups.RightDataTable.Rows)
         {
            if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
            {
               Db.Security.AddEntry(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, secContext, secObject, null, oDrR["Name"].ToString());
            }
         }

         foreach (DataRow oDrR in flexMultiSelectGroups.LeftDataTable.Rows)
         {
            if (oDrR[MultiSelect.FlagCrud].ToString() == "R")
            {
               Db.Security.RemoveEntry(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, secContext.Name, secObject.Name, null, oDrR["Name"].ToString());
            }
         }

      }
   }
}
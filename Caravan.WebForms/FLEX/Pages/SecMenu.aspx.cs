using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebForms.Properties;
using Finsa.CodeServices.Common.Collections;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
    public partial class SecMenu : PageBase
    {
        private readonly Dictionary<string, string> _filesPath = new Dictionary<string, string>();
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
                        if (xNode.Attributes.GetNamedItem("Caption").Value != "Separator")
                        {
                            inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes.GetNamedItem("Caption").Value, xNode.Attributes.GetNamedItem("ID").Value));
                            tNode = inTreeNode.ChildNodes[inTreeNode.ChildNodes.Count - 1];
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
            docXML.Load(Server.MapPath(Settings.Default.MenuBarXmlPath));

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
            var entries = CaravanDataSource.Security.GetEntriesForObject(CommonConfiguration.Instance.AppName, "menu", TreeView1.SelectedValue);
            
            var usersInEntries = entries.Where(e => e.UserLogin != null).Select(e => e.UserLogin).ToHashSet();
            var groupsInEntries = entries.Where(e => e.GroupName != null).Select(e => e.GroupName).ToHashSet();

            var allUsers = CaravanDataSource.Security.GetUsers(CommonConfiguration.Instance.AppName);
            var allGroups = CaravanDataSource.Security.GetGroups(CommonConfiguration.Instance.AppName);

            var allowedUsers = allUsers.Where(u => !usersInEntries.Contains(u.Login));
            var allowedGroups = allGroups.Where(g => !groupsInEntries.Contains(g.Name));
            var blockedUsers = allUsers.Where(u => usersInEntries.Contains(u.Login));
            var blockedGroups = allGroups.Where(g => groupsInEntries.Contains(g.Name));

            //Users
            var leftTable = new DataTable();
            leftTable.Columns.Add("Login", typeof(string));
            leftTable.Columns.Add("FirstName", typeof(string));
            leftTable.Columns.Add("LastName", typeof(string));

            var rightTable = new DataTable();
            rightTable.Columns.Add("Login", typeof(string));
            rightTable.Columns.Add("FirstName", typeof(string));
            rightTable.Columns.Add("LastName", typeof(string));

            foreach (var item in allowedUsers)
            {
                leftTable.Rows.Add(item.Login, item.FirstName, item.LastName);
            }

            foreach (var item in blockedUsers)
            {
                rightTable.Rows.Add(item.Login, item.FirstName, item.LastName);
            }

            flexMultipleSelectUsers.SetLeftDataSource(leftTable);
            flexMultipleSelectUsers.SetRightDataSource(rightTable);
            flexMultipleSelectUsers.LeftPanelTitle = "Available Users";
            flexMultipleSelectUsers.RightPanelTitle = "Chosen Users";

            //Groups
            DataTable _tableLeftGroups = new DataTable();
            _tableLeftGroups.Columns.Add("Name", typeof(string));
            _tableLeftGroups.Columns.Add("Description", typeof(string));

            DataTable _tableRightGroups = new DataTable();
            _tableRightGroups.Columns.Add("Name", typeof(string));
            _tableRightGroups.Columns.Add("Description", typeof(string));

            foreach (var item in allowedGroups)
            {
                _tableLeftGroups.Rows.Add(item.Name, item.Description);
            }

            foreach (var item in blockedGroups)
            {
                _tableRightGroups.Rows.Add(item.Name, item.Description);
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
            var secObject = new SecObject { Name = TreeView1.SelectedValue, Description = TreeView1.SelectedNode.Text, Type = "menu" };

            foreach (DataRow oDrR in flexMultipleSelectUsers.RightDataTable.Rows)
            {
                if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
                {
                    CaravanDataSource.Security.AddEntry(CommonConfiguration.Instance.AppName, secContext, secObject, oDrR["Login"].ToString(), null);
                }
            }

            foreach (DataRow oDrL in flexMultipleSelectUsers.LeftDataTable.Rows)
            {
                if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
                {
                    CaravanDataSource.Security.RemoveEntry(CommonConfiguration.Instance.AppName, secContext.Name, secObject.Name, oDrL["Login"].ToString(), null);
                }
            }

            //Groups
            foreach (DataRow oDrR in flexMultiSelectGroups.RightDataTable.Rows)
            {
                if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
                {
                    CaravanDataSource.Security.AddEntry(CommonConfiguration.Instance.AppName, secContext, secObject, null, oDrR["Name"].ToString());
                }
            }

            foreach (DataRow oDrR in flexMultiSelectGroups.LeftDataTable.Rows)
            {
                if (oDrR[MultiSelect.FlagCrud].ToString() == "R")
                {
                    CaravanDataSource.Security.RemoveEntry(CommonConfiguration.Instance.AppName, secContext.Name, secObject.Name, null, oDrR["Name"].ToString());
                }
            }
        }
    }
}
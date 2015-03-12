<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataManagement.aspx.cs" Inherits="FLEX.TestWebsite.DataManagement" MasterPageFile="FLEX/MasterPages/Base.Master" %>
<%@ MasterType VirtualPath="FLEX/MasterPages/Base.Master"%>
<%@ Register TagPrefix="flex" TagName="AutoSuggest" Src="~/FLEX/UserControls/Ajax/AutoSuggest.ascx" %>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
   <title>Data Management</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="mainContent" runat="server">
   <asp:Button runat="server" ID="ClearDatabase" OnClick="ClearDatabase_Click" Text="Clear Database"/>
   <asp:Button runat="server" ID="ResetDatabase" OnClick="ResetDatabase_Click" Text="Reset Database"/>
   <flex:AutoSuggest ID="autosuggestEmail" XmlLookup="Candidate" LookupBy="Cand_Email" MenuWidth="250" MenuHeight="300" runat="server" />
  
   <asp:UpdatePanel runat="server">
      <ContentTemplate>
         <asp:Panel runat="server" ID="TestPanel">
            <asp:Label runat="server" ID="TestLabel"></asp:Label>
         </asp:Panel>
         <asp:Panel ID="Panel1" runat="server">
            <flex:flxDataGrid ID="fdtgGender" runat="server" SelectColumn="False" 
                              GridFixWidth="600" AllowSorting="True" GridFixHeight="200" AutoGenerateColumns="False" GridLines="None" AllowPaging="True" Width="100%"
                              PageSize="15" CssClass="DGStyle" PagerType="2" OnDataBinding="fdtgGender_DataBinding">
               <PagerStyle Visible="False" CssClass="DGPager" Mode="NumericPages"></PagerStyle>
               <AlternatingItemStyle CssClass="DGAlternateItem"></AlternatingItemStyle>
               <FooterStyle CssClass="DGFooter"></FooterStyle>
               <ItemStyle CssClass="DGItem" wrap="false"></ItemStyle>
               <HeaderStyle CssClass="DGHeader"></HeaderStyle>
               <Columns>
                  <asp:BoundColumn DataField="Gend_Id" SortExpression="Gend_Id" HeaderText="ID">
                     <HeaderStyle Wrap="False" Width="30px" HorizontalAlign="Left"></HeaderStyle>
                     <ItemStyle Wrap="False" Width="30px" HorizontalAlign="Left"></ItemStyle>
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="Gend_Description" SortExpression="Gend_Description" HeaderText="Description">
                     <HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                     <ItemStyle Wrap="False" Width="100px" HorizontalAlign="Left"></ItemStyle>
                  </asp:BoundColumn>
               </Columns>            
            </flex:flxDataGrid>
         </asp:Panel>
         <asp:Panel ID="Panel2" runat="server">
            <flex:flxDataGrid ID="fdtgSchools" runat="server" SelectColumn="False" 
                              GridFixWidth="600" AllowSorting="True" GridFixHeight="200" AutoGenerateColumns="False" GridLines="None" AllowPaging="True" Width="100%"
                              PageSize="15" CssClass="DGStyle" PagerType="2" OnDataBinding="fdtgSchools_DataBinding">
               <PagerStyle Visible="False" CssClass="DGPager" Mode="NumericPages"></PagerStyle>
               <AlternatingItemStyle CssClass="DGAlternateItem"></AlternatingItemStyle>
               <FooterStyle CssClass="DGFooter"></FooterStyle>
               <ItemStyle CssClass="DGItem" wrap="false"></ItemStyle>
               <HeaderStyle CssClass="DGHeader"></HeaderStyle>
               <Columns>
                  <asp:BoundColumn DataField="Schl_Id" SortExpression="Schl_Id" HeaderText="ID">
                     <HeaderStyle Wrap="False" Width="30px" HorizontalAlign="Left"></HeaderStyle>
                     <ItemStyle Wrap="False" Width="30px" HorizontalAlign="Left"></ItemStyle>
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="Schl_Description" SortExpression="Schl_Description" HeaderText="Description">
                     <HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                     <ItemStyle Wrap="False" Width="100px" HorizontalAlign="Left"></ItemStyle>
                  </asp:BoundColumn>
               </Columns>            
            </flex:flxDataGrid>
         </asp:Panel>
         <asp:Panel ID="Panel3" runat="server">
            <flex:flxDataGrid ID="fdtgCandidates" runat="server" SelectColumn="False" 
                              GridFixWidth="600" AllowSorting="True" GridFixHeight="200" AutoGenerateColumns="False" GridLines="None" AllowPaging="True" Width="100%"
                              PageSize="15" CssClass="DGStyle" PagerType="2" OnDataBinding="fdtgCandidates_DataBinding">
               <PagerStyle Visible="False" CssClass="DGPager" Mode="NumericPages"></PagerStyle>
               <AlternatingItemStyle CssClass="DGAlternateItem"></AlternatingItemStyle>
               <FooterStyle CssClass="DGFooter"></FooterStyle>
               <ItemStyle CssClass="DGItem" wrap="false"></ItemStyle>
               <HeaderStyle CssClass="DGHeader"></HeaderStyle>
               <Columns>
                  <asp:BoundColumn DataField="Cand_Id" SortExpression="Schl_Id" HeaderText="ID">
                     <HeaderStyle Wrap="False" Width="30px" HorizontalAlign="Left"></HeaderStyle>
                     <ItemStyle Wrap="False" Width="30px" HorizontalAlign="Left"></ItemStyle>
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="Cand_Name" SortExpression="Cand_Name" HeaderText="Name">
                     <HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                     <ItemStyle Wrap="False" Width="100px" HorizontalAlign="Left"></ItemStyle>
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="Cand_Surname" SortExpression="Cand_Surname" HeaderText="Surname">
                     <HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                     <ItemStyle Wrap="False" Width="100px" HorizontalAlign="Left"></ItemStyle>
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="Cand_Email" SortExpression="Cand_Email" HeaderText="Email">
                     <HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Left"></HeaderStyle>
                     <ItemStyle Wrap="False" Width="100px" HorizontalAlign="Left"></ItemStyle>
                  </asp:BoundColumn>
               </Columns>            
            </flex:flxDataGrid>
         </asp:Panel>
      </ContentTemplate>
   </asp:UpdatePanel>
        
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecGroupDetails.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecGroupDetails" MasterPageFile="~/FLEX/MasterPages/Popup.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/Popup.Master"%>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Group Details</title>
</asp:Content>

<asp:Content ID="aspMainContent" ContentPlaceHolderID="mainContent" runat="server">
   <asp:TextBox runat="server" ID="txtGrpId" Enabled="False" ReadOnly="True" />
   <asp:TextBox runat="server" ID="txtGrpName" />
   <asp:TextBox runat="server" ID="txtGrpDescr" />
   <asp:CheckBox runat="server" ID="chkAdmin" />

</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="buttonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   
</asp:Content>
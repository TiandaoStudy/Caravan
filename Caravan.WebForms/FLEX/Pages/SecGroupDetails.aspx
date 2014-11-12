<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecGroupDetails.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecGroupDetails" MasterPageFile="~/FLEX/MasterPages/Popup.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/Popup.Master"%>
<%@ Register TagPrefix="crvn" TagName="HiddenTrigger" Src="~/FLEX/UserControls/Ajax/HiddenTrigger.ascx" %>
<%@ Register TagPrefix="crvn" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Group Details</title>
   
   <script type="text/javascript">
      function saveGroup() {
         triggerAsyncPostBack("<%= hiddenSave.Trigger.ClientID %>");
      }
   </script>
</asp:Content>

<asp:Content ID="aspMainContent" ContentPlaceHolderID="mainContent" runat="server">
   <asp:TextBox runat="server" ID="txtGrpId" Enabled="False" ReadOnly="True" />
   <asp:TextBox runat="server" ID="txtGrpName" />
   <asp:TextBox runat="server" ID="txtGrpDescr" />
   <asp:CheckBox runat="server" ID="chkAdmin" />

</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="buttonsContent" runat="server">
   <crvn:ImageButton runat="server" ID="btxExit" ButtonClass="btn btn-primary" ButtonText="Exit" IconClass="glyphicon glyphicon-remove" OnClientClick="closeWindow(); return false;" />
   <crvn:ImageButton runat="server" ID="btxSave" ButtonClass="btn btn-success" ButtonText="Save & Exit" IconClass="glyphicon glyphicon-floppy-disk" OnClientClick="saveGroup(); return false;" />
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   <crvn:HiddenTrigger runat="server" ID="hiddenSave" OnTriggered="hiddenSave_OnTriggered" />
</asp:Content>
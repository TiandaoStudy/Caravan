<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecGroupDetails.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecGroupDetails" MasterPageFile="~/FLEX/MasterPages/Popup.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/Popup.Master"%>
<%@ Register TagPrefix="crvn" TagName="HiddenTrigger" Src="~/FLEX/UserControls/Ajax/HiddenTrigger.ascx" %>
<%@ Register TagPrefix="crvn" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>
<%@ Register TagPrefix="crvn" TagName="MultiSelect" Src="~/FLEX/UserControls/Ajax/MultiSelect.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Group Details</title>
   
   <script type="text/javascript">
      function saveGroup() {
         triggerAsyncPostBack("<%= hiddenSave.Trigger.ClientID %>");
      }
   </script>
</asp:Content>

<asp:Content ID="aspMainContent" ContentPlaceHolderID="mainContent" runat="server">

  


  <div class="col-xs-12">
      <div class="row form-group">
         <label class="control-label col-xs-1 text-right">Id</label>
         <div class="col-xs-1">
              <asp:TextBox runat="server" ID="txtGrpId" Enabled="False" ReadOnly="True" />
         </div>

         <label class="control-label col-xs-1 text-right">Name</label>
         <div class="col-xs-2">
             <asp:TextBox runat="server" ID="txtGrpName" />
         </div>

         <label class="control-label col-xs-2 text-right">Description</label>
         <div class="col-xs-2">
            <asp:TextBox runat="server" ID="txtGrpDescr" />
         </div>


         <label id="Label1" class="control-label col-xs-2 text-right" runat="server">Is Admin</label>
         <div class="col-xs-1">
              <asp:CheckBox runat="server" ID="chkAdmin" />
         </div>
      </div>

       <div class="row form-group">
         <label class="control-label col-xs-1 text-right">Notes</label>
         <div class="col-xs-11">
            <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine"/>
         </div>
       </div>

       <div class="row form-group">
            <crvn:MultiSelect runat="server" ID="crvnMultiSelectUsersGroups"></crvn:MultiSelect>
      </div>
   </div>

     



</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="buttonsContent" runat="server">
   <crvn:ImageButton runat="server" ID="btxExit" ButtonClass="btn btn-primary" ButtonText="Exit" IconClass="glyphicon glyphicon-remove" OnClientClick="closeWindow(); return false;" />
   <crvn:ImageButton runat="server" ID="btxSave" ButtonClass="btn btn-success" ButtonText="Save & Exit" IconClass="glyphicon glyphicon-floppy-disk" OnClientClick="saveGroup(); return false;" />
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   <crvn:HiddenTrigger runat="server" ID="hiddenSave" OnTriggered="hiddenSave_OnTriggered" />
</asp:Content>
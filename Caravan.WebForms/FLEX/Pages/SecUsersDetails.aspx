<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecUsersDetails.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecUsersDetails" MasterPageFile="~/FLEX/MasterPages/Popup.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/Popup.Master"%>
<%@ Register TagPrefix="crvn" TagName="HiddenTrigger" Src="~/FLEX/UserControls/Ajax/HiddenTrigger.ascx" %>
<%@ Register TagPrefix="crvn" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>
<%@ Register TagPrefix="crvn" TagName="MultiSelect" Src="~/FLEX/UserControls/Ajax/MultiSelect.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>User Details</title>
   
   <script type="text/javascript">
      function saveGroup() {
         triggerAsyncPostBack("<%= hiddenSave.Trigger.ClientID %>");
      }
   </script>
</asp:Content>

<asp:Content ID="aspMainContent" ContentPlaceHolderID="mainContent" runat="server">

  


  <div class="col-xs-12">
      <div class="row form-group">
             <label class="control-label col-xs-4 text-right">Id</label>
             <div class="col-xs-8">
                  <asp:TextBox runat="server" ID="txtUserId" Enabled="False" ReadOnly="True" />
             </div>
      </div>

      <div class="row form-group">
             <label class="control-label col-xs-4 text-right">FirstName</label>
             <div class="col-xs-8">
                 <asp:TextBox runat="server" ID="txtFirstName" />
             </div>
      </div>

       <div class="row form-group">
             <label class="control-label col-xs-4 text-right">LastName</label>
             <div class="col-xs-8">
                <asp:TextBox runat="server" ID="txtLastName" />
             </div>
        </div>

        <div class="row form-group">
              <label class="control-label col-xs-4 text-right">Email</label>
             <div class="col-xs-8">
                <asp:TextBox runat="server" ID="txtEmail" />
             </div>
         </div>


        <div class="row form-group">
             <label id="Label1" class="control-label col-xs-4 text-right" runat="server">Is Active</label>
             <div class="col-xs-8">
                  <asp:CheckBox runat="server" ID="chkIsActive" />
             </div>
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
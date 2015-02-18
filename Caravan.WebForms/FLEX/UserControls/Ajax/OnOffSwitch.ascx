<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnOffSwitch.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.OnOffSwitch" %>
<%@ Import Namespace="PommaLabs.Extensions" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<script type="text/javascript">
   function switchOnOff_<%= ClientID %>(event) {
      var btnON = $("#<%= btnON.ClientID %>");
      var btnOFF = $("#<%= btnOFF.ClientID %>");

      var checked = !btnON.hasClass("switch-active");     

      btnON.attr("class", checked ? "<%= ActiveOnClass %>" : "<%= InactiveOnClass %>");
      btnOFF.attr("class", checked ? "<%= InactiveOffClass %>" : "<%= ActiveOffClass %>");

      var onValue = <%= OnValue.ToJavaScriptString() %>;
      var offValue = <%= OffValue.ToJavaScriptString() %>;
      // This may trigger the real postback.
      $("#<%= txtSwitched.ClientID %>").val(checked ? onValue : offValue);
   }

   function styleOnOff_<%= ClientID %>() {
      
   }
</script>

<div class="btn-group">
   <ajax:UpdatePanel ID="updPanel" runat="server">
      <ContentTemplate>
         <asp:TextBox runat="server" ID="txtSwitched" CssClass="hidden" OnTextChanged="txtSwitched_OnTextChanged" />
         <div class="btn-group">
            <asp:Button ID="btnON" runat="server" Text="ON" CausesValidation="False" />
            <asp:Button ID="btnOFF" runat="server" Text="OFF" CausesValidation="False" />
         </div>
      </ContentTemplate>
   </ajax:UpdatePanel> 
</div>
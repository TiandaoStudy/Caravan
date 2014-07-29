<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnOffSwitch.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.OnOffSwitch" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<script type="text/javascript">
   function switchOnOff_<%= ClientID %>(event) {
      var btnON = $("#<%= btnON.ClientID %>");
      var btnOFF = $("#<%= btnOFF.ClientID %>");

      var checked = !btnON.hasClass("switch-active");
      $("#<%= hidSwitched.ClientID %>").val(checked ? 1 : 0);

      var activeClass = "<%= ActiveClass %>";
      var inactiveClass = "<%= InactiveClass %>";
      btnON.attr("class", checked ? activeClass : inactiveClass);
      btnOFF.attr("class", checked ? inactiveClass : activeClass);

      if (<%= (!DoPostBack).ToString().ToLower() %>) event.preventDefault();
   }

   function styleOnOff_<%= ClientID %>() {
      
   }
</script>

<div class="btn-group">
   <ajax:UpdatePanel ID="updPanel" runat="server">
      <ContentTemplate>
         <asp:HiddenField runat="server" ID="hidSwitched" />
         <div class="btn-group">
            <asp:Button ID="btnON" runat="server" Text="ON" OnClick="btnON_OFF_OnClick" CausesValidation="False" />
            <asp:Button ID="btnOFF" runat="server" Text="OFF" OnClick="btnON_OFF_OnClick" CausesValidation="False" />
         </div>
      </ContentTemplate>
   </ajax:UpdatePanel> 
</div>
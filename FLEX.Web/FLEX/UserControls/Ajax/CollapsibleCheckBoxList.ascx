<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollapsibleCheckBoxList.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.CollapsibleCheckBoxList" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<script lang="javascript">
   function P(toggleId, iconId) {
      // $("#" + toggleId).collapse('toggle');
      var ic = $("#" + iconId);
      if (ic.attr('class') == 'glyphicon glyphicon-chevron-down') {
         ic.removeClass('glyphicon-chevron-down');
         ic.addClass('glyphicon-chevron-up');
      } else if (ic.attr('class') == 'glyphicon glyphicon-chevron-up') {
         ic.removeClass('glyphicon-chevron-up');
         ic.addClass('glyphicon-chevron-down');
      }
   }
</script>

<div id="divData" runat="server" class="input-group" style="margin-bottom: 0">
   <ajax:UpdatePanel ID="updPanel1" runat="server">
      <ContentTemplate>
         <asp:CheckBoxList AutoPostBack="true" ID="chklVisible" runat="server" />               
      </ContentTemplate>
   </ajax:UpdatePanel>
</div>
 
<!-- Collapsible Element HTML -->
<div id="divToggle" class="collapse" runat="server" style="margin-top: 0">
   <div id="divG" runat="server" class="input-group">
  
      <ajax:UpdatePanel ID="updPanel2" runat="server">
         <ContentTemplate>
            <asp:CheckBoxList AutoPostBack="true" ID="chklHidden" runat="server" />                    
         </ContentTemplate>
      </ajax:UpdatePanel>        
   </div>
</div>
   
<!-- Trigger HTML -->
<div id="divAltre" class="collapse-group" runat="server" >
   <a id="altro" class="textbox" runat="server" data-toggle="collapse">Others <i id="ic" class="glyphicon glyphicon-chevron-down" runat="server"></i></a>
</div>
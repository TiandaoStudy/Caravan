<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.DatePicker" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<ajax:UpdatePanel ID="datepickerPanel" runat="server">
   <ContentTemplate>
      <div class="input-group date">
         <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
         <span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
      </div>

      <script type="text/javascript">
         if (<%= Enabled.ToString().ToLower() %>) {
            // Control is ENABLED
            $('#<%= datepickerPanel.ClientID %> .input-group.date').datepicker({
               format: "<%= DateFormat %>",
               weekStart: 1,
               //startDate: "<%= StartDate %>",
               //endDate: "<%= EndDate %>",
               startView: <%= Convert.ToByte(StartView) %>,
               minViewMode: <%= Convert.ToByte(MinView) %>,
               todayBtn: "linked",
               autoclose: true,
               todayHighlight: true
            });
         }
      </script>
   </ContentTemplate>
</ajax:UpdatePanel>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NumericSpinner.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.NumericSpinner" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<ajax:UpdatePanel runat="server" ID="updPanel">
   <ContentTemplate>
      <div class="input-group">
         <asp:TextBox ID="txtNumber" runat="server" />
         <div class="input-group-btn">
            <asp:LinkButton runat="server" ID="btnApply" CssClass="btn btn-default numeric-spinner-apply" Text="Apply" />
         </div>
      </div>

      <script type="text/javascript">
         $("#<%= txtNumber.ClientID %>").TouchSpin({
            initval: "", // Applied when no explicit value is set on the input with the value attribute. Empty string means that the value remains empty on initialization.
            min: <%= MinValue %>, // Minimum value.
            max: <%= MaxValue %>, // Maximum value.
            decimals: <%= DecimalCount %>, // Number of decimal points.
            step: <%= Step %>, // Incremental-decremental step on up-down change.
            stepinterval: 50,
            booster: true, // If enabled, the the spinner is continually becoming faster as holding the button.
            boostat: 10, // Boost at every nth step.
            maxboostedstep: <%= MaxValue %>, // Maximum step when boosted.
            mousewheel: true // Enables the mouse wheel to change the value of the input.
         });
      </script>
   </ContentTemplate>
</ajax:UpdatePanel>
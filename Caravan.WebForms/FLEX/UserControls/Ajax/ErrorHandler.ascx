<%@ Control Language="cs" AutoEventWireup="true" Codebehind="ErrorHandler.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.ErrorHandler" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<script type="text/javascript">
   var _eventHandlerCode = <%= (byte) FLEX.Web.UserControls.Ajax.ErrorLocation.EventHandler %>;
   var _pageEventCode = <%= (byte) FLEX.Web.UserControls.Ajax.ErrorLocation.PageEvent %>;
   var _modalWindowCode = <%= (byte) FLEX.Web.UserControls.Ajax.ErrorLocation.ModalWindow %>;
</script>

<ajax:UpdatePanel ID="errorPanel" runat="server">
   <ContentTemplate>
      <asp:Label ID="txtSystemErrorCode" CssClass="hidden" runat="server" EnableViewState="False"></asp:Label>
      
      <script type="text/javascript">
         var _errorHandler = $("#<%= txtSystemErrorCode.ClientID %>");

         if (_errorHandler) {
            var errorCode = parseInt(_errorHandler.text());
            if (!isNaN(errorCode)) {
               displaySystemError();
               // Now, we have to handle the error code.
               if (errorCode == (_pageEventCode | _modalWindowCode)) {
                  // An exception has been thrown during modal loading;
                  // therefore, we need to close the modal window.
                  closeWindow();
               } else if (errorCode == _pageEventCode) {
                  // An exception has been thrown during page loading;
                  // therefore, we need to get back to the previous page.
                  window.history.back();
               } else if (errorCode == _eventHandlerCode || errorCode == _modalWindowCode) {
                  // If it has been thrown from a common event handler or from a modal event, 
                  // then we have nothing to do.
               } else if (errorCode == (_eventHandlerCode | _modalWindowCode)) {
                  // If it has been thrown from a common event handler inside a modal, 
                  // then we have nothing to do.
               } 
            }
         }
      </script>
   </ContentTemplate>
</ajax:UpdatePanel>
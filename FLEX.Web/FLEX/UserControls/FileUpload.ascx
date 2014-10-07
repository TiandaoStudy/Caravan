<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.ascx.cs" Inherits="FLEX.Web.UserControls.FileUpload" %>

<input type="file" id="inputFileUpload" runat="server" class="btn btn-primary" />

<script type="text/javascript">
   $(document).ready(function() {
      $(<%= JQueryID(inputFileUpload) %>).bootstrapFileInput();
   });
</script>
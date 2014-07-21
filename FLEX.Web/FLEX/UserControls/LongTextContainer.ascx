<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LongTextContainer.ascx.cs" Inherits="FLEX.Web.UserControls.LongTextContainer" %>

<label id="shortenedText" class="long-text-ctn-label" runat="server"><%= ShortenedText %></label>

<script type="text/javascript">
   $('#<%= shortenedText.ClientID %>').popover({
      animation: true,
      placement: "auto",
      html: true,
      trigger: "hover",
      content: "<%= Text %>".escapeSpecialChars(),
      title: "<%= ContainerTitle %>".escapeSpecialChars()
   });
</script>
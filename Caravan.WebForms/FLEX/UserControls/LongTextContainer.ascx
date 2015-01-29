<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LongTextContainer.ascx.cs" Inherits="FLEX.Web.UserControls.LongTextContainer" %>
<%@ Import Namespace="PommaLabs.Extensions" %>

<a href="#" onclick="window.prompt('Copy to clipboard: Ctrl+C, Enter', <%= Text.HtmlEncode().ToJavaScriptString(true) %>); return false;"><i class="fa fa-clipboard"></i></a>
<label id="shortenedText" class="long-text-ctn-label" runat="server"><%= ShortenedText.HtmlEncode() %></label>

<style type="text/css">
    .popover {
        max-width: 50%;
        width: auto;
    }
</style>

<script type="text/javascript">
    $('#<%= shortenedText.ClientID %>').popover({
        animation: true,
        container: "body",
        placement: "auto",
        html: true,
        trigger: "hover",
        // Text should not have normal line breaks (it is HTML).
        content: <%= Text.HtmlEncode().Replace(Environment.NewLine, "<br/>").ToJavaScriptString() %>,
        title: <%= ContainerTitle.ToJavaScriptString() %>
    }).one('click', function() {
        $('#<%= shortenedText.ClientID %>').popover('destroy').popover({
        animation: true,
        container: "body",
        placement: "auto",
        html: true,
        trigger: "click",
            // Text should not have normal line breaks (it is HTML).
        content: <%= Text.HtmlEncode().Replace(Environment.NewLine, "<br/>").ToJavaScriptString() %>,
        title: <%= ContainerTitle.ToJavaScriptString() %>
        }).popover('show');
    });
</script>
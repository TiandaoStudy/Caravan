﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LongTextContainer.ascx.cs" Inherits="FLEX.Web.UserControls.LongTextContainer" %>
<%@ Import Namespace="PommaLabs.Extensions" %>

<label id="shortenedText" class="long-text-ctn-label" runat="server"><%= Server.HtmlEncode(ShortenedText) %></label>

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
      content: <%= Server.HtmlEncode(Text).ToJavaScriptString() %>,
      title: <%= ContainerTitle.ToJavaScriptString() %>
      }).one('click', function() {
         $('#<%= shortenedText.ClientID %>').popover('destroy')
         .popover({
            animation: true,
            container: "body",
            placement: "auto",
            html: true,
            trigger: "click",
            content: <%= Text.ToJavaScriptString() %>,
            title: <%= ContainerTitle.ToJavaScriptString() %>
            })
         .popover('show');
   });
</script>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeEditor.aspx.cs" Inherits="FLEX.Web.Pages.CodeEditor" MasterPageFile="~/FLEX/MasterPages/CustomView.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/CustomView.Master"%>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Code Editor</title>  

   <style type="text/css" media="screen">
      #editor {
         /*position: absolute;*/
         top: 0;
         right: 0;
         bottom: 0;
         left: 0;
         height: 400px;
      }
   </style>

   <script src="../Scripts/ace/ace.js" type="text/javascript" charset="utf-8"></script>

   <script>
      function ChangeModeEditor(mode) {
         var editor = ace.edit("editor");
         editor.setTheme("ace/theme/eclipse");
         switch (mode) {
         case 'cs':
            editor.getSession().setMode("ace/mode/csharp");
            break;
         case 'xml':
            editor.getSession().setMode("ace/mode/xml");
            break;
         default:
            editor.getSession().setMode("ace/mode/csharp");
         }
         var textbox = $("#<%= txtContentFiles.ClientID %>").hide();
         editor.getSession().setValue(textbox.val());
      }

      function Save() {
         var editor = ace.edit("editor");
         var text = editor.getSession().getValue();
         $("#<%= txtContentFiles.ClientID %>").val(text);
      }
   </script>

</asp:Content>

<asp:Content ID="aspCustomContent" ContentPlaceHolderID="customContent" runat="server">
   <div class="col-xs-12">
      <ajax:UpdatePanel ID="UpdatePanel3" runat="server">
         <ContentTemplate>
            <div class="panel-body">                    
               <div class="row">
                  <div class="col-xs-4">
                     <asp:TreeView ID="TreeView1" runat="server" 
                                   onselectednodechanged="TreeView1_SelectedNodeChanged" OnTreeNodeExpanded="TreeView1_TreeNodeExpanded" OnTreeNodeCollapsed="TreeView1_TreeNodeCollapsed">
                     </asp:TreeView>
                  </div>
                  <div class="col-xs-8">
                     <div class="row" runat="server" id="divContenFiles">
                        <div id="editor">
                        </div>
                        <asp:TextBox runat="server" ID="txtContentFiles" Visible="false" TextMode="MultiLine" ></asp:TextBox>
                     </div>
                  </div>                
               </div>
            </div>
         </ContentTemplate>
      </ajax:UpdatePanel>
   </div>
</asp:Content>

<asp:Content runat="server" ID="aspRightButtonsContent" ContentPlaceHolderID="rightButtonsContent">
   <!-- Il bottone deve stare in un UpdatePanel, perché appare e scompare a seconda che i file siano selezionati o meno -->
   <ajax:UpdatePanel runat="server">
      <ContentTemplate>
         <asp:LinkButton class="btn btn-success btn-sm" ID="lbtnSave" Visible="false"  runat="server" OnClick="lbtnSave_Click" OnClientClick=" Save(); "><span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;Save</asp:LinkButton>         
      </ContentTemplate>
   </ajax:UpdatePanel>
</asp:Content>
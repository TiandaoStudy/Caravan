<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorHandler.aspx.cs" Inherits="FLEX.Web.Pages.ErrorHandler" MasterPageFile="../MasterPages/Popup.Master"%>
<%@ MasterType VirtualPath="../MasterPages/Popup.Master" %>

<asp:Content runat="server" ID="aspHeadContent" ContentPlaceHolderID="headContent">
   <title>Error</title>
  
   <script type="text/javascript">
      onPopupClose = function(retval) {
         // Vouta, per ora...
      };

      function detailError() {
         if (document.getElementById('btnDetail').value == "Detail >>") {
            document.getElementById('btnDetail').value = "Detail <<";
            document.getElementById('TDError').style.visibility = 'visible';
            $('#<%= txtDetail.ClientID %>').show();
            $('#<%= txtDetail.ClientID %>').css({ height: "200px" });
         } else {
            document.getElementById('btnDetail').value = "Detail >>";
            document.getElementById('TDError').style.visibility = 'hidden';
            $('#<%= txtDetail.ClientID %>').css({ height: "1px" });
            $('#<%= txtDetail.ClientID %>').hide();
         }
      }

      function onCancel() {
         window.close();
      }
   </script>  
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="mainContent">
   <div class="panel panel-danger">
      <div class="panel-heading" id="tblTitolo">       
         <asp:label id="txtTitle" runat="server" CssClass="ErrorTitle"> Applicazione</asp:label>       
      </div>
	   
      <div class="panel-body" id="tblErrore">

         <div class="col-xs-12">	
			
            <div class="row" style="font-family: Verdana, Arial; font-size: 10pt; font-weight: bolder;">
               <div class="col-xs-12">
                  <div class="row form-group">
                     <div class="col-xs-1"><i class="glyphicon glyphicon-remove-sign text-danger fa-3x"></i></div>
                     <div class="col-xs-11"><strong>An error has occurred in the application.</strong></div>
                  </div>
									
                  <div class="row form-group">
                     <div class="col-xs-2" style="text-align: right;"><B>Message&nbsp;:</B>
                     </div>
                     <div class="col-xs-10"><asp:label id="txtMessage" runat="server" ForeColor="Brown" Font-Size="11pt" Font-Bold="True"></asp:label></div>
                  </div>

                  <div class="row form-group">
                     <div class="col-xs-2" style="text-align: right;"><B>Source :</B>
                     </div>
                     <div class="col-xs-10"><asp:label id="txtSource" runat="server" ForeColor="Black"></asp:label></div>
                  </div>

                  <div class="row form-group">
                     <div class="col-xs-2" style="text-align: right;">
                        <input class="btn btn-default" id="btnDetail" onclick=" detailError(); "
                               type="button" value="Detail >>" name="btnDetail">
                     </div>
                     <div class="col-xs-10" id="TDError" style="visibility: hidden"><asp:textbox id="txtDetail" runat="server" ForeColor="Black" BorderStyle="None" ReadOnly="True" TextMode="MultiLine" ></asp:textbox></div>
                  </div>
								
               </div>
            </div>
         </div>
						
      </div> 
					    
   </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="buttonsContent">
   <asp:Button ID="btnEsci"  class="btn btn-primary" Text="Exit" runat="server"  CausesValidation="false" OnClientClick=" onCancel(); return false; "/>
   <asp:Button id="btnPrint" runat="server" class="btn btn-primary" Text="Print" Width="80px"></asp:Button>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CandidateList.aspx.cs" Inherits="FLEX.Sample.WebUI.CandidateList" MasterPageFile="FLEX/MasterPages/Base.Master"%>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/Base.Master"%>
<%@ Register TagPrefix="flex" TagName="AutoSuggest" Src="~/FLEX/UserControls/Ajax/AutoSuggest.ascx" %>
<%@ Register TagPrefix="flex" TagName="CollapsibleCheckBoxList" Src="~/FLEX/UserControls/Ajax/CollapsibleCheckBoxList.ascx" %>
<%@ Register TagPrefix="flex" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>
<%@ Register TagPrefix="flex" TagName="NumericSpinner" Src="~/FLEX/UserControls/Ajax/NumericSpinner.ascx" %>
<%@ Register TagPrefix="flex" TagName="OnOffSwitch" Src="~/FLEX/UserControls/Ajax/OnOffSwitch.ascx" %>
<%@ Register TagPrefix="flex" Namespace="FLEX.WebForms.UserControls" Assembly="Finsa.Caravan.WebForms" %>
<%@ Register TagPrefix="flex" TagName="ExportList" Src="~/FLEX/UserControls/ExportList.ascx" %>
<%@ Register TagPrefix="flex" TagName="LongTextContainer" Src="~/FLEX/UserControls/LongTextContainer.ascx" %>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
   <title>Candidate List</title>
   <style>
        /* show the move cursor as the user moves the mouse over the panel header.*/
      #divShownSearchCriteria .panel-heading {
         cursor: move;
         cursor: pointer;
      }

      .workingArea { height: 500px; }
   </style>
   <script type="text/javascript">

      $(function() {
         var panelList = $('#divShownSearchCriteria');

         panelList.sortable({
            // Only make the .panel-heading child elements support dragging.
            // Omit this to make then entire <li>...</li> draggable.
            handle: '.panel-heading',
            update: function() {
               $('.panel', panelList).each(function(index, elem) {
                  var $listItem = $(elem),
                     newIndex = $listItem.index();

                  // Persist the new indices.
               });
            }
         });

         $("#btnCollapseSearchCriteria").click(function() {
            var divSearchCriteria = $("#divSearchCriteria");
            var divDataGrid = $("#divDataGrid");
            var headSearchCriteria = $("#headSearchCriteria");
            var divShownSearchCriteria = $("#divShownSearchCriteria");
            var divHiddenSearchCriteria = $("#divHiddenSearchCriteria");
            var spanCollapse = $("#spanCollapseSearchCriteria");

            if (divSearchCriteria.hasClass('col-xs-3')) {
               // Collapse Search criteria and expand Data grid
               headSearchCriteria.hide();
               divShownSearchCriteria.hide();
               divSearchCriteria.removeClass("col-xs-3").addClass("col-xs-1");
               divDataGrid.removeClass("col-xs-9").addClass("col-xs-11");
               //$("#ContentRicerca").hide();
               //$("#HeaderRicerca > h1").hide();
               //$("#ButtonRicerca").addClass('styleRicerca');
               spanCollapse.removeClass('glyphicon-arrow-left').addClass('glyphicon-arrow-right');
               divHiddenSearchCriteria.show();
            } else if (divSearchCriteria.hasClass('col-xs-1')) {
               // Expand Search criteria and compress Data grid
               divHiddenSearchCriteria.hide();
               spanCollapse.removeClass('glyphicon-arrow-right').addClass('glyphicon-arrow-left');
               divDataGrid.removeClass("col-xs-11").addClass("col-xs-9");
               divSearchCriteria.removeClass("col-xs-1").addClass("col-xs-3");
               divShownSearchCriteria.show();
               headSearchCriteria.show();
               //$("#ContentRicerca").show();
               //$("#HeaderRicerca > h1").show();
               //$("#ButtonRicerca").removeClass('styleRicerca');
            }
         });
      });

      function R(id) {
         //$("#divToggleR" + id).collapse('toggle');
         if ($("#icr" + id).attr('class') == 'glyphicon glyphicon-chevron-down') {
            $("#icr" + id).removeClass('glyphicon-chevron-down');
            $("#icr" + id).addClass('glyphicon-chevron-up');
         } else if ($("#icr" + id).attr('class') == 'glyphicon glyphicon-chevron-up') {
            $("#icr" + id).removeClass('glyphicon-chevron-up');
            $("#icr" + id).addClass('glyphicon-chevron-down');
         }
      }
   </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="mainContent" runat="server">
   <div class="row">
        
      <!-- Start Search criteria -->
      <div id="divSearchCriteria" class="col-xs-3">
            
         <!-- Start Search criteria panel -->
         <div class="panel panel-primary workingArea">
            <div class="panel-heading">
               <div class="row">
                  <div id="headSearchCriteria">
                     <div class="col-xs-7">      
                        <h3 class="panel-title" style="display: inline">
                           Search Criteria
                        </h3>
                     </div>
                    
                     <div class="col-xs-2">

                            
                     </div>
                  </div>
                  <div class="col-xs-2">
                     <button id="btnCollapseSearchCriteria" type="button" class="btn btn-info">
                        <span id="spanCollapseSearchCriteria" class="glyphicon glyphicon-arrow-left"></span>
                     </button>
                  </div>    
               </div>                  
            </div>
            <div class="panel-body" style="height: 420px; overflow-y: auto;">                   
               <ul id="divShownSearchCriteria" class="panel-group list-unstyled" runat="server">
                  <li class="panel panel-info">
                     <div onclick=" javascript:R(3); ">
                        <a class="text-info" data-toggle="collapse" data-target="#divCandId">
                           <i id="icr3" class="glyphicon glyphicon-chevron-up"></i>&nbsp;Name
                        </a>
                     </div>
                     <div class="collapse in collapse-content">
                        <div class="panel-body">
                           <flex:AutoSuggest ID="autosuggestFullName" DoPostBack="true" XmlLookup="Candidate" LookupBy="Cand_NameSurname" MenuWidth="250" MenuHeight="300" runat="server" />                                    
                        </div>                     
                     </div>                                           
                  </li>
                  <li class="panel panel-info">
                     <div onclick=" javascript:R(2); ">
                        <a class="text-info" data-toggle="collapse" data-target="#divCandEmail">
                           <i id="icr2" class="glyphicon glyphicon-chevron-up"></i>&nbsp;Email
                        </a>
                     </div>
                     <div class="collapse in collapse-content">
                        <div class="panel-body">
                           <flex:AutoSuggest ID="autosuggestEmail" DoPostBack="true" XmlLookup="Candidate" LookupBy="Cand_Email" MenuWidth="250" MenuHeight="300" runat="server" />                                    
                        </div>                     
                     </div>                                           
                  </li>
                  <li class="panel panel-info">
                     <div onclick=" javascript:R(4); ">
                        <a class="text-info" data-toggle="collapse" data-target="#divGendId">
                           <i id="icr4" class="glyphicon glyphicon-chevron-up"></i>&nbsp;Gender
                        </a>
                     </div>
                     <div class="collapse in collapse-content">
                        <div class="panel-body">
                           <flex:CollapsibleCheckBoxList ID="ctrlGendId" runat="server" MaxVisibleItemCount="2" DoPostBack="true" />                                
                        </div>                     
                     </div>                                           
                  </li>
                  <li class="panel panel-info">
                     <div onclick=" javascript:R(1); ">
                        <a class="text-info" data-toggle="collapse" data-target="#divSchlId">
                           <i id="icr1" class="glyphicon glyphicon-chevron-up"></i>&nbsp;School
                        </a>
                     </div>
                     <div class="collapse in collapse-content">
                        <div class="panel-body">
                           <flex:CollapsibleCheckBoxList ID="ctrlSchlId" runat="server" MaxVisibleItemCount="2" DoPostBack="true" />
                           <flex:NumericSpinner runat="server" ID="flexTestSpinner" />                                 
                        </div>                     
                     </div>                                           
                  </li>
               </ul>                                 
            </div>           
         </div>
         <!-- End Search criteria panel -->
                 
      </div>
      <!-- End Search criteria -->

      <!-- Start Data grid -->
      <div id="divDataGrid" class="col-xs-9">
            
         <!-- Start Data grid panel -->
         <div class="panel panel-primary workingArea">
            <div class="panel-heading">
               <h3 class="panel-title">Results</h3>
            </div>
            <div class="panel-body">
               <asp:UpdatePanel ID="updDataGrid" runat="server">                     
                  <ContentTemplate>            
                    <flex:DataGrid ID="fdtgCandidates" runat="server" DefaultSortExpression="Cand_Name" DefaultSortDirection="Ascending" PageSize="3" OnDataSourceUpdating="fdtgCandidates_DataSourceUpdating">
                        <Columns>
                           <asp:TemplateField HeaderText="Cand_Name" SortExpression="Cand_Name">
                              <ItemTemplate>
                             <asp:Label runat="server" Text="ffffffff"/>
                                   "/"
                                <flex:LongTextContainer ID="LongTextContainer_Cand_Name" runat="server" ContainerTitle="Cand_Name" MaxTextLength="10" Text='<%# Eval("Cand_Name")%>' />
                                 <asp:CheckBox runat="server" Text="Okkk"/>
                              </ItemTemplate>
                           </asp:TemplateField>
                          <%-- <asp:BoundField DataField="Cand_Id" HeaderText="ID" SortExpression="Cand_Id" />
                           <asp:BoundField DataField="Cand_Name" HeaderText="Name" SortExpression="Cand_Name" />
                           <asp:BoundField DataField="Cand_Surname" HeaderText="Surname" SortExpression="Cand_Surname" />
                           <asp:BoundField DataField="Cand_Email" HeaderText="Email" SortExpression="Cand_Email" />
                           <asp:BoundField DataField="Gend_Description" HeaderText="Gender" SortExpression="Gend_Description" />
                           <asp:BoundField DataField="Schl_Description" HeaderText="School" SortExpression="Schl_Description" />--%>
                        </Columns>
                     </flex:DataGrid>

                   <%--<asp:GridView ID="DataGrid1" runat="server" PageSize="3" AutoGenerateColumns="false" EnableViewState="true">
                        <Columns>
                           <asp:TemplateField HeaderText="Cand_Name" SortExpression="Cand_Name">
                              <ItemTemplate>
                                 <asp:CheckBox ID="CheckBox1" runat="server" Text="Okkk"/>
                              </ItemTemplate>
                           </asp:TemplateField>
                        </Columns>
                     </asp:GridView>--%>

                  </ContentTemplate>
               </asp:UpdatePanel>
            </div>           
         </div>
         <!-- End Data grid panel -->
         <flex:ImageButton runat="server" ID="btnError" ButtonClass="btn btn-warning" ButtonText="Error"  OnClick="OnClick_Error" />
         <flex:ImageButton runat="server" ID="btnLog" ButtonClass="btn btn-danger" ButtonText="Log"  OnClick="OnClick_Log" />
         <flex:OnOffSwitch runat="server" ID="flexOnOffSwitch" Enabled="False" />
         <flex:ExportList runat="server" ID="flexExportList" OnDataSourceNeeded="flexExportList_DataSourceNeeded" ReportName="CandidateList"></flex:ExportList>
         
         <flex:ImageButton runat="server" ID="flexReport" ButtonClass="btn btn-success" ButtonText="Report" DoPostBack="False" OnClientClick="openReportViewer('SimpleReport', 'A simple report'); return false;" />           
      </div>
      <!-- End Data grid --> 
   </div>
</asp:Content>
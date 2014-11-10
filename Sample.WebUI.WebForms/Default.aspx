<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FLEX.TestWebUI.Default" %>
<%@ Register TagPrefix="ctrl" TagName="MenuBar" Src="~/FLEX/UserControls/MenuBar.ascx" %>
<%@ Register TagPrefix="ctrl" TagName="AutoSuggest" Src="~/FLEX/UserControls/Ajax/AutoSuggest.ascx" %>
<%@ Register TagPrefix="ctrl" TagName="CollapsibleCheckBoxList" Src="~/FLEX/UserControls/Ajax/CollapsibleCheckBoxList.ascx" %>
<%@ Register TagPrefix="flex" Namespace="FLEX.WebControls" Assembly="FLEX.WebControls.DataGrid" %>

<%--<%@ Register TagPrefix="ctrl1" TagName="CollapsibleCheckBoxList" Src="~/FLEX.Extensions.Web/UserControls/Ajax/CollapsibleCheckBoxList.ascx" %>
<%@ Register TagPrefix="ctrl2" TagName="SlidingVerticalPanel" Src="~/FLEX.Extensions.Web/UserControls/SlidingVerticalPanel.ascx" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <title></title>
    
        <script src="Scripts/jquery-1.11.0.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
        <script src="Scripts/jquery-ui-1.10.4.min.js"></script>
        
        <link href="~/FLEX.Extensions.Web/Styles/FLEX.css" rel="stylesheet" />
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
        <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
        <link rel="Stylesheet" type="text/css" href="FLEX.Extensions.Web/Themes/jQueryUI-Bootstrap/jquery-ui-1.10.3.custom.css" />
        <script>



        </script>
        <style>

            .styleRicerca {
                width: 48.5px;
                height: 697px;
                vertical-align: top;
                background-color: #428bca;
                color: black;
                margin-left: -30px;
            }

            .collapse-header {
                color: #ddd;
                background-color: #ddd;
                cursor: pointer;
                font-size: large;
                font-weight: bold;
            }

            .collapse-content { background-color: #ECECEC; }

            #sortable {
                list-style-type: none;
                margin: 0;
                padding: 0;
                width: 100%;
            }

            .col-md-0-5 {
                /*width: 4.166666665%;*/
                width: 2.0833333325%;
                position: relative;
                min-height: 1px;
                padding-right: 15px;
                padding-left: 15px;
                float: left;
            }

            .col-md-11-5 {
                width: 95.833333335%;
                /*width: 97.9166666675%;*/
                position: relative;
                min-height: 1px;
                padding-right: 15px;
                padding-left: 15px;
                float: left;
            }

        </style>

    </head>


    <body>
        <form runat="server">
            <!-- Menu -->
            <div class="active" style="position: relative; z-index: 1">
                <ctrl:MenuBar ID="MenuBar1" runat="server"/>
            </div>

            <!-- Content Page -->
            <div class="container" style="border: 4px solid #428bca; height: 700px;">
                <div id="wrapper" class="active" style="z-index: 0">

                    <asp:ScriptManager ID="ricercaScriptManager" runat="server">
                    </asp:ScriptManager>
          

                    <!-- Sidebar -->
                    <div class="col-md-3" id="sidebar" style="height: 697px; border-right: 4px solid #428bca;">
                      
                        <div class="row">
                            <%-- <div class="col-md-12">   
                    
                         <div class="row">--%>
                            <div class="col-md-7">
                                <div id="HeaderRicerca" style="text-align: left;">
                                    <h1 >Ricerca</h1> 
                                </div>
                            </div>



                            <div class="col-md-2">
                                <!--Button Clear -->
                                <div id="ButtonClear" style="text-align: right;">
                                    <a id="btnClear" class="btn btn-default btn-sm glyphicon glyphicon-trash"><i class="icon-reorder"></i></a>
                                </div> </div>

                            <div class="col-md-2">
                                <!--Button Ricerca -->
                                <div id="ButtonRicerca" style="text-align: right;">
                                    <a id="btnRicerca" class="btn btn-default btn-sm glyphicon glyphicon-arrow-left"><i class="icon-reorder"></i></a>
                                </div> </div>


                            <div class="col-md-1">
                            
                                 
                            </div>
                            <%-- </div>
                             

                        </div>--%>
                        </div>
                     
                        <br />

   

                        <div class="row">
                      
                
                            <script type="text/javascript">
                                $(document).ready(function() {
                                    $("#sortable").sortable({ axis: "y" });
                                    $("#sortable").disableSelection();
                                });

                                var prm = Sys.WebForms.PageRequestManager.getInstance();

                                prm.add_endRequest(function() {
                                    $("#sortable").sortable({ axis: "y" });
                                    $("#sortable").disableSelection();
                                });
                            </script>
                            <div id="ContentRicerca">
                                <ul>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div>    
                                                <!-- Trigger Button HTML -->
                                                <%--<input type="button" class="btn btn-primary" data-toggle="collapse" data-target="#toggleDemo" value="Toggle Button">--%>
                                                <div id="Div1" class="collapse-header" onclick="javascript:R(1)" runat="server">
                                                    <a class="textbox" data-toggle="collapse" data-target="#toggleDemo" ><i id="icr1" class="glyphicon glyphicon-chevron-down"></i>  Genere </a>
                                                </div>
                                                <!-- Collapsible Element HTML -->
                                                <div id="toggleDemo" class="collapse in collapse-content">
                                                    <ctrl:CollapsibleCheckBoxList ID="ctrlGendId" runat="server" MaxVisibleItemCount="2" />
                                                </div>
                                              
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br/>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div>    
                                                <!-- Trigger3 HTML -->
                                                <div id="Div2" class="collapse-header" onclick="javascript:R(3)" runat="server">
                                                    <a class="textbox" data-toggle="collapse" data-target="#divDataAltreR3" ><i id="icr3" class="glyphicon glyphicon-chevron-down"></i>  Istruzione </a>
                                                </div>
                                                <!-- Collapsible Element HTML -->
                                                <div id="divDataAltreR3" class="collapse in collapse-content">
                                                    <ctrl:CollapsibleCheckBoxList  ID="ctrlSchlId" runat="server" MaxVisibleItemCount="3"/>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br/>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div>

                                                <!-- Trigger2 HTML -->
                                                <div id="Div3" class="collapse-header" onclick="javascript:R(2)" runat="server">
                                                    <a id="t2" class="textbox" data-toggle="collapse" data-target="#divDataAltreR2"><i id="icr2" class="glyphicon glyphicon-chevron-down"></i>  Email</a>
                                                </div>


                                                <!-- Collapsible Element2 HTML -->
                                                <%--  <div id="divToggleR2" class="collapse">--%>
                                                <div id="divDataAltreR2" class="collapse in collapse-content" runat="server">
                                                    <ctrl:AutoSuggest ID="autosuggestEmail" Lkup="Candidate" LookupBy="Cand_Email" MenuWidth="250" MenuHeight="300" runat="server" />
                                                </div>
                                                <%--   </div>--%>


                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br/>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div>

                                                <!-- Trigger2 HTML -->
                                                <div id="Div4" class="collapse-header" onclick="javascript:R(4)" runat="server">
                                                    <a id="A1" class="textbox" data-toggle="collapse" data-target="#div5"><i id="icr4" class="glyphicon glyphicon-chevron-down"></i> Nome</a>
                                                </div>


                                                <!-- Collapsible Element2 HTML -->
                                                <%--  <div id="divToggleR2" class="collapse">--%>
                                                <div id="div5" class="collapse in collapse-content" runat="server">
                                                    <ctrl:AutoSuggest ID="autosuggestFullName" Lkup="Candidate" LookupBy="Cand_NameSurname" MenuWidth="250" MenuHeight="300" runat="server" />
                                                </div>
                                                <%--   </div>--%>


                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                                                         
                            </div>    
                            
                        </div> 
                    </div>


                    <!-- Page content -->
                    <div class="col-md-9"  id="page-content">
                        <!-- Content -->
                        <h1>Data</h1>
                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>

                          </ContentTemplate>
                         </asp:UpdatePanel>--%>

                        <asp:UpdatePanel ID="ricercaUpdatePanel" runat="server">
                            <Triggers>

                            </Triggers>
                          
                            <ContentTemplate>

                                <asp:Panel ID="Panel1" runat="server">
                                    
                                </asp:Panel>  
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

      
              

                </div>

            </div>


  

            <!-- Custom JavaScript for the Button Ricerca -->
            <script>
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


                $(function() {
                    $("#btnRicerca").click(function() {
                        if ($("#sidebar").hasClass('col-md-3')) { // Collapse sidebar Ricerca
                            $("#sidebar").switchClass("col-md-3", "col-md-0-5", 0);
                            $("#page-content").switchClass("col-md-9", "col-md-11-5", 0);
                            $("#ContentRicerca").hide();
                            $("#ButtonClear").hide();
                            $("#HeaderRicerca > h1").hide();
                            $("#ButtonRicerca").addClass('styleRicerca');
                            $("#btnRicerca").removeClass('glyphicon glyphicon-arrow-left');
                            $("#btnRicerca").addClass('glyphicon glyphicon-arrow-right');
                        }

                        else if ($("#sidebar").hasClass('col-md-0-5')) { //Expanse sidebar Ricerca
                            $("#page-content").switchClass("col-md-11-5", "col-md-9", 0);
                            $("#sidebar").switchClass("col-md-0-5", "col-md-3", 0);
                            $("#ContentRicerca").show();
                            $("#ButtonClear").show();
                            $("#HeaderRicerca > h1").show();
                            $("#ButtonRicerca").removeClass('styleRicerca');
                            $("#btnRicerca").removeClass('glyphicon glyphicon-arrow-right');
                            $("#btnRicerca").addClass('glyphicon glyphicon-arrow-left');

                        }

                        return false;
                    });
                });

            </script>
        </form>

    </body>
</html>
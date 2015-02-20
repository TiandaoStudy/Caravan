<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAutoSuggest.aspx.cs" Inherits="FLEX.Sample.WebUI.TestAutoSuggest" MasterPageFile="FLEX/MasterPages/Base.Master"%>
<%@ Register TagPrefix="uc" TagName="AutoSuggestControl" Src="~/FLEX/UserControls/Ajax/AutoSuggest.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

    <title>Auto suggest test</title>
    <script type="text/javascript" src="Scripts/popupHandler.js"></script>
    <%--<link href="~/FLEX.Extensions.Web/Styles/FLEX.css" rel="stylesheet" />--%>
    <link href="Content/themes/base/jquery.ui.base.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="Content/04_jsModal.css" />
    <style type="text/css">
        .accordionDiv {
            width: 400px;
        }
    </style>
    <script type="text/javascript">
        function popupOpen() {
            sessionStorage.name = $("#<%=txtbx1.ClientID%>").val();
            sessionStorage.surname = $("#<%=txtbx2.ClientID%>").val();
        }

        function popupClose() {
            //var popupData = top.returnValue;
            //if (!popupData)
            //    return false;
            //alert(popupData.name + ' ' + popupData.surname);
            //$("#txtbx").val(popupData.name + ' ' + popupData.surname);
            $("#<%=txtbx1.ClientID%>").val(sessionStorage.name);
            $("#<%=txtbx2.ClientID%>").val(sessionStorage.surname);
        }

        /* Salvataggio e lettura dei dati in web storage */
        function saveAccordionPositions(sortingPositions) {
            if (!sortingPositions instanceof String)
                return false;

            localStorage.setItem("accordionPositions", sortingPositions); // (!) Salvataggio senza nessun particolare controllo
            return true;
        }

        function readAccordionPositions() {
            return localStorage.getItem("accordionPositions");
        }
        
        function saveAccordionDivState(divId, isOpen) { // Salvo lo stato del div (aperto o chiuso) in una variabile web storage
            if (!divId instanceof String && !isOpen instanceof Boolean) // Controllo dei paramentri
                return false;

            var localDivSel = localStorage.getItem("accDivSelected"); // Prende la variabile web storage di salvataggio
            if (!localDivSel) // Controlla che sia definita
                localDivSel = "";

            var divSel = localDivSel.split(','); // Splitta la stringa in un array

            if (isOpen) // Se il div è aperto
                if (divSel.length == 1 && divSel[0] == "") // Controlla se è il primo elemento a essere "pushato"
                    divSel = divId; // Nel caso si inserisce come semplice stringa
                else
                    divSel.push(divId); // Altrimenti lo aggiunge all'array
            else { // Se è chiuso
                var index = divSel.indexOf(divId); // Cerca se è presente nell'array
                if (index != -1) // Se presente lo elimina
                    divSel.splice(index, 1);
            }

            if (divSel instanceof Array && divSel.length > 1) // Controlla che la variabile sia un array e che abbia almeno un elemento
                divSel.join(','); // Quindi unisce gli elementi in una stringa, separandoli da ','

            localStorage.setItem("accDivSelected", divSel); // Scrive la stringa nella variabile web storage
            return true;
        }

        function readAccordionDivState(divId) { // Ritorna true se il div è stato salvato come aperto, false se come chiuso
            if (!divId instanceof String) // Controllo dei paramentri
                return false; // Se invalido ritorna comunque false

            var localDivSel = localStorage.getItem("accDivSelected");
            if (!localDivSel) // Se non ancora impostata, la imposta vuota
                localDivSel = "";

            var divSel = localDivSel.split(','); // Splitta gli elementi in un array

            for (var i = 0; i < divSel.length; i++) // For su ogni elemento
                if (divSel[i] == divId) // Se trova l'id allora il div è stato salvato come aperto
                    return true;

            return false;
        }
        /*** ***/

        function getAllAccordionDivId(accordionId) { // Ritorna tutti gli id dei div contenuti nell'accordion
            if (!accordionId instanceof String)
                return "";
            return $("#" + accordionId).children().map(function () { return this.id; }).get().join(',');
        }

        function restoreAccordionState() {
            var accPos = readAccordionPositions(); // Legge la posizione dei div dalla varibile di web storage

            if (!accPos) // Se non è impostata la variabile, la imposta con l'ordinamento di default
                accPos = getAllAccordionDivId("accordion");

            $.each(accPos.split(','), function (i, id) { // Per ogni div
                $("#" + id).appendTo('#accordion'); // Lo appende in fondo
                slideAccordionDiv($("#" + id + "> .ui-accordion-header"), readAccordionDivState(id)); // Lo apre o lo chiude a seconda dello stato salvato
            });
        }

        function initAccordionDivState() { // Imposta tutti i div come aperti se non è stata inizializzata (non esiste) la variabile in web storage
            var divSel = localStorage.getItem("accDivSelected");
            if (!divSel && divSel != "") {
                divSel = getAllAccordionDivId("accordion");
                localStorage.setItem("accDivSelected", divSel);
            }
        }

        function slideAccordionDiv(header, down) { // Chiude o apre il div
            var content = header.next('.ui-accordion-content'); // Prende il content del div

            // Toggle the panel's header
            header.toggleClass('ui-corner-all', !down).toggleClass('accordion-header-active ui-state-active ui-corner-top', down).attr('aria-selected', ((down).toString()));
            // Toggle the panel's icon
            header.children('.ui-icon').toggleClass('ui-icon-triangle-1-e', down).toggleClass('ui-icon-triangle-1-s', down);
            // Toggle the panel's content
            header.toggleClass('accordion-content-active', down);

            if (down) // Nasconde o mostra il contenuto
                content.slideDown();
            else
                content.slideUp();
        }

        $(function () {
            $('#accordion').accordion({
                collapsible: true, // Necessaria per il corretto funzionamento
                header: "> div > h3",
                beforeActivate: function (event, ui) {
                    // The accordion believes a panel is being opened
                    if (ui.newHeader[0])
                        var currHeader = ui.newHeader;
                        // The accordion believes a panel is being closed
                    else 
                        var currHeader = ui.oldHeader;
                    
                    // Since we've changed the default behavior, this detects the actual status
                    var isPanelSelected = currHeader.attr('aria-selected') == 'true';

                    slideAccordionDiv(currHeader, !isPanelSelected); // Slideup o slidedown del div

                    event.preventDefault(); // Cancel the default action

                    var divId = currHeader[0].parentNode.id; // Id del div
                    saveAccordionDivState(divId, !isPanelSelected); // Salvataggio dello stato in web storage
                }
            }).sortable({
                axis: "y",
                handle: "h3",
                distance: 25,
                stop: function (event, ui) {
                    // IE doesn't register the blur when sorting
                    // so trigger focusout handlers to remove .ui-state-focus
                    ui.item.children("h3").triggerHandler("focusout");
                },
                update: function (event, ui) { // When sorted
                    // Retrieve IDs
                    var accPos = getAllAccordionDivId($(this).attr("id"));
                    // Salvataggio del'ordine dei div in web storage
                    saveAccordionPositions(accPos);
                }
            });
            initAccordionDivState(); // Inizializza la viabile web storage per lo stato dei div (inizialmente sono tutti aperti)
            restoreAccordionState(); // Ripristino dello stato dell'accordion salvato in web storage
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">

    <div id="accordion" class="accordionDiv">
        <div id="popupDiv" class="group">
            <h3 class="accordionH3">Popoup</h3>
            <div>
                <asp:TextBox ID="txtbx1" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtbx2" runat="server"></asp:TextBox>
                <button id="PopoupButton" onclick="openModalWindow('Popup.aspx',300,300,true,popupOpen,popupClose); return false;">Popup</button>
            </div>
        </div>
        <div id="autoSuggestDiv" class="group">
            <h3 class="accordionH3">Auto suggest</h3>
            <div>

                <asp:UpdatePanel ID="labelUpdatePanel" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="labelSelected" Text="[Valore selezionato]" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <uc:AutoSuggestControl ID="autoSuggestSchl" InputWidth="220" MenuWidth="220" Lkup="School" LookupBy="Schl_Description" runat="server"></uc:AutoSuggestControl>
                <uc:AutoSuggestControl ID="autoSuggestEmail" InputWidth="250" MenuWidth="250" Lkup="Candidate" LookupBy="Cand_Email" runat="server"></uc:AutoSuggestControl>
            </div>
        </div>
        <div id="testDiv" class="group">
            <h3 class="accordionH3">div</h3>
            <div>
                <span>Test div</span>
            </div>
        </div>
    </div>

</asp:Content>

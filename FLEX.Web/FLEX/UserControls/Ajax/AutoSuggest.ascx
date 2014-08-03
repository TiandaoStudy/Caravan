<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoSuggest.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.AutoSuggest" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<style type="text/css">
   .<%= ClientID %>-sugg-size {
      max-height: <%= MaxMenuHeight %>px;
   }
</style>
    
<script type="text/javascript">
   function autoSuggestSource_<%= ClientID %>(request, response) {
      // Campi con i dati
      var txtKey = $("#<%= txtKey.ClientID %>");
      var txtSugg = $("#<%= txtSuggestion.ClientID %>");

      var searchText = txtSugg.val();

      // La stringa da ricercare deve avere almeno due caratteri
      if (!searchText || searchText.length < <%= MinLengthForHint %>) {
         // Se è stata effettuata una ricerca precedente ed il campo e' vuoto
         if (txtKey.val() != "" && searchText == "") {
            setTextBoxValue(txtKey, ""); // Resetto il codice
         }
         response({}); // Resetto i dati dell'autocomplete
         return;
      }

      // La richiesta da inoltrare al servizio
      var json = { xmlLookup: "<%= XmlLookup %>", lookupBy: "<%= LookupBy %>", userQuery: request.term, queryFilter: eval("<%= QueryFilter %>") };
      json = JSON.stringify(json);

      $.ajax({
         type: "POST",
         url: "FLEX/Services/AjaxLookup.asmx/Lookup",
         data: json,
         contentType: "application/json; charset=utf-8",
         dataType: "json",
         success: function(ajaxResponse) {
            // Connessione al web service avvenuta con successo
            response(ajaxResponse.d);
         },
         error: function(jqXhr, textStatus, errorThrown) {
            // Errore durante la connessione al web service
            bootbox.alert("Error during lookup: " + errorThrown);
         }
      });
   }

</script>

<ajax:UpdatePanel ID="updPanel" runat="server">
   <ContentTemplate>       
      <asp:TextBox ID="txtKey" CssClass="hidden" runat="server" OnTextChanged="txtKey_OnTextChanged" />
      <asp:TextBox ID="txtSuggestion" CssClass="ui-widget form-control" runat="server" />
        
      <script type="text/javascript">
         var autoSuggest = $("#<%= txtSuggestion.ClientID %>");

         autoSuggest.autocomplete({
            source: autoSuggestSource_<%= ClientID %>,
            minLength: 0,
            select: function(event, ui) {
                setTextBoxValue($(this), ui.item.Label);
                setTextBoxValue($("#<%= txtKey.ClientID %>"), ui.item.Value);
               if (event) {
                  event.preventDefault();
               }
            },
            delay: 400
         });

         autoSuggest.data("ui-autocomplete")._renderMenu = function(ul, items) {
            var that = this;
            $.each(items, function(index, item) {
               that._renderItemData(ul, item);
            });
            // Assegnazione della classe per il dimensionamento del menu dei suggerimenti
            $(ul).addClass("<%= ClientID %>-sugg-size");
         };

         autoSuggest.data("ui-autocomplete")._renderItem = function(ul, item) {
            var descPresent = false;
            if (typeof item.Descr == "string" && item.Descr.length > 0) // controlla se è presente una descrizione
               descPresent = true;

            var htmlStr = "<a title=\"" + (descPresent ? item.Descr : item.Label) + "\">" + item.Label; // inizio con apertura del tag ed inserimento del label

            if (descPresent) // se presente aggiunge la descrizione
               htmlStr = htmlStr.concat("<br /><span style=\"font-size:smaller\">" + item.Descr + "</span>");

            htmlStr = htmlStr.concat("</a>"); // fine della stringa
            return $("<li></li>").data("ui-autocomplete-item", item).append(htmlStr).appendTo(ul); // ritorno dell'elemento
         };
      </script>
   </ContentTemplate>
</ajax:UpdatePanel>
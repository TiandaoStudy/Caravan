/*
 * NOTA BENE: La sorgente di questo file è il progetto Caravan.
 * Se necessario applicare modifiche a questo script, avvisare chi sviluppa Caravan.
 */

// Gestisce in modo molto semplice l'aggiunta dell'access token di OAuth2
// all'header di autorizzazione (Authorization: Bearer ...)
// all'interno di ogni request destinata a un servizio web prefissato.
'use strict';

var SimpleAuthInterceptor = function SimpleAuthInterceptor(Storage) {
    return {
        'request': function request(config) {
            // Recupero il token, potrebbe non essere presente.
            var token = Storage.get('token');
            // Recupero il nome del web service e lo rendo minuscolo.
            // Faccio la stessa cosa con l'URL della richiesta.
            var wsName = SimpleAuthInterceptor.webServiceName.toLowerCase();
            var url = config.url.toLowerCase();
            // Se il token è presente e l'indirizzo contiene il nome del servizio,
            // allora aggiungo il token agli header della request HTTP.
            if (token && url.indexOf(wsName) > -1) {
                config.headers.Authorization = 'Bearer ' + token.access_token;
            }
            return config;
        }
    };
};

// Registra l'interceptor all'interno di AngularJS.
angular.module(constants.appName).factory('simpleAuthInterceptor', ['Storage', SimpleAuthInterceptor]);

// Il nome del servizio verso cui il token va sicuramente incluso.
// Viene usato per restringere il numero di request in cui viene incluso il token.
SimpleAuthInterceptor.webServiceName = "wsSample";

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIkM6L1Byb2dldHRpL0ZJTlNBL0NhcmF2YW4vQ2FyYXZhbi5XZWJTZXJ2aWNlL0NhcmF2YW4vQWRtaW5pc3RyYXRpb24vanMvZ2VuZXJhdGVkL19pbnRlcmNlcHRvcnMuanMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7OztBQVFBLElBQUkscUJBQXFCLEdBQUcsU0FBeEIscUJBQXFCLENBQWEsT0FBTyxFQUFFO0FBQzNDLFdBQU87QUFDSCxpQkFBUyxFQUFFLGlCQUFVLE1BQU0sRUFBRTs7QUFFekIsZ0JBQUksS0FBSyxHQUFHLE9BQU8sQ0FBQyxHQUFHLENBQUMsT0FBTyxDQUFDLENBQUM7OztBQUdqQyxnQkFBSSxNQUFNLEdBQUcscUJBQXFCLENBQUMsY0FBYyxDQUFDLFdBQVcsRUFBRSxDQUFDO0FBQ2hFLGdCQUFJLEdBQUcsR0FBRyxNQUFNLENBQUMsR0FBRyxDQUFDLFdBQVcsRUFBRSxDQUFDOzs7QUFHbkMsZ0JBQUksS0FBSyxJQUFJLEdBQUcsQ0FBQyxPQUFPLENBQUMsTUFBTSxDQUFDLEdBQUcsQ0FBQyxDQUFDLEVBQUU7QUFDbkMsc0JBQU0sQ0FBQyxPQUFPLENBQUMsYUFBYSxHQUFHLFNBQVMsR0FBRyxLQUFLLENBQUMsWUFBWSxDQUFDO2FBQ2pFO0FBQ0QsbUJBQU8sTUFBTSxDQUFDO1NBQ2pCO0tBQ0osQ0FBQTtDQUNKLENBQUE7OztBQUdELE9BQU8sQ0FBQyxNQUFNLENBQUMsU0FBUyxDQUFDLE9BQU8sQ0FBQyxDQUFDLE9BQU8sQ0FBQyx1QkFBdUIsRUFBRSxDQUFDLFNBQVMsRUFBRSxxQkFBcUIsQ0FBQyxDQUFDLENBQUM7Ozs7QUFJdkcscUJBQXFCLENBQUMsY0FBYyxHQUFHLFVBQVUsQ0FBQyIsImZpbGUiOiJ1bmRlZmluZWQiLCJzb3VyY2VzQ29udGVudCI6WyIvKlxyXG4gKiBOT1RBIEJFTkU6IExhIHNvcmdlbnRlIGRpIHF1ZXN0byBmaWxlIMOoIGlsIHByb2dldHRvIENhcmF2YW4uXHJcbiAqIFNlIG5lY2Vzc2FyaW8gYXBwbGljYXJlIG1vZGlmaWNoZSBhIHF1ZXN0byBzY3JpcHQsIGF2dmlzYXJlIGNoaSBzdmlsdXBwYSBDYXJhdmFuLlxyXG4gKi9cclxuXHJcbi8vIEdlc3Rpc2NlIGluIG1vZG8gbW9sdG8gc2VtcGxpY2UgbCdhZ2dpdW50YSBkZWxsJ2FjY2VzcyB0b2tlbiBkaSBPQXV0aDJcclxuLy8gYWxsJ2hlYWRlciBkaSBhdXRvcml6emF6aW9uZSAoQXV0aG9yaXphdGlvbjogQmVhcmVyIC4uLilcclxuLy8gYWxsJ2ludGVybm8gZGkgb2duaSByZXF1ZXN0IGRlc3RpbmF0YSBhIHVuIHNlcnZpemlvIHdlYiBwcmVmaXNzYXRvLlxyXG52YXIgU2ltcGxlQXV0aEludGVyY2VwdG9yID0gZnVuY3Rpb24gKFN0b3JhZ2UpIHtcclxuICAgIHJldHVybiB7XHJcbiAgICAgICAgJ3JlcXVlc3QnOiBmdW5jdGlvbiAoY29uZmlnKSB7XHJcbiAgICAgICAgICAgIC8vIFJlY3VwZXJvIGlsIHRva2VuLCBwb3RyZWJiZSBub24gZXNzZXJlIHByZXNlbnRlLlxyXG4gICAgICAgICAgICB2YXIgdG9rZW4gPSBTdG9yYWdlLmdldCgndG9rZW4nKTtcclxuICAgICAgICAgICAgLy8gUmVjdXBlcm8gaWwgbm9tZSBkZWwgd2ViIHNlcnZpY2UgZSBsbyByZW5kbyBtaW51c2NvbG8uXHJcbiAgICAgICAgICAgIC8vIEZhY2NpbyBsYSBzdGVzc2EgY29zYSBjb24gbCdVUkwgZGVsbGEgcmljaGllc3RhLlxyXG4gICAgICAgICAgICB2YXIgd3NOYW1lID0gU2ltcGxlQXV0aEludGVyY2VwdG9yLndlYlNlcnZpY2VOYW1lLnRvTG93ZXJDYXNlKCk7XHJcbiAgICAgICAgICAgIHZhciB1cmwgPSBjb25maWcudXJsLnRvTG93ZXJDYXNlKCk7XHJcbiAgICAgICAgICAgIC8vIFNlIGlsIHRva2VuIMOoIHByZXNlbnRlIGUgbCdpbmRpcml6em8gY29udGllbmUgaWwgbm9tZSBkZWwgc2Vydml6aW8sXHJcbiAgICAgICAgICAgIC8vIGFsbG9yYSBhZ2dpdW5nbyBpbCB0b2tlbiBhZ2xpIGhlYWRlciBkZWxsYSByZXF1ZXN0IEhUVFAuXHJcbiAgICAgICAgICAgIGlmICh0b2tlbiAmJiB1cmwuaW5kZXhPZih3c05hbWUpID4gLTEpIHtcclxuICAgICAgICAgICAgICAgIGNvbmZpZy5oZWFkZXJzLkF1dGhvcml6YXRpb24gPSAnQmVhcmVyICcgKyB0b2tlbi5hY2Nlc3NfdG9rZW47XHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgcmV0dXJuIGNvbmZpZztcclxuICAgICAgICB9XHJcbiAgICB9XHJcbn1cclxuXHJcbi8vIFJlZ2lzdHJhIGwnaW50ZXJjZXB0b3IgYWxsJ2ludGVybm8gZGkgQW5ndWxhckpTLlxyXG5hbmd1bGFyLm1vZHVsZShjb25zdGFudHMuYXBwTmFtZSkuZmFjdG9yeSgnc2ltcGxlQXV0aEludGVyY2VwdG9yJywgWydTdG9yYWdlJywgU2ltcGxlQXV0aEludGVyY2VwdG9yXSk7XHJcblxyXG4vLyBJbCBub21lIGRlbCBzZXJ2aXppbyB2ZXJzbyBjdWkgaWwgdG9rZW4gdmEgc2ljdXJhbWVudGUgaW5jbHVzby5cclxuLy8gVmllbmUgdXNhdG8gcGVyIHJlc3RyaW5nZXJlIGlsIG51bWVybyBkaSByZXF1ZXN0IGluIGN1aSB2aWVuZSBpbmNsdXNvIGlsIHRva2VuLlxyXG5TaW1wbGVBdXRoSW50ZXJjZXB0b3Iud2ViU2VydmljZU5hbWUgPSBcIndzU2FtcGxlXCI7Il19

/*
 * NOTA BENE: La sorgente di questo file è il progetto Caravan.
 * Se necessario applicare modifiche a questo script, avvisare chi sviluppa Caravan.
 */

// Gestisce in modo molto semplice l'aggiunta dell'access token di OAuth2
// all'header di autorizzazione (Authorization: Bearer ...)
// all'interno di ogni request destinata a un servizio web prefissato.
var SimpleAuthInterceptor = function (Storage) {
    return {
        'request': function (config) {
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
    }
}

// Registra l'interceptor all'interno di AngularJS.
angular.module(constants.appName).factory('simpleAuthInterceptor', ['Storage', SimpleAuthInterceptor]);

// Il nome del servizio verso cui il token va sicuramente incluso.
// Viene usato per restringere il numero di request in cui viene incluso il token.
SimpleAuthInterceptor.webServiceName = "wsSample";
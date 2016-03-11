### v1.50.0 (12/03/2016) ###

* [FIX] SqlCorsPolicyService ora gestisce anche gli URL di tipo "file://" (issue #2).
* [REM] WebApi non permette più di disabilitare le dipendenze di IdentityServer. Risolve bug allo startup.
* [UPD] Aggiornato AutoMapper alla versione 4.2.1.
* [UDP] Aggiornato IdentityServer alla versione 2.4.0.
* [REM] Rimosso il vecchio Reporting Service, ora ne stiamo usando un altro.
* [FIX] Il DbContext aveva perso il riferimento all'attributo per la gestione del tipo delle date.
* [UPD] Aggiornate le mappature di Caravan alle nuove API di AutoMapper.

### v1.49.0 (10/03/2016) ###

* [UPD] HttpProxyMiddleware ora ignora i cookie e l'header "Host".
* [UPD] Il servizio "help/serviceInfo" ora restituisce data e ora del server e della sorgente dati.

### v1.48.0 (08/03/2016) ###

* [REM] Il pacchetto EFInteractiveViews non è più necessario.
* [REM] Il file LoggerController.cs non è più allegato con WebApi.Runtime.
* [UPD] Fix per HttpProxyMiddleware nella gestione delle POST senza body.
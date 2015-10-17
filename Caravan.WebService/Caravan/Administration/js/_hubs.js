$(function () {
    var ticker = $.connection.serverTime;
    // Add a client-side hub method that the server will call
    ticker.client.getServerTime = function (utcNow) {
        console.log("UTCNOW: " + utcNow.UtcNow);
    }
    // Start the connection
    $.connection.hub.logging = true;
    $.connection.hub.start({ transport: ["serverSentEvents", "foreverFrame", "longPolling"] }).done(function () {
        console.log("DONE");
    }).fail(function () {
        console.log("FAIL");
    });
});
(function() {
  var Settings, root;

  root = typeof exports !== "undefined" && exports !== null ? exports : this;

  Settings = (function() {

    function Settings() {}

    Settings.minFormOffset = 10;

    Settings.sessionTimeoutInMilliseconds = 0;

    Settings.sessionExpiredRedirectUrl = "";

    Settings.rootPath = "";

    Settings.flexPath = "";

    Settings.myFlexPath = "";

    return Settings;

  })();

  root.settings = Settings;

}).call(this);

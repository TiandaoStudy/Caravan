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

  root.showSpinnerOn = function(targetId) {
    var opts, target;
    target = document.getElementById(targetId);
    opts = {
      lines: 13,
      length: 20,
      width: 10,
      radius: 30,
      corners: 1,
      rotate: 0,
      direction: 1,
      color: '#AAA',
      speed: 1,
      trail: 60,
      shadow: false,
      hwaccel: false,
      className: 'spinner',
      zIndex: 2e9,
      top: '50%',
      left: '50%'
    };
    return new Spinner(opts).spin(target);
  };

  root.hideSpinner = function(spinner) {
    return spinner.stop();
  };

  root.showMenuBarSpinner = function() {
    return root.menuBarSpinner = root.showSpinnerOn("main-page-container");
  };

  root.hideMenuBarSpinner = function() {
    return hideSpinner(root.menuBarSpinner);
  };

}).call(this);

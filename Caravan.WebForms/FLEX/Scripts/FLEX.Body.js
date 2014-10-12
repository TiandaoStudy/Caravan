(function() {
  var root;

  root = typeof exports !== "undefined" && exports !== null ? exports : this;

  root.beginRequestHandler = function(sender, args) {
    return $(document).trigger(root.settings.beginAjaxRequest, [sender, args]);
  };

  root.endRequestHandler = function(sender, args) {
    return $(document).trigger(root.settings.endAjaxRequest, [sender, args]);
  };

  root.pageLoad = function() {
    var prm;
    if (!root.pageLoaded) {
      prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.add_beginRequest(beginRequestHandler);
      prm.add_endRequest(endRequestHandler);
      root.pageLoaded = true;
    }
  };

  $(document).ready(function() {
    initPage();
  });

  $(document).on(root.settings.beginAjaxRequest, function(ev, sender, args) {
    if (document.activeElement && document.activeElement.id) {
      root.lastActiveElement = document.activeElement.id;
    }
    root.disableButtonsBeforePostBack();
  });

  $(document).on(root.settings.endAjaxRequest, function(ev, sender, args) {
    var elemToFocus, panel, script, scripts, _i, _j, _len, _len1, _ref;
    _ref = sender._updatePanelClientIDs;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      panel = _ref[_i];
      scripts = $("#" + panel + " script");
      for (_j = 0, _len1 = scripts.length; _j < _len1; _j++) {
        script = scripts[_j];
        eval(script.innerHTML.escapeSpecialChars());
      }
    }
    initPage();
    if (root.lastActiveElement) {
      elemToFocus = document.getElementById(root.lastActiveElement);
      if (elemToFocus && elemToFocus.focus) {
        elemToFocus.focus();
      }
    }
    root.enableButtonsAfterPostBack();
  });

}).call(this);

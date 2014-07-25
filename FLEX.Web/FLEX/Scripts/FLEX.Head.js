(function() {
  var Common, Settings, root;

  root = typeof exports !== "undefined" && exports !== null ? exports : this;

  Settings = (function() {

    function Settings() {}

    Settings.dummyReturnValue = true;

    Settings.beginAjaxRequest = "beginAjaxRequest";

    Settings.endAjaxRequest = "endAjaxRequest";

    Settings.minFormOffset = 10;

    Settings.sessionTimeoutInMilliseconds = 0;

    return Settings;

  })();

  root.settings = Settings;

  Common = (function() {

    function Common() {}

    Common.disableButtonsBeforePostBack = true;

    Common.sessionJsTimeout = null;

    return Common;

  })();

  root.common = Common;

  String.prototype.escapeSpecialChars = function() {
    return this.replace(/\\n/g, "\\n").replace(/\\'/g, "\\'").replace(/\\"/g, '\\"').replace(/\\&/g, "\\&").replace(/\\r/g, "\\r").replace(/\\t/g, "\\t").replace(/\\b/g, "\\b").replace(/\\f/g, "\\f");
  };

  root.bootstrapifyControls = function() {
    $(".form-group input[type=text], .form-group textarea, .form-group select").addClass("form-control");
    $('input[type=file]').bootstrapFileInput();
    return $('input[type="checkbox"]').checkbox({
      checkedClass: 'icomoon icon-checkbox-checked',
      uncheckedClass: 'icomoon icon-checkbox-unchecked'
    });
  };

  root.randomQueryTag = function(opts) {
    var defaults, tag;
    defaults = {
      isFirst: false
    };
    if (!opts) {
      opts = defaults;
    }
    if (!opts.isFirst) {
      opts.isFirst = defaults.isFirst;
    }
    tag = opts.isFirst ? "_ID_=" : "&_ID_=";
    return tag + Date.now();
  };

  root.centerMainContainer = function() {
    var area, diff, frm, nav, top, win;
    frm = $('#mainContainer');
    win = $(window).height();
    nav = $("#menuBar").outerHeight();
    area = win - nav;
    diff = (area - frm.outerHeight()) / 2;
    top = (diff > 0 ? diff : settings.minFormOffset) + nav;
    frm.css({
      position: 'relative',
      top: top + "px"
    });
  };

  root.fixAllDataGrids = function() {
    return $(".datagrid").each(function(idx, dg) {
      return root.fixDataGridPager(dg.id);
    });
  };

  root.fixDataGridPager = function(tableId) {
    var row;
    row = $("#" + tableId + " .datagrid-pager").parent();
    row.css({
      backgroundColor: "white",
      borderBottom: "0px"
    });
  };

  root.displaySystemError = function() {
    return window.open("FLEX/Pages/ErrorHandler.aspx", "SystemError", "menubar=no,location=yes,resizable=yes,scrollbars=yes,status=no,height=500,width=800");
  };

  root.triggerAsyncPostBack = function(hiddenTriggerId) {
    var hiddenTrigger;
    hiddenTrigger = $("#" + hiddenTriggerId);
    hiddenTrigger.val(Math.random());
    return hiddenTrigger.change();
  };

  root.disableButtonsBeforePostBack = function() {
    if (root.common.disableButtonsBeforePostBack) {
      $(".btn").addClass("disabled");
    }
  };

  root.enableButtonsAfterPostBack = function() {
    $(".btn").removeClass("disabled");
  };

  window.onbeforeunload = disableButtonsBeforePostBack;

  root.setSessionJsTimeout = function() {
    if (common.sessionJsTimeout) {
      window.clearTimeout(common.sessionJsTimeout);
    }
    common.sessionJsTimeout = window.setTimeout(function() {
      return window.location = "";
    }, settings.sessionTimeoutInMilliseconds);
  };

  root.initPage = function() {
    root.bootstrapifyControls();
    root.fixAllDataGrids();
  };

}).call(this);

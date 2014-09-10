(function() {
  var Common, Settings, root;

  root = typeof exports !== "undefined" && exports !== null ? exports : this;

  Settings = (function() {

    function Settings() {}

    Settings.dummyReturnValue = true;

    Settings.beginAjaxRequest = "beginAjaxRequest";

    Settings.endAjaxRequest = "endAjaxRequest";

    Settings.minFormOffset = 10;

    Settings.flexPath = "";

    Settings.sessionTimeoutInMilliseconds = 0;

    Settings.sessionExpiredPageUrl = "";

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

  root.setTextBoxValue = function(textBox, value) {
    textBox.val(value);
    textBox.attr("value", value);
    return textBox.change();
  };

  root.bootstrapifyControls = function() {
    return $(".form-group input[type=text], .form-group input:not([type]), .form-group textarea, .form-group select").addClass("form-control");
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
    return window.open(root.settings.flexPath + "/Pages/ErrorHandler.aspx", "SystemError", "menubar=no,location=yes,resizable=yes,scrollbars=yes,status=no,height=500,width=800");
  };

  root.triggerAsyncPostBack = function(hiddenTriggerId) {
    var hiddenTrigger;
    hiddenTrigger = $("#" + hiddenTriggerId);
    hiddenTrigger.val(Math.random());
    return hiddenTrigger.change();
  };

  root.openReportViewer = function(reportName, reportTitle, reportParameters) {
    var encodedParameters;
    if (!reportParameters) {
      reportParameters = {
        dummyParameter: "dummyParameter"
      };
    }
    encodedParameters = Base64.encode64(JSON.stringify(reportParameters));
    openModal({
      url: root.settings.flexPath + ("/Pages/ReportViewer.aspx?reportName=" + reportName + "&encodedParameters=" + encodedParameters) + randomQueryTag(),
      width: "1000px",
      height: "620px",
      title: reportTitle,
      draggable: true
    });
    return false;
  };

  root.disableButtonsBeforePostBack = function() {
    var buttons;
    if (root.common.disableButtonsBeforePostBack && (typeof Page_IsValid === "undefined" || Page_IsValid)) {
      buttons = $(".btn");
      buttons.addClass("disabled");
      buttons.prop("disabled", true);
    }
  };

  root.enableButtonsAfterPostBack = function() {
    var buttons;
    buttons = $(".btn");
    buttons.prop("disabled", false);
    buttons.removeClass("disabled");
  };

  window.onstop = disableButtonsBeforePostBack;

  root.setSessionJsTimeout = function() {
    if (common.sessionJsTimeout) {
      window.clearTimeout(common.sessionJsTimeout);
    }
    common.sessionJsTimeout = window.setTimeout(function() {
      return window.location = settings.sessionExpiredPageUrl;
    }, settings.sessionTimeoutInMilliseconds);
  };

  root.initPage = function() {
    root.bootstrapifyControls();
    root.fixAllDataGrids();
    root.setSessionJsTimeout();
  };

}).call(this);

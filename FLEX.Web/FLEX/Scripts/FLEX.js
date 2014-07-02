(function() {
  var Common, Settings, root;

  root = typeof exports !== "undefined" && exports !== null ? exports : this;

  Settings = (function() {

    function Settings() {}

    Settings.dummyReturnValue = true;

    Settings.beginAjaxRequest = "beginAjaxRequest";

    Settings.endAjaxRequest = "endAjaxRequest";

    Settings.minFormOffset = 10;

    return Settings;

  })();

  root.settings = Settings;

  Common = (function() {

    function Common() {}

    Common.disableButtonsBeforePostBack = true;

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
    return tag + new Date().getDate().toString();
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
    var maxWidth, pageBtnWidths, pageButtons, row;
    pageButtons = $("#" + tableId + " .datagrid-pager span, #" + tableId + " .datagrid-pager a");
    pageBtnWidths = _.map(pageButtons, function(x) {
      return $(x).width();
    });
    maxWidth = _.max(pageBtnWidths);
    pageButtons.width(maxWidth);
    row = $("#" + tableId + " .datagrid-pager").parent();
    row.css({
      backgroundColor: "white"
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
    root.bootstrapifyControls();
    root.fixAllDataGrids();
  });

  $(document).on(root.settings.beginAjaxRequest, function(ev, sender, args) {
    if (document.activeElement && document.activeElement.id) {
      root.lastActiveElement = document.activeElement.id;
    }
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
    root.bootstrapifyControls();
    root.fixAllDataGrids();
    if (root.lastActiveElement) {
      elemToFocus = document.getElementById(root.lastActiveElement);
      if (elemToFocus && elemToFocus.focus) {
        elemToFocus.focus();
      }
    }
    root.enableButtonsAfterPostBack();
  });

  root.disableButtonsBeforePostBack = function() {
    if (root.common.disableButtonsBeforePostBack) {
      $(".btn").addClass("disabled");
    }
  };

  root.enableButtonsAfterPostBack = function() {
    $(".btn").removeClass("disabled");
  };

  window.onbeforeunload = disableButtonsBeforePostBack;

}).call(this);

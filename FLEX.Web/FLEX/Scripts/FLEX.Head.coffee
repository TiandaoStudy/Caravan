# CoffeeScript
root = exports ? this

################################################################################
# Settings
################################################################################

class Settings
   @dummyReturnValue: true
   @beginAjaxRequest: "beginAjaxRequest"
   @endAjaxRequest: "endAjaxRequest"
   @minFormOffset: 10
   @flexPath: "" # This variable is set in Head.Master
   @sessionTimeoutInMilliseconds: 0 # This variable is set in Head.Master
   @sessionExpiredPageUrl: "" # This variable is set in Head.Master
   
root.settings = Settings

class Common
   @disableButtonsBeforePostBack = true
   @sessionJsTimeout = null
   
root.common = Common

################################################################################
# Various
################################################################################

String.prototype.escapeSpecialChars = () ->
    return this.replace(/\\n/g, "\\n")
               .replace(/\\'/g, "\\'")
               .replace(/\\"/g, '\\"')
               .replace(/\\&/g, "\\&")
               .replace(/\\r/g, "\\r")
               .replace(/\\t/g, "\\t")
               .replace(/\\b/g, "\\b")
               .replace(/\\f/g, "\\f")

root.setTextBoxValue = (textBox, value) ->
    textBox.val(value)
    textBox.attr("value", value)
    textBox.change()

root.bootstrapifyControls = () ->
   # Makes form elements prettier
   $(".form-group input[type=text], .form-group textarea, .form-group select").addClass("form-control")

root.randomQueryTag = (opts) ->
   defaults = {isFirst: false}
   unless opts
      opts = defaults
   unless opts.isFirst
      opts.isFirst = defaults.isFirst
   tag = if opts.isFirst then "_ID_=" else "&_ID_="
   return tag + Date.now()
   
root.centerMainContainer = () ->
   frm = $('#mainContainer')
   win = $(window).height()
   nav = $("#menuBar").outerHeight()
   area = win - nav
   diff = (area - frm.outerHeight()) / 2
   top = (if diff > 0 then diff else settings.minFormOffset) + nav
   frm.css({
      position: 'relative',
      top: top + "px"
   })
   return

################################################################################
# AutoSuggest
################################################################################



################################################################################
# DataGrid
################################################################################

root.fixAllDataGrids = () ->
   $(".datagrid").each((idx, dg) -> root.fixDataGridPager(dg.id))

root.fixDataGridPager = (tableId) ->   
   # Pagination should neither hover, nor have an alternating background
   row = $("##{tableId} .datagrid-pager").parent()
   row.css({backgroundColor: "white"; borderBottom: "0px"})
   
   # Required to avoid strange CoffeeScript behaviour
   return

################################################################################
# ErrorHandler
################################################################################

root.displaySystemError = () ->
   window.open(root.settings.flexPath + "/Pages/ErrorHandler.aspx", "SystemError",
               "menubar=no,location=yes,resizable=yes,scrollbars=yes,status=no,height=500,width=800")

################################################################################
# HiddenTrigger
################################################################################

root.triggerAsyncPostBack = (hiddenTriggerId) ->
   hiddenTrigger = $("##{hiddenTriggerId}")
   hiddenTrigger.val(Math.random())
   hiddenTrigger.change()
   
################################################################################
# ReportViewer
################################################################################

root.openReportViewer = (reportName, reportTitle) ->
   openModal(
      url: root.settings.flexPath + "/Pages/ReportViewer.aspx?reportName=#{reportName}" + randomQueryTag(),
      width: "1000px", # One pixel more than the width specified in Pages/ReportViewer.aspx
      height: "620px"
      title: reportTitle
      draggable: true
   )
   return false
   
################################################################################
# PostBack Management
################################################################################

root.disableButtonsBeforePostBack = () ->
   # First trigger page validation. If page is not valid, then we do not disable buttons.
   if typeof Page_ClientValidate != "undefined" then Page_ClientValidate()

   if root.common.disableButtonsBeforePostBack and (typeof Page_IsValid == "undefined" or Page_IsValid)
      buttons = $(".btn")
      buttons.addClass("disabled")
      # This line is needed for IE9, since "disabled" class is not enough...
      buttons.prop("disabled", true)
   
   # We do not need to cleanup "common.disableButtonsBeforePostBack", because the post back will.
   # Therefore, on next run, disableButtonsBeforePostBack will be correctly true.
   
   # Required, otherwise browser shows a quit message.
   return 

root.enableButtonsAfterPostBack = () ->
   buttons = $(".btn")
   buttons.prop("disabled", false)
   buttons.removeClass("disabled")
   # Required, otherwise browser shows a quit message
   return 

window.onbeforeunload = disableButtonsBeforePostBack;

################################################################################
# Page Management
################################################################################

root.setSessionJsTimeout = () ->
   # A timeout is already active, therefore we should stop it.
   if common.sessionJsTimeout
      window.clearTimeout(common.sessionJsTimeout)
   
   common.sessionJsTimeout = window.setTimeout(() ->
      window.location = settings.sessionExpiredPageUrl
   , settings.sessionTimeoutInMilliseconds)
   
   return

root.initPage = () ->
   # Sets controls styles
   root.bootstrapifyControls()
   root.fixAllDataGrids()
   
   # Automatic session expiry maganagement
   root.setSessionJsTimeout()
   
   return
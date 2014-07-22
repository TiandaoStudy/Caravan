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
   
root.settings = Settings

class Common
   @disableButtonsBeforePostBack = true;
   
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

root.bootstrapifyControls = () ->
   # Makes form elements prettier
   $(".form-group input[type=text], .form-group textarea, .form-group select").addClass("form-control")
   $('input[type=file]').bootstrapFileInput()
   $('input[type="checkbox"]').checkbox({
      checkedClass: 'icomoon icon-checkbox-checked',
      uncheckedClass: 'icomoon icon-checkbox-unchecked'
   })

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
   # All buttons should have the same width
#   pageButtons = $("##{tableId} .datagrid-pager span, ##{tableId} .datagrid-pager a");
#   pageBtnWidths = _.map(pageButtons, (x) -> $(x).width())
#   maxWidth = _.max(pageBtnWidths)
#   pageButtons.width(maxWidth)
   
   # Pagination should neither hover, nor have an alternating background
   row = $("##{tableId} .datagrid-pager").parent()
   row.css({backgroundColor: "white"; borderBottom: "0px"})
   
   # Required to avoid strange CoffeeScript behaviour
   return

################################################################################
# ErrorHandler
################################################################################

root.displaySystemError = () ->
   window.open("FLEX/Pages/ErrorHandler.aspx", "SystemError",
               "menubar=no,location=yes,resizable=yes,scrollbars=yes,status=no,height=500,width=800")

################################################################################
# HiddenTrigger
################################################################################

root.triggerAsyncPostBack = (hiddenTriggerId) ->
   hiddenTrigger = $("##{hiddenTriggerId}")
   hiddenTrigger.val(Math.random())
   hiddenTrigger.change()

################################################################################
# UpdatePanel & JS
################################################################################

root.beginRequestHandler = (sender, args) ->
   $(document).trigger(root.settings.beginAjaxRequest, [sender, args])

root.endRequestHandler = (sender, args) ->
   $(document).trigger(root.settings.endAjaxRequest, [sender, args])

root.pageLoad = () ->
   unless root.pageLoaded
      prm = Sys.WebForms.PageRequestManager.getInstance()
      prm.add_beginRequest(beginRequestHandler)
      prm.add_endRequest(endRequestHandler)
      root.pageLoaded = true
   return 

$(document).ready(() ->
   # Sets controls styles
   root.bootstrapifyControls()
   root.fixAllDataGrids()
   
   # Required to avoid strange CoffeeScript behaviour
   return
)

$(document).on(root.settings.beginAjaxRequest, (ev, sender, args) ->
   # Stores control with focus
   if document.activeElement and document.activeElement.id
      root.lastActiveElement = document.activeElement.id
   
   # Required to avoid strange CoffeeScript behaviour
   return
)

# Reloads scripts inside each update panel
$(document).on(root.settings.endAjaxRequest, (ev, sender, args) ->
   for panel in sender._updatePanelClientIDs
      scripts = $("##{panel} script")
      for script in scripts then eval(script.innerHTML.escapeSpecialChars())
      
   # Restores controls styles
   root.bootstrapifyControls()
   root.fixAllDataGrids()
   
   # It seems that some HTML elements do not have a focus method...
   if root.lastActiveElement
      elemToFocus = document.getElementById(root.lastActiveElement)
      if elemToFocus and elemToFocus.focus
         elemToFocus.focus()
   
   # We need to enable buttons, because IE9 treats partial postbacks as full postbacks
   root.enableButtonsAfterPostBack()
   
   # Required to avoid strange CoffeeScript behaviour
   return
)

################################################################################
# PostBack
################################################################################

root.disableButtonsBeforePostBack = () ->
   if root.common.disableButtonsBeforePostBack
      $(".btn").addClass("disabled")
   # We do not need to cleanup that value, because the post back will.
   # Therefore, on next run, disableButtonsBeforePostBack will be correctly true.
   
   # Required, otherwise browser shows a quit message
   return 

root.enableButtonsAfterPostBack = () ->
   $(".btn").removeClass("disabled")
   # Required, otherwise browser shows a quit message
   return 

window.onbeforeunload = disableButtonsBeforePostBack;
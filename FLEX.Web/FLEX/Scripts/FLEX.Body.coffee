# CoffeeScript
root = exports ? this

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
   initPage()
   
   # Required to avoid strange CoffeeScript behaviour
   return
)

$(document).on(root.settings.beginAjaxRequest, (ev, sender, args) ->
   # Stores control with focus
   if document.activeElement and document.activeElement.id
      root.lastActiveElement = document.activeElement.id
   
   root.disableButtonsBeforePostBack()
   
   # Required to avoid strange CoffeeScript behaviour
   return
)

# Reloads scripts inside each update panel
$(document).on(root.settings.endAjaxRequest, (ev, sender, args) ->
   for panel in sender._updatePanelClientIDs
      scripts = $("##{panel} script")
      for script in scripts then eval(script.innerHTML.escapeSpecialChars())
      
   initPage()
   
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
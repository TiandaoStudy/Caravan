# CoffeeScript
root = exports ? this

################################################################################
# Settings
################################################################################

class Settings
   # These variables are set in FlexCoreLayout
   @sessionTimeoutInMilliseconds: 0 
   @sessionExpiredRedirectUrl: ""
   @rootPath: ""
   @flexPath: ""
   @myFlexPath: ""
   
root.settings = Settings

################################################################################
# Search Criteria
################################################################################

root.SearchCriteria = Backbone.Model.extend({
   # Empty?
})

root.searchCriteriaCollection = []

root.addSearchCriteria = (searchCriteriaId) ->
   searchCriteria = new SearchCriteria()
   searchCriteria.set({ id: searchCriteriaId })
   root.searchCriteriaCollection[searchCriteriaId] = searchCriteria

root.getSearchCriteria = (searchCriteriaId) -> 
   root.searchCriteriaCollection[searchCriteriaId]
         
root.removeSearchCriteria = (searchCriteriaId) ->
   delete root.searchCriteriaCollection[searchCriteriaId]
   
root.initSearchCriteria = (searchCriteriaId) ->
   root.searchCriteriaCollection[searchCriteriaId].set({ criteria: [] })

################################################################################
# Wait Spinner
################################################################################

root.showSpinnerOn = (targetId) ->
   target = document.getElementById(targetId)
   opts = {
     lines: 13, # The number of lines to draw
     length: 20, # The length of each line
     width: 10, # The line thickness
     radius: 30, # The radius of the inner circle
     corners: 1, # Corner roundness (0..1)
     rotate: 0, # The rotation offset
     direction: 1, # 1: clockwise, -1: counterclockwise
     color: '#AAA', # #rgb or #rrggbb or array of colors
     speed: 1, # Rounds per second
     trail: 60, # Afterglow percentage
     shadow: false, # Whether to render a shadow
     hwaccel: false, # Whether to use hardware acceleration
     className: 'spinner', # The CSS class to assign to the spinner
     zIndex: 2e9, # The z-index (defaults to 2000000000)
     top: '50%', # Top position relative to parent
     left: '50%' # Left position relative to parent
   }
   return new Spinner(opts).spin(target)
   
root.hideSpinner = (spinner) ->
   spinner.stop()
   
root.showMenuBarSpinner = () ->
   root.menuBarSpinner = root.showSpinnerOn("main-page-container")
   
root.hideMenuBarSpinner = () ->
   hideSpinner(root.menuBarSpinner)
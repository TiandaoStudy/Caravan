# CoffeeScript
root = exports ? this

################################################################################
# Settings
################################################################################

class Settings
   @minFormOffset: 10
   # These variables are set in FlexCoreLayout
   @sessionTimeoutInMilliseconds: 0 
   @sessionExpiredRedirectUrl: ""
   @rootPath: ""
   @flexPath: ""
   @myFlexPath: ""
   
root.settings = Settings
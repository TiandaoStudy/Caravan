# CoffeeScript
root = exports ? this

################################################################################
# SecGroup
################################################################################

root.getGroupsByNameOrID = (term) ->
   query = "$filter=substringof('#{term}',Name) eq true or substringof('#{term}',Description) eq true"
   if not root.isNaN(root.parseInt(term)) then query += " or substringof(#{term},Id) eq true"
   return query

root.mapGroupsForSelect2 = (groups) ->
   _.map(groups.Groups, (g) ->
      { id: g.Id, text: g.Name + " - " + g.Description, disabled: false }
   )
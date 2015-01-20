(function() {
  var root;

  root = typeof exports !== "undefined" && exports !== null ? exports : this;

  root.getGroupsByNameOrID = function(term) {
    var query;
    query = "$filter=substringof('" + term + "',Name) eq true or substringof('" + term + "',Description) eq true";
    if (!root.isNaN(root.parseInt(term))) {
      query += " or substringof(" + term + ",Id) eq true";
    }
    return query;
  };

  root.mapGroupsForSelect2 = function(groups) {
    return _.map(groups.Groups, function(g) {
      return {
        id: g.Id,
        text: g.Name + " - " + g.Description,
        disabled: false
      };
    });
  };

}).call(this);

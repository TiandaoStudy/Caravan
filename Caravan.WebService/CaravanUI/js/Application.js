/// <reference path='_all.ts' />
/**
 * The main Caravan app module.
 *
 * @type {angular.Module}
 */
var todos;
(function (todos) {
    'use strict';
    var todomvc = angular.module('todomvc', [])
        .controller('todoCtrl', todos.TodoCtrl)
        .directive('todoBlur', todos.todoBlur)
        .directive('todoFocus', todos.todoFocus)
        .service('todoStorage', todos.TodoStorage);
})(todos || (todos = {}));
//# sourceMappingURL=Application.js.map
'use strict';

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

var moduleName = "caravanAdminUI";
/*global angular */

/**
 * The main TodoMVC app module
 *
 * @type {angular.Module}
 */
angular.module(moduleName, ['ngRoute']).config(function ($routeProvider) {
	'use strict';

	var routeConfig = {
		controller: 'TodoCtrl',
		templateUrl: 'caravanAdminUI-index.html',
		resolve: {
			store: function store(todoStorage) {
				// Get the correct module (API or localStorage).
				return todoStorage.then(function (module) {
					module.get(); // Fetch the todo records in the background.
					return module;
				});
			}
		}
	};

	$routeProvider.when('/', routeConfig).when('/:status', routeConfig).otherwise({
		redirectTo: '/'
	});
});

/*global angular */

/**
 * The main controller for the app. The controller:
 * - retrieves and persists the model via the todoStorage service
 * - exposes the model to the template and provides event handlers
 */
angular.module(moduleName).controller('TodoCtrl', function TodoCtrl($scope, $routeParams, $filter, store, logService) {
	'use strict';

	var todos = $scope.todos = store.todos;

	$scope.newTodo = '';
	$scope.editedTodo = null;

	$scope.$watch('todos', function () {
		$scope.remainingCount = $filter('filter')(todos, { completed: false }).length;
		$scope.completedCount = todos.length - $scope.remainingCount;
		$scope.allChecked = !$scope.remainingCount;
	}, true);

	// Monitor the current route for changes and adjust the filter accordingly.
	$scope.$on('$routeChangeSuccess', function () {
		var status = $scope.status = $routeParams.status || '';
		$scope.statusFilter = status === 'active' ? { completed: false } : status === 'completed' ? { completed: true } : {};
	});

	$scope.addTodo = function () {
		var newTodo = {
			title: $scope.newTodo.trim(),
			completed: false
		};

		console.log(logService.getAllSettings());

		if (!newTodo.title) {
			return;
		}

		$scope.saving = true;
		store.insert(newTodo).then(function success() {
			$scope.newTodo = '';
		})['finally'](function () {
			$scope.saving = false;
		});
	};

	$scope.editTodo = function (todo) {
		$scope.editedTodo = todo;
		// Clone the original todo to restore it on demand.
		$scope.originalTodo = angular.extend({}, todo);
	};

	$scope.saveEdits = function (todo, event) {
		// Blur events are automatically triggered after the form submit event.
		// This does some unfortunate logic handling to prevent saving twice.
		if (event === 'blur' && $scope.saveEvent === 'submit') {
			$scope.saveEvent = null;
			return;
		}

		$scope.saveEvent = event;

		if ($scope.reverted) {
			// Todo edits were reverted-- don't save.
			$scope.reverted = null;
			return;
		}

		todo.title = todo.title.trim();

		if (todo.title === $scope.originalTodo.title) {
			$scope.editedTodo = null;
			return;
		}

		store[todo.title ? 'put' : 'delete'](todo).then(function success() {}, function error() {
			todo.title = $scope.originalTodo.title;
		})['finally'](function () {
			$scope.editedTodo = null;
		});
	};

	$scope.revertEdits = function (todo) {
		todos[todos.indexOf(todo)] = $scope.originalTodo;
		$scope.editedTodo = null;
		$scope.originalTodo = null;
		$scope.reverted = true;
	};

	$scope.removeTodo = function (todo) {
		store['delete'](todo);
	};

	$scope.saveTodo = function (todo) {
		store.put(todo);
	};

	$scope.toggleCompleted = function (todo, completed) {
		if (angular.isDefined(completed)) {
			todo.completed = completed;
		}
		store.put(todo, todos.indexOf(todo)).then(function success() {}, function error() {
			todo.completed = !todo.completed;
		});
	};

	$scope.clearCompletedTodos = function () {
		store.clearCompleted();
	};

	$scope.markAll = function (completed) {
		todos.forEach(function (todo) {
			if (todo.completed !== completed) {
				$scope.toggleCompleted(todo, completed);
			}
		});
	};
});

var LogService = (function () {
	function LogService($http) {
		_classCallCheck(this, LogService);

		this.$http = $http;
	}

	_createClass(LogService, [{
		key: 'getAllSettings',
		value: function getAllSettings() {
			return this.$http.get('https://localhost:44300/logger/settings').then(function (result) {
				return result.data;
			});
		}
	}, {
		key: 'getActiveBooks',
		value: function getActiveBooks() {
			return this.$http.get('/api/activeBooks').then(function (result) {
				return result.data;
			});
		}
	}, {
		key: 'getArchivedBooks',
		value: function getArchivedBooks() {
			return this.$http.get('/api/archivedBooks').then(function (result) {
				return result.data;
			});
		}
	}, {
		key: 'markBookRead',
		value: function markBookRead(bookId, isBookRead) {
			return this.$http.put('/api/markRead/' + bookId, { bookId: bookId, read: isBookRead });
		}
	}, {
		key: 'addToArchive',
		value: function addToArchive(bookId) {
			return this.$http.put('/api/addToArchive/' + bookId, {});
		}
	}, {
		key: 'checkIfBookExists',
		value: function checkIfBookExists(title) {
			return this.$http.get('/api/bookExists/' + title).then(function (result) {
				return result.data;
			});
		}
	}, {
		key: 'addBook',
		value: function addBook(book) {
			return this.$http.post('/api/books', book);
		}
	}], [{
		key: 'logFactory',
		value: function logFactory($http) {
			return new LogService($http);
		}
	}]);

	return LogService;
})();

LogService.logFactory.$inject = ['$http'];

angular.module(moduleName).factory('logService', LogService.logFactory);
/*global angular */

/**
 * Services that persists and retrieves todos from localStorage or a backend API
 * if available.
 *
 * They both follow the same API, returning promises for all changes to the
 * model.
 */
angular.module(moduleName).factory('todoStorage', function ($http, $injector) {
	'use strict';

	// Detect if an API backend is present. If so, return the API module, else
	// hand off the localStorage adapter
	return $http.get('/api').then(function () {
		return $injector.get('api');
	}, function () {
		return $injector.get('localStorage');
	});
}).factory('api', function ($http) {
	'use strict';

	var store = {
		todos: [],

		clearCompleted: function clearCompleted() {
			var originalTodos = store.todos.slice(0);

			var completeTodos = [];
			var incompleteTodos = [];
			store.todos.forEach(function (todo) {
				if (todo.completed) {
					completeTodos.push(todo);
				} else {
					incompleteTodos.push(todo);
				}
			});

			angular.copy(incompleteTodos, store.todos);

			return $http['delete']('/api/todos').then(function success() {
				return store.todos;
			}, function error() {
				angular.copy(originalTodos, store.todos);
				return originalTodos;
			});
		},

		'delete': function _delete(todo) {
			var originalTodos = store.todos.slice(0);

			store.todos.splice(store.todos.indexOf(todo), 1);

			return $http['delete']('/api/todos/' + todo.id).then(function success() {
				return store.todos;
			}, function error() {
				angular.copy(originalTodos, store.todos);
				return originalTodos;
			});
		},

		get: function get() {
			return $http.get('/api/todos').then(function (resp) {
				angular.copy(resp.data, store.todos);
				return store.todos;
			});
		},

		insert: function insert(todo) {
			var originalTodos = store.todos.slice(0);

			return $http.post('/api/todos', todo).then(function success(resp) {
				todo.id = resp.data.id;
				store.todos.push(todo);
				return store.todos;
			}, function error() {
				angular.copy(originalTodos, store.todos);
				return store.todos;
			});
		},

		put: function put(todo) {
			var originalTodos = store.todos.slice(0);

			return $http.put('/api/todos/' + todo.id, todo).then(function success() {
				return store.todos;
			}, function error() {
				angular.copy(originalTodos, store.todos);
				return originalTodos;
			});
		}
	};

	return store;
}).factory('localStorage', function ($q) {
	'use strict';

	var STORAGE_ID = 'todos-angularjs';

	var store = {
		todos: [],

		_getFromLocalStorage: function _getFromLocalStorage() {
			return JSON.parse(localStorage.getItem(STORAGE_ID) || '[]');
		},

		_saveToLocalStorage: function _saveToLocalStorage(todos) {
			localStorage.setItem(STORAGE_ID, JSON.stringify(todos));
		},

		clearCompleted: function clearCompleted() {
			var deferred = $q.defer();

			var completeTodos = [];
			var incompleteTodos = [];
			store.todos.forEach(function (todo) {
				if (todo.completed) {
					completeTodos.push(todo);
				} else {
					incompleteTodos.push(todo);
				}
			});

			angular.copy(incompleteTodos, store.todos);

			store._saveToLocalStorage(store.todos);
			deferred.resolve(store.todos);

			return deferred.promise;
		},

		'delete': function _delete(todo) {
			var deferred = $q.defer();

			store.todos.splice(store.todos.indexOf(todo), 1);

			store._saveToLocalStorage(store.todos);
			deferred.resolve(store.todos);

			return deferred.promise;
		},

		get: function get() {
			var deferred = $q.defer();

			angular.copy(store._getFromLocalStorage(), store.todos);
			deferred.resolve(store.todos);

			return deferred.promise;
		},

		insert: function insert(todo) {
			var deferred = $q.defer();

			store.todos.push(todo);

			store._saveToLocalStorage(store.todos);
			deferred.resolve(store.todos);

			return deferred.promise;
		},

		put: function put(todo, index) {
			var deferred = $q.defer();

			store.todos[index] = todo;

			store._saveToLocalStorage(store.todos);
			deferred.resolve(store.todos);

			return deferred.promise;
		}
	};

	return store;
});

//const HTTP = new WeakMap();

//class BookShelfService
//{
//    constructor($http)
//    {
//        HTTP.set(this, $http);
//    }

//    getActiveBooks(){
//        return HTTP.get(this).get('/api/activeBooks').then(result => result.data );
//    }

//    getArchivedBooks(){
//        return HTTP.get(this).get('/api/archivedBooks').then(result => result.data );
//    }

//    markBookRead(bookId, isBookRead){
//        return HTTP.get(this).put(`/api/markRead/${bookId}`, {bookId: bookId, read: isBookRead});
//    }

//    addToArchive(bookId){
//        return HTTP.get(this).put(`/api/addToArchive/${bookId}`,{});
//    }

//    checkIfBookExists(title){
//        return HTTP.get(this).get(`/api/bookExists/${title}`).then(result =>  result.data );
//    }

//    addBook(book){
//        return HTTP.get(this).post('/api/books', book);
//    }

//    static bookShelfFactory($http){
//        return new BookShelfService($http);
//    }
//}

//BookShelfService.bookShelfFactory.$inject = ['$http'];

//angular.module(moduleName, [])
//  .factory('bookShelfSvc', BookShelfService.bookShelfFactory);
/*global angular */

/**
 * Directive that executes an expression when the element it is applied to gets
 * an `escape` keydown event.
 */
angular.module(moduleName).directive('todoEscape', function () {
	'use strict';

	var ESCAPE_KEY = 27;

	return function (scope, elem, attrs) {
		elem.bind('keydown', function (event) {
			if (event.keyCode === ESCAPE_KEY) {
				scope.$apply(attrs.todoEscape);
			}
		});

		scope.$on('$destroy', function () {
			elem.unbind('keydown');
		});
	};
});

/*global angular */

/**
 * Directive that places focus on the element it is applied to when the
 * expression it binds to evaluates to true
 */
angular.module(moduleName).directive('todoFocus', function todoFocus($timeout) {
	'use strict';

	return function (scope, elem, attrs) {
		scope.$watch(attrs.todoFocus, function (newVal) {
			if (newVal) {
				$timeout(function () {
					elem[0].focus();
				}, 0, false);
			}
		});
	};
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIkQ6L1Byb2dldHRpL0ZJTlNBL0NhcmF2YW4vQ2FyYXZhbi5XZWJTZXJ2aWNlL0NhcmF2YW4vQWRtaW5pc3RyYXRpb24vanMvX2FsbC5qcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Ozs7QUFBQSxJQUFNLFVBQVUsR0FBRyxnQkFBZ0IsQ0FBQzs7Ozs7Ozs7QUFRcEMsT0FBTyxDQUFDLE1BQU0sQ0FBQyxVQUFVLEVBQUUsQ0FBQyxTQUFTLENBQUMsQ0FBQyxDQUNyQyxNQUFNLENBQUMsVUFBVSxjQUFjLEVBQUU7QUFDakMsYUFBWSxDQUFDOztBQUViLEtBQUksV0FBVyxHQUFHO0FBQ2pCLFlBQVUsRUFBRSxVQUFVO0FBQ3RCLGFBQVcsRUFBRSwyQkFBMkI7QUFDeEMsU0FBTyxFQUFFO0FBQ1IsUUFBSyxFQUFFLGVBQVUsV0FBVyxFQUFFOztBQUU3QixXQUFPLFdBQVcsQ0FBQyxJQUFJLENBQUMsVUFBVSxNQUFNLEVBQUU7QUFDekMsV0FBTSxDQUFDLEdBQUcsRUFBRSxDQUFDO0FBQ2IsWUFBTyxNQUFNLENBQUM7S0FDZCxDQUFDLENBQUM7SUFDSDtHQUNEO0VBQ0QsQ0FBQzs7QUFFRixlQUFjLENBQ1osSUFBSSxDQUFDLEdBQUcsRUFBRSxXQUFXLENBQUMsQ0FDdEIsSUFBSSxDQUFDLFVBQVUsRUFBRSxXQUFXLENBQUMsQ0FDN0IsU0FBUyxDQUFDO0FBQ1YsWUFBVSxFQUFFLEdBQUc7RUFDZixDQUFDLENBQUM7Q0FDSixDQUFDLENBQUM7Ozs7Ozs7OztBQVNKLE9BQU8sQ0FBQyxNQUFNLENBQUMsVUFBVSxDQUFDLENBQ3hCLFVBQVUsQ0FBQyxVQUFVLEVBQUUsU0FBUyxRQUFRLENBQUMsTUFBTSxFQUFFLFlBQVksRUFBRSxPQUFPLEVBQUUsS0FBSyxFQUFFLFVBQVUsRUFBRTtBQUMzRixhQUFZLENBQUM7O0FBRWIsS0FBSSxLQUFLLEdBQUcsTUFBTSxDQUFDLEtBQUssR0FBRyxLQUFLLENBQUMsS0FBSyxDQUFDOztBQUV2QyxPQUFNLENBQUMsT0FBTyxHQUFHLEVBQUUsQ0FBQztBQUNwQixPQUFNLENBQUMsVUFBVSxHQUFHLElBQUksQ0FBQzs7QUFFekIsT0FBTSxDQUFDLE1BQU0sQ0FBQyxPQUFPLEVBQUUsWUFBWTtBQUNsQyxRQUFNLENBQUMsY0FBYyxHQUFHLE9BQU8sQ0FBQyxRQUFRLENBQUMsQ0FBQyxLQUFLLEVBQUUsRUFBRSxTQUFTLEVBQUUsS0FBSyxFQUFFLENBQUMsQ0FBQyxNQUFNLENBQUM7QUFDOUUsUUFBTSxDQUFDLGNBQWMsR0FBRyxLQUFLLENBQUMsTUFBTSxHQUFHLE1BQU0sQ0FBQyxjQUFjLENBQUM7QUFDN0QsUUFBTSxDQUFDLFVBQVUsR0FBRyxDQUFDLE1BQU0sQ0FBQyxjQUFjLENBQUM7RUFDM0MsRUFBRSxJQUFJLENBQUMsQ0FBQzs7O0FBR1QsT0FBTSxDQUFDLEdBQUcsQ0FBQyxxQkFBcUIsRUFBRSxZQUFZO0FBQzdDLE1BQUksTUFBTSxHQUFHLE1BQU0sQ0FBQyxNQUFNLEdBQUcsWUFBWSxDQUFDLE1BQU0sSUFBSSxFQUFFLENBQUM7QUFDdkQsUUFBTSxDQUFDLFlBQVksR0FBRyxBQUFDLE1BQU0sS0FBSyxRQUFRLEdBQ3pDLEVBQUUsU0FBUyxFQUFFLEtBQUssRUFBRSxHQUFHLEFBQUMsTUFBTSxLQUFLLFdBQVcsR0FDOUMsRUFBRSxTQUFTLEVBQUUsSUFBSSxFQUFFLEdBQUcsRUFBRSxDQUFDO0VBQzFCLENBQUMsQ0FBQzs7QUFFSCxPQUFNLENBQUMsT0FBTyxHQUFHLFlBQVk7QUFDNUIsTUFBSSxPQUFPLEdBQUc7QUFDYixRQUFLLEVBQUUsTUFBTSxDQUFDLE9BQU8sQ0FBQyxJQUFJLEVBQUU7QUFDNUIsWUFBUyxFQUFFLEtBQUs7R0FDaEIsQ0FBQzs7QUFFRixTQUFPLENBQUMsR0FBRyxDQUFDLFVBQVUsQ0FBQyxjQUFjLEVBQUUsQ0FBQyxDQUFDOztBQUV6QyxNQUFJLENBQUMsT0FBTyxDQUFDLEtBQUssRUFBRTtBQUNuQixVQUFPO0dBQ1A7O0FBRUQsUUFBTSxDQUFDLE1BQU0sR0FBRyxJQUFJLENBQUM7QUFDckIsT0FBSyxDQUFDLE1BQU0sQ0FBQyxPQUFPLENBQUMsQ0FDbkIsSUFBSSxDQUFDLFNBQVMsT0FBTyxHQUFHO0FBQ3hCLFNBQU0sQ0FBQyxPQUFPLEdBQUcsRUFBRSxDQUFDO0dBQ3BCLENBQUMsV0FDTSxDQUFDLFlBQVk7QUFDcEIsU0FBTSxDQUFDLE1BQU0sR0FBRyxLQUFLLENBQUM7R0FDdEIsQ0FBQyxDQUFDO0VBQ0osQ0FBQzs7QUFFRixPQUFNLENBQUMsUUFBUSxHQUFHLFVBQVUsSUFBSSxFQUFFO0FBQ2pDLFFBQU0sQ0FBQyxVQUFVLEdBQUcsSUFBSSxDQUFDOztBQUV6QixRQUFNLENBQUMsWUFBWSxHQUFHLE9BQU8sQ0FBQyxNQUFNLENBQUMsRUFBRSxFQUFFLElBQUksQ0FBQyxDQUFDO0VBQy9DLENBQUM7O0FBRUYsT0FBTSxDQUFDLFNBQVMsR0FBRyxVQUFVLElBQUksRUFBRSxLQUFLLEVBQUU7OztBQUd6QyxNQUFJLEtBQUssS0FBSyxNQUFNLElBQUksTUFBTSxDQUFDLFNBQVMsS0FBSyxRQUFRLEVBQUU7QUFDdEQsU0FBTSxDQUFDLFNBQVMsR0FBRyxJQUFJLENBQUM7QUFDeEIsVUFBTztHQUNQOztBQUVELFFBQU0sQ0FBQyxTQUFTLEdBQUcsS0FBSyxDQUFDOztBQUV6QixNQUFJLE1BQU0sQ0FBQyxRQUFRLEVBQUU7O0FBRXBCLFNBQU0sQ0FBQyxRQUFRLEdBQUcsSUFBSSxDQUFDO0FBQ3ZCLFVBQU87R0FDUDs7QUFFRCxNQUFJLENBQUMsS0FBSyxHQUFHLElBQUksQ0FBQyxLQUFLLENBQUMsSUFBSSxFQUFFLENBQUM7O0FBRS9CLE1BQUksSUFBSSxDQUFDLEtBQUssS0FBSyxNQUFNLENBQUMsWUFBWSxDQUFDLEtBQUssRUFBRTtBQUM3QyxTQUFNLENBQUMsVUFBVSxHQUFHLElBQUksQ0FBQztBQUN6QixVQUFPO0dBQ1A7O0FBRUQsT0FBSyxDQUFDLElBQUksQ0FBQyxLQUFLLEdBQUcsS0FBSyxHQUFHLFFBQVEsQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUN4QyxJQUFJLENBQUMsU0FBUyxPQUFPLEdBQUcsRUFBRSxFQUFFLFNBQVMsS0FBSyxHQUFHO0FBQzdDLE9BQUksQ0FBQyxLQUFLLEdBQUcsTUFBTSxDQUFDLFlBQVksQ0FBQyxLQUFLLENBQUM7R0FDdkMsQ0FBQyxXQUNNLENBQUMsWUFBWTtBQUNwQixTQUFNLENBQUMsVUFBVSxHQUFHLElBQUksQ0FBQztHQUN6QixDQUFDLENBQUM7RUFDSixDQUFDOztBQUVGLE9BQU0sQ0FBQyxXQUFXLEdBQUcsVUFBVSxJQUFJLEVBQUU7QUFDcEMsT0FBSyxDQUFDLEtBQUssQ0FBQyxPQUFPLENBQUMsSUFBSSxDQUFDLENBQUMsR0FBRyxNQUFNLENBQUMsWUFBWSxDQUFDO0FBQ2pELFFBQU0sQ0FBQyxVQUFVLEdBQUcsSUFBSSxDQUFDO0FBQ3pCLFFBQU0sQ0FBQyxZQUFZLEdBQUcsSUFBSSxDQUFDO0FBQzNCLFFBQU0sQ0FBQyxRQUFRLEdBQUcsSUFBSSxDQUFDO0VBQ3ZCLENBQUM7O0FBRUYsT0FBTSxDQUFDLFVBQVUsR0FBRyxVQUFVLElBQUksRUFBRTtBQUNuQyxPQUFLLFVBQU8sQ0FBQyxJQUFJLENBQUMsQ0FBQztFQUNuQixDQUFDOztBQUVGLE9BQU0sQ0FBQyxRQUFRLEdBQUcsVUFBVSxJQUFJLEVBQUU7QUFDakMsT0FBSyxDQUFDLEdBQUcsQ0FBQyxJQUFJLENBQUMsQ0FBQztFQUNoQixDQUFDOztBQUVGLE9BQU0sQ0FBQyxlQUFlLEdBQUcsVUFBVSxJQUFJLEVBQUUsU0FBUyxFQUFFO0FBQ25ELE1BQUksT0FBTyxDQUFDLFNBQVMsQ0FBQyxTQUFTLENBQUMsRUFBRTtBQUNqQyxPQUFJLENBQUMsU0FBUyxHQUFHLFNBQVMsQ0FBQztHQUMzQjtBQUNELE9BQUssQ0FBQyxHQUFHLENBQUMsSUFBSSxFQUFFLEtBQUssQ0FBQyxPQUFPLENBQUMsSUFBSSxDQUFDLENBQUMsQ0FDbEMsSUFBSSxDQUFDLFNBQVMsT0FBTyxHQUFHLEVBQUUsRUFBRSxTQUFTLEtBQUssR0FBRztBQUM3QyxPQUFJLENBQUMsU0FBUyxHQUFHLENBQUMsSUFBSSxDQUFDLFNBQVMsQ0FBQztHQUNqQyxDQUFDLENBQUM7RUFDSixDQUFDOztBQUVGLE9BQU0sQ0FBQyxtQkFBbUIsR0FBRyxZQUFZO0FBQ3hDLE9BQUssQ0FBQyxjQUFjLEVBQUUsQ0FBQztFQUN2QixDQUFDOztBQUVGLE9BQU0sQ0FBQyxPQUFPLEdBQUcsVUFBVSxTQUFTLEVBQUU7QUFDckMsT0FBSyxDQUFDLE9BQU8sQ0FBQyxVQUFVLElBQUksRUFBRTtBQUM3QixPQUFJLElBQUksQ0FBQyxTQUFTLEtBQUssU0FBUyxFQUFFO0FBQ2pDLFVBQU0sQ0FBQyxlQUFlLENBQUMsSUFBSSxFQUFFLFNBQVMsQ0FBQyxDQUFDO0lBQ3hDO0dBQ0QsQ0FBQyxDQUFDO0VBQ0gsQ0FBQztDQUNGLENBQUMsQ0FBQzs7SUFFRSxVQUFVO0FBRUQsVUFGVCxVQUFVLENBRUEsS0FBSyxFQUNqQjt3QkFIRSxVQUFVOztBQUlSLE1BQUksQ0FBQyxLQUFLLEdBQUcsS0FBSyxDQUFDO0VBQ3RCOztjQUxDLFVBQVU7O1NBV0UsMEJBQUc7QUFDYixVQUFPLElBQUksQ0FBQyxLQUFLLENBQUMsR0FBRyxDQUFDLHlDQUF5QyxDQUFDLENBQUMsSUFBSSxDQUFDLFVBQUEsTUFBTTtXQUFJLE1BQU0sQ0FBQyxJQUFJO0lBQUEsQ0FBQyxDQUFDO0dBQ2hHOzs7U0FFYSwwQkFBRTtBQUNaLFVBQU8sSUFBSSxDQUFDLEtBQUssQ0FBQyxHQUFHLENBQUMsa0JBQWtCLENBQUMsQ0FBQyxJQUFJLENBQUMsVUFBQSxNQUFNO1dBQUksTUFBTSxDQUFDLElBQUk7SUFBQSxDQUFFLENBQUM7R0FDMUU7OztTQUVlLDRCQUFFO0FBQ2QsVUFBTyxJQUFJLENBQUMsS0FBSyxDQUFDLEdBQUcsQ0FBQyxvQkFBb0IsQ0FBQyxDQUFDLElBQUksQ0FBQyxVQUFBLE1BQU07V0FBSSxNQUFNLENBQUMsSUFBSTtJQUFBLENBQUUsQ0FBQztHQUM1RTs7O1NBRVcsc0JBQUMsTUFBTSxFQUFFLFVBQVUsRUFBQztBQUM1QixVQUFPLElBQUksQ0FBQyxLQUFLLENBQUMsR0FBRyxvQkFBa0IsTUFBTSxFQUFJLEVBQUMsTUFBTSxFQUFFLE1BQU0sRUFBRSxJQUFJLEVBQUUsVUFBVSxFQUFDLENBQUMsQ0FBQztHQUN4Rjs7O1NBRVcsc0JBQUMsTUFBTSxFQUFDO0FBQ2hCLFVBQU8sSUFBSSxDQUFDLEtBQUssQ0FBQyxHQUFHLHdCQUFzQixNQUFNLEVBQUcsRUFBRSxDQUFDLENBQUM7R0FDM0Q7OztTQUVnQiwyQkFBQyxLQUFLLEVBQUM7QUFDcEIsVUFBTyxJQUFJLENBQUMsS0FBSyxDQUFDLEdBQUcsc0JBQW9CLEtBQUssQ0FBRyxDQUFDLElBQUksQ0FBQyxVQUFBLE1BQU07V0FBSyxNQUFNLENBQUMsSUFBSTtJQUFBLENBQUUsQ0FBQztHQUNuRjs7O1NBRU0saUJBQUMsSUFBSSxFQUFDO0FBQ1QsVUFBTyxJQUFJLENBQUMsS0FBSyxDQUFDLElBQUksQ0FBQyxZQUFZLEVBQUUsSUFBSSxDQUFDLENBQUM7R0FDOUM7OztTQTlCZ0Isb0JBQUMsS0FBSyxFQUFFO0FBQ3JCLFVBQU8sSUFBSSxVQUFVLENBQUMsS0FBSyxDQUFDLENBQUM7R0FDaEM7OztRQVRDLFVBQVU7OztBQXdDaEIsVUFBVSxDQUFDLFVBQVUsQ0FBQyxPQUFPLEdBQUcsQ0FBQyxPQUFPLENBQUMsQ0FBQzs7QUFFMUMsT0FBTyxDQUFDLE1BQU0sQ0FBQyxVQUFVLENBQUMsQ0FDdkIsT0FBTyxDQUFDLFlBQVksRUFBRSxVQUFVLENBQUMsVUFBVSxDQUFDLENBQUM7Ozs7Ozs7Ozs7QUFVaEQsT0FBTyxDQUFDLE1BQU0sQ0FBQyxVQUFVLENBQUMsQ0FDeEIsT0FBTyxDQUFDLGFBQWEsRUFBRSxVQUFVLEtBQUssRUFBRSxTQUFTLEVBQUU7QUFDbkQsYUFBWSxDQUFDOzs7O0FBSWIsUUFBTyxLQUFLLENBQUMsR0FBRyxDQUFDLE1BQU0sQ0FBQyxDQUN0QixJQUFJLENBQUMsWUFBWTtBQUNqQixTQUFPLFNBQVMsQ0FBQyxHQUFHLENBQUMsS0FBSyxDQUFDLENBQUM7RUFDNUIsRUFBRSxZQUFZO0FBQ2QsU0FBTyxTQUFTLENBQUMsR0FBRyxDQUFDLGNBQWMsQ0FBQyxDQUFDO0VBQ3JDLENBQUMsQ0FBQztDQUNKLENBQUMsQ0FFRCxPQUFPLENBQUMsS0FBSyxFQUFFLFVBQVUsS0FBSyxFQUFFO0FBQ2hDLGFBQVksQ0FBQzs7QUFFYixLQUFJLEtBQUssR0FBRztBQUNYLE9BQUssRUFBRSxFQUFFOztBQUVULGdCQUFjLEVBQUUsMEJBQVk7QUFDM0IsT0FBSSxhQUFhLEdBQUcsS0FBSyxDQUFDLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQyxDQUFDLENBQUM7O0FBRXpDLE9BQUksYUFBYSxHQUFHLEVBQUUsQ0FBQztBQUN2QixPQUFJLGVBQWUsR0FBRyxFQUFFLENBQUM7QUFDekIsUUFBSyxDQUFDLEtBQUssQ0FBQyxPQUFPLENBQUMsVUFBVSxJQUFJLEVBQUU7QUFDbkMsUUFBSSxJQUFJLENBQUMsU0FBUyxFQUFFO0FBQ25CLGtCQUFhLENBQUMsSUFBSSxDQUFDLElBQUksQ0FBQyxDQUFDO0tBQ3pCLE1BQU07QUFDTixvQkFBZSxDQUFDLElBQUksQ0FBQyxJQUFJLENBQUMsQ0FBQztLQUMzQjtJQUNELENBQUMsQ0FBQzs7QUFFSCxVQUFPLENBQUMsSUFBSSxDQUFDLGVBQWUsRUFBRSxLQUFLLENBQUMsS0FBSyxDQUFDLENBQUM7O0FBRTNDLFVBQU8sS0FBSyxVQUFPLENBQUMsWUFBWSxDQUFDLENBQy9CLElBQUksQ0FBQyxTQUFTLE9BQU8sR0FBRztBQUN4QixXQUFPLEtBQUssQ0FBQyxLQUFLLENBQUM7SUFDbkIsRUFBRSxTQUFTLEtBQUssR0FBRztBQUNuQixXQUFPLENBQUMsSUFBSSxDQUFDLGFBQWEsRUFBRSxLQUFLLENBQUMsS0FBSyxDQUFDLENBQUM7QUFDekMsV0FBTyxhQUFhLENBQUM7SUFDckIsQ0FBQyxDQUFDO0dBQ0o7O0FBRUQsWUFBUSxpQkFBVSxJQUFJLEVBQUU7QUFDdkIsT0FBSSxhQUFhLEdBQUcsS0FBSyxDQUFDLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQyxDQUFDLENBQUM7O0FBRXpDLFFBQUssQ0FBQyxLQUFLLENBQUMsTUFBTSxDQUFDLEtBQUssQ0FBQyxLQUFLLENBQUMsT0FBTyxDQUFDLElBQUksQ0FBQyxFQUFFLENBQUMsQ0FBQyxDQUFDOztBQUVqRCxVQUFPLEtBQUssVUFBTyxDQUFDLGFBQWEsR0FBRyxJQUFJLENBQUMsRUFBRSxDQUFDLENBQzFDLElBQUksQ0FBQyxTQUFTLE9BQU8sR0FBRztBQUN4QixXQUFPLEtBQUssQ0FBQyxLQUFLLENBQUM7SUFDbkIsRUFBRSxTQUFTLEtBQUssR0FBRztBQUNuQixXQUFPLENBQUMsSUFBSSxDQUFDLGFBQWEsRUFBRSxLQUFLLENBQUMsS0FBSyxDQUFDLENBQUM7QUFDekMsV0FBTyxhQUFhLENBQUM7SUFDckIsQ0FBQyxDQUFDO0dBQ0o7O0FBRUQsS0FBRyxFQUFFLGVBQVk7QUFDaEIsVUFBTyxLQUFLLENBQUMsR0FBRyxDQUFDLFlBQVksQ0FBQyxDQUM1QixJQUFJLENBQUMsVUFBVSxJQUFJLEVBQUU7QUFDckIsV0FBTyxDQUFDLElBQUksQ0FBQyxJQUFJLENBQUMsSUFBSSxFQUFFLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQztBQUNyQyxXQUFPLEtBQUssQ0FBQyxLQUFLLENBQUM7SUFDbkIsQ0FBQyxDQUFDO0dBQ0o7O0FBRUQsUUFBTSxFQUFFLGdCQUFVLElBQUksRUFBRTtBQUN2QixPQUFJLGFBQWEsR0FBRyxLQUFLLENBQUMsS0FBSyxDQUFDLEtBQUssQ0FBQyxDQUFDLENBQUMsQ0FBQzs7QUFFekMsVUFBTyxLQUFLLENBQUMsSUFBSSxDQUFDLFlBQVksRUFBRSxJQUFJLENBQUMsQ0FDbkMsSUFBSSxDQUFDLFNBQVMsT0FBTyxDQUFDLElBQUksRUFBRTtBQUM1QixRQUFJLENBQUMsRUFBRSxHQUFHLElBQUksQ0FBQyxJQUFJLENBQUMsRUFBRSxDQUFDO0FBQ3ZCLFNBQUssQ0FBQyxLQUFLLENBQUMsSUFBSSxDQUFDLElBQUksQ0FBQyxDQUFDO0FBQ3ZCLFdBQU8sS0FBSyxDQUFDLEtBQUssQ0FBQztJQUNuQixFQUFFLFNBQVMsS0FBSyxHQUFHO0FBQ25CLFdBQU8sQ0FBQyxJQUFJLENBQUMsYUFBYSxFQUFFLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQztBQUN6QyxXQUFPLEtBQUssQ0FBQyxLQUFLLENBQUM7SUFDbkIsQ0FBQyxDQUFDO0dBQ0o7O0FBRUQsS0FBRyxFQUFFLGFBQVUsSUFBSSxFQUFFO0FBQ3BCLE9BQUksYUFBYSxHQUFHLEtBQUssQ0FBQyxLQUFLLENBQUMsS0FBSyxDQUFDLENBQUMsQ0FBQyxDQUFDOztBQUV6QyxVQUFPLEtBQUssQ0FBQyxHQUFHLENBQUMsYUFBYSxHQUFHLElBQUksQ0FBQyxFQUFFLEVBQUUsSUFBSSxDQUFDLENBQzdDLElBQUksQ0FBQyxTQUFTLE9BQU8sR0FBRztBQUN4QixXQUFPLEtBQUssQ0FBQyxLQUFLLENBQUM7SUFDbkIsRUFBRSxTQUFTLEtBQUssR0FBRztBQUNuQixXQUFPLENBQUMsSUFBSSxDQUFDLGFBQWEsRUFBRSxLQUFLLENBQUMsS0FBSyxDQUFDLENBQUM7QUFDekMsV0FBTyxhQUFhLENBQUM7SUFDckIsQ0FBQyxDQUFDO0dBQ0o7RUFDRCxDQUFDOztBQUVGLFFBQU8sS0FBSyxDQUFDO0NBQ2IsQ0FBQyxDQUVELE9BQU8sQ0FBQyxjQUFjLEVBQUUsVUFBVSxFQUFFLEVBQUU7QUFDdEMsYUFBWSxDQUFDOztBQUViLEtBQUksVUFBVSxHQUFHLGlCQUFpQixDQUFDOztBQUVuQyxLQUFJLEtBQUssR0FBRztBQUNYLE9BQUssRUFBRSxFQUFFOztBQUVULHNCQUFvQixFQUFFLGdDQUFZO0FBQ2pDLFVBQU8sSUFBSSxDQUFDLEtBQUssQ0FBQyxZQUFZLENBQUMsT0FBTyxDQUFDLFVBQVUsQ0FBQyxJQUFJLElBQUksQ0FBQyxDQUFDO0dBQzVEOztBQUVELHFCQUFtQixFQUFFLDZCQUFVLEtBQUssRUFBRTtBQUNyQyxlQUFZLENBQUMsT0FBTyxDQUFDLFVBQVUsRUFBRSxJQUFJLENBQUMsU0FBUyxDQUFDLEtBQUssQ0FBQyxDQUFDLENBQUM7R0FDeEQ7O0FBRUQsZ0JBQWMsRUFBRSwwQkFBWTtBQUMzQixPQUFJLFFBQVEsR0FBRyxFQUFFLENBQUMsS0FBSyxFQUFFLENBQUM7O0FBRTFCLE9BQUksYUFBYSxHQUFHLEVBQUUsQ0FBQztBQUN2QixPQUFJLGVBQWUsR0FBRyxFQUFFLENBQUM7QUFDekIsUUFBSyxDQUFDLEtBQUssQ0FBQyxPQUFPLENBQUMsVUFBVSxJQUFJLEVBQUU7QUFDbkMsUUFBSSxJQUFJLENBQUMsU0FBUyxFQUFFO0FBQ25CLGtCQUFhLENBQUMsSUFBSSxDQUFDLElBQUksQ0FBQyxDQUFDO0tBQ3pCLE1BQU07QUFDTixvQkFBZSxDQUFDLElBQUksQ0FBQyxJQUFJLENBQUMsQ0FBQztLQUMzQjtJQUNELENBQUMsQ0FBQzs7QUFFSCxVQUFPLENBQUMsSUFBSSxDQUFDLGVBQWUsRUFBRSxLQUFLLENBQUMsS0FBSyxDQUFDLENBQUM7O0FBRTNDLFFBQUssQ0FBQyxtQkFBbUIsQ0FBQyxLQUFLLENBQUMsS0FBSyxDQUFDLENBQUM7QUFDdkMsV0FBUSxDQUFDLE9BQU8sQ0FBQyxLQUFLLENBQUMsS0FBSyxDQUFDLENBQUM7O0FBRTlCLFVBQU8sUUFBUSxDQUFDLE9BQU8sQ0FBQztHQUN4Qjs7QUFFRCxZQUFRLGlCQUFVLElBQUksRUFBRTtBQUN2QixPQUFJLFFBQVEsR0FBRyxFQUFFLENBQUMsS0FBSyxFQUFFLENBQUM7O0FBRTFCLFFBQUssQ0FBQyxLQUFLLENBQUMsTUFBTSxDQUFDLEtBQUssQ0FBQyxLQUFLLENBQUMsT0FBTyxDQUFDLElBQUksQ0FBQyxFQUFFLENBQUMsQ0FBQyxDQUFDOztBQUVqRCxRQUFLLENBQUMsbUJBQW1CLENBQUMsS0FBSyxDQUFDLEtBQUssQ0FBQyxDQUFDO0FBQ3ZDLFdBQVEsQ0FBQyxPQUFPLENBQUMsS0FBSyxDQUFDLEtBQUssQ0FBQyxDQUFDOztBQUU5QixVQUFPLFFBQVEsQ0FBQyxPQUFPLENBQUM7R0FDeEI7O0FBRUQsS0FBRyxFQUFFLGVBQVk7QUFDaEIsT0FBSSxRQUFRLEdBQUcsRUFBRSxDQUFDLEtBQUssRUFBRSxDQUFDOztBQUUxQixVQUFPLENBQUMsSUFBSSxDQUFDLEtBQUssQ0FBQyxvQkFBb0IsRUFBRSxFQUFFLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQztBQUN4RCxXQUFRLENBQUMsT0FBTyxDQUFDLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQzs7QUFFOUIsVUFBTyxRQUFRLENBQUMsT0FBTyxDQUFDO0dBQ3hCOztBQUVELFFBQU0sRUFBRSxnQkFBVSxJQUFJLEVBQUU7QUFDdkIsT0FBSSxRQUFRLEdBQUcsRUFBRSxDQUFDLEtBQUssRUFBRSxDQUFDOztBQUUxQixRQUFLLENBQUMsS0FBSyxDQUFDLElBQUksQ0FBQyxJQUFJLENBQUMsQ0FBQzs7QUFFdkIsUUFBSyxDQUFDLG1CQUFtQixDQUFDLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQztBQUN2QyxXQUFRLENBQUMsT0FBTyxDQUFDLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQzs7QUFFOUIsVUFBTyxRQUFRLENBQUMsT0FBTyxDQUFDO0dBQ3hCOztBQUVELEtBQUcsRUFBRSxhQUFVLElBQUksRUFBRSxLQUFLLEVBQUU7QUFDM0IsT0FBSSxRQUFRLEdBQUcsRUFBRSxDQUFDLEtBQUssRUFBRSxDQUFDOztBQUUxQixRQUFLLENBQUMsS0FBSyxDQUFDLEtBQUssQ0FBQyxHQUFHLElBQUksQ0FBQzs7QUFFMUIsUUFBSyxDQUFDLG1CQUFtQixDQUFDLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQztBQUN2QyxXQUFRLENBQUMsT0FBTyxDQUFDLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQzs7QUFFOUIsVUFBTyxRQUFRLENBQUMsT0FBTyxDQUFDO0dBQ3hCO0VBQ0QsQ0FBQzs7QUFFRixRQUFPLEtBQUssQ0FBQztDQUNiLENBQUMsQ0FBQzs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7QUFrREosT0FBTyxDQUFDLE1BQU0sQ0FBQyxVQUFVLENBQUMsQ0FDeEIsU0FBUyxDQUFDLFlBQVksRUFBRSxZQUFZO0FBQ3BDLGFBQVksQ0FBQzs7QUFFYixLQUFJLFVBQVUsR0FBRyxFQUFFLENBQUM7O0FBRXBCLFFBQU8sVUFBVSxLQUFLLEVBQUUsSUFBSSxFQUFFLEtBQUssRUFBRTtBQUNwQyxNQUFJLENBQUMsSUFBSSxDQUFDLFNBQVMsRUFBRSxVQUFVLEtBQUssRUFBRTtBQUNyQyxPQUFJLEtBQUssQ0FBQyxPQUFPLEtBQUssVUFBVSxFQUFFO0FBQ2pDLFNBQUssQ0FBQyxNQUFNLENBQUMsS0FBSyxDQUFDLFVBQVUsQ0FBQyxDQUFDO0lBQy9CO0dBQ0QsQ0FBQyxDQUFDOztBQUVILE9BQUssQ0FBQyxHQUFHLENBQUMsVUFBVSxFQUFFLFlBQVk7QUFDakMsT0FBSSxDQUFDLE1BQU0sQ0FBQyxTQUFTLENBQUMsQ0FBQztHQUN2QixDQUFDLENBQUM7RUFDSCxDQUFDO0NBQ0YsQ0FBQyxDQUFDOzs7Ozs7OztBQVFKLE9BQU8sQ0FBQyxNQUFNLENBQUMsVUFBVSxDQUFDLENBQ3hCLFNBQVMsQ0FBQyxXQUFXLEVBQUUsU0FBUyxTQUFTLENBQUMsUUFBUSxFQUFFO0FBQ3BELGFBQVksQ0FBQzs7QUFFYixRQUFPLFVBQVUsS0FBSyxFQUFFLElBQUksRUFBRSxLQUFLLEVBQUU7QUFDcEMsT0FBSyxDQUFDLE1BQU0sQ0FBQyxLQUFLLENBQUMsU0FBUyxFQUFFLFVBQVUsTUFBTSxFQUFFO0FBQy9DLE9BQUksTUFBTSxFQUFFO0FBQ1gsWUFBUSxDQUFDLFlBQVk7QUFDcEIsU0FBSSxDQUFDLENBQUMsQ0FBQyxDQUFDLEtBQUssRUFBRSxDQUFDO0tBQ2hCLEVBQUUsQ0FBQyxFQUFFLEtBQUssQ0FBQyxDQUFDO0lBQ2I7R0FDRCxDQUFDLENBQUM7RUFDSCxDQUFDO0NBQ0YsQ0FBQyxDQUFDIiwiZmlsZSI6InVuZGVmaW5lZCIsInNvdXJjZXNDb250ZW50IjpbImNvbnN0IG1vZHVsZU5hbWUgPSBcImNhcmF2YW5BZG1pblVJXCI7XHJcbi8qZ2xvYmFsIGFuZ3VsYXIgKi9cblxuLyoqXG4gKiBUaGUgbWFpbiBUb2RvTVZDIGFwcCBtb2R1bGVcbiAqXG4gKiBAdHlwZSB7YW5ndWxhci5Nb2R1bGV9XG4gKi9cbmFuZ3VsYXIubW9kdWxlKG1vZHVsZU5hbWUsIFsnbmdSb3V0ZSddKVxuXHQuY29uZmlnKGZ1bmN0aW9uICgkcm91dGVQcm92aWRlcikge1xuXHRcdCd1c2Ugc3RyaWN0JztcblxuXHRcdHZhciByb3V0ZUNvbmZpZyA9IHtcblx0XHRcdGNvbnRyb2xsZXI6ICdUb2RvQ3RybCcsXG5cdFx0XHR0ZW1wbGF0ZVVybDogJ2NhcmF2YW5BZG1pblVJLWluZGV4Lmh0bWwnLFxuXHRcdFx0cmVzb2x2ZToge1xuXHRcdFx0XHRzdG9yZTogZnVuY3Rpb24gKHRvZG9TdG9yYWdlKSB7XG5cdFx0XHRcdFx0Ly8gR2V0IHRoZSBjb3JyZWN0IG1vZHVsZSAoQVBJIG9yIGxvY2FsU3RvcmFnZSkuXG5cdFx0XHRcdFx0cmV0dXJuIHRvZG9TdG9yYWdlLnRoZW4oZnVuY3Rpb24gKG1vZHVsZSkge1xuXHRcdFx0XHRcdFx0bW9kdWxlLmdldCgpOyAvLyBGZXRjaCB0aGUgdG9kbyByZWNvcmRzIGluIHRoZSBiYWNrZ3JvdW5kLlxuXHRcdFx0XHRcdFx0cmV0dXJuIG1vZHVsZTtcblx0XHRcdFx0XHR9KTtcblx0XHRcdFx0fVxuXHRcdFx0fVxuXHRcdH07XG5cblx0XHQkcm91dGVQcm92aWRlclxuXHRcdFx0LndoZW4oJy8nLCByb3V0ZUNvbmZpZylcblx0XHRcdC53aGVuKCcvOnN0YXR1cycsIHJvdXRlQ29uZmlnKVxuXHRcdFx0Lm90aGVyd2lzZSh7XG5cdFx0XHRcdHJlZGlyZWN0VG86ICcvJ1xuXHRcdFx0fSk7XG5cdH0pO1xuXHJcbi8qZ2xvYmFsIGFuZ3VsYXIgKi9cblxuLyoqXG4gKiBUaGUgbWFpbiBjb250cm9sbGVyIGZvciB0aGUgYXBwLiBUaGUgY29udHJvbGxlcjpcbiAqIC0gcmV0cmlldmVzIGFuZCBwZXJzaXN0cyB0aGUgbW9kZWwgdmlhIHRoZSB0b2RvU3RvcmFnZSBzZXJ2aWNlXG4gKiAtIGV4cG9zZXMgdGhlIG1vZGVsIHRvIHRoZSB0ZW1wbGF0ZSBhbmQgcHJvdmlkZXMgZXZlbnQgaGFuZGxlcnNcbiAqL1xuYW5ndWxhci5tb2R1bGUobW9kdWxlTmFtZSlcblx0LmNvbnRyb2xsZXIoJ1RvZG9DdHJsJywgZnVuY3Rpb24gVG9kb0N0cmwoJHNjb3BlLCAkcm91dGVQYXJhbXMsICRmaWx0ZXIsIHN0b3JlLCBsb2dTZXJ2aWNlKSB7XG5cdFx0J3VzZSBzdHJpY3QnO1xuXG5cdFx0dmFyIHRvZG9zID0gJHNjb3BlLnRvZG9zID0gc3RvcmUudG9kb3M7XG5cblx0XHQkc2NvcGUubmV3VG9kbyA9ICcnO1xuXHRcdCRzY29wZS5lZGl0ZWRUb2RvID0gbnVsbDtcblxuXHRcdCRzY29wZS4kd2F0Y2goJ3RvZG9zJywgZnVuY3Rpb24gKCkge1xuXHRcdFx0JHNjb3BlLnJlbWFpbmluZ0NvdW50ID0gJGZpbHRlcignZmlsdGVyJykodG9kb3MsIHsgY29tcGxldGVkOiBmYWxzZSB9KS5sZW5ndGg7XG5cdFx0XHQkc2NvcGUuY29tcGxldGVkQ291bnQgPSB0b2Rvcy5sZW5ndGggLSAkc2NvcGUucmVtYWluaW5nQ291bnQ7XG5cdFx0XHQkc2NvcGUuYWxsQ2hlY2tlZCA9ICEkc2NvcGUucmVtYWluaW5nQ291bnQ7XG5cdFx0fSwgdHJ1ZSk7XG5cblx0XHQvLyBNb25pdG9yIHRoZSBjdXJyZW50IHJvdXRlIGZvciBjaGFuZ2VzIGFuZCBhZGp1c3QgdGhlIGZpbHRlciBhY2NvcmRpbmdseS5cblx0XHQkc2NvcGUuJG9uKCckcm91dGVDaGFuZ2VTdWNjZXNzJywgZnVuY3Rpb24gKCkge1xuXHRcdFx0dmFyIHN0YXR1cyA9ICRzY29wZS5zdGF0dXMgPSAkcm91dGVQYXJhbXMuc3RhdHVzIHx8ICcnO1xuXHRcdFx0JHNjb3BlLnN0YXR1c0ZpbHRlciA9IChzdGF0dXMgPT09ICdhY3RpdmUnKSA/XG5cdFx0XHRcdHsgY29tcGxldGVkOiBmYWxzZSB9IDogKHN0YXR1cyA9PT0gJ2NvbXBsZXRlZCcpID9cblx0XHRcdFx0eyBjb21wbGV0ZWQ6IHRydWUgfSA6IHt9O1xuXHRcdH0pO1xuXG5cdFx0JHNjb3BlLmFkZFRvZG8gPSBmdW5jdGlvbiAoKSB7XG5cdFx0XHR2YXIgbmV3VG9kbyA9IHtcblx0XHRcdFx0dGl0bGU6ICRzY29wZS5uZXdUb2RvLnRyaW0oKSxcblx0XHRcdFx0Y29tcGxldGVkOiBmYWxzZVxuXHRcdFx0fTtcblxuXHRcdFx0Y29uc29sZS5sb2cobG9nU2VydmljZS5nZXRBbGxTZXR0aW5ncygpKTtcblxuXHRcdFx0aWYgKCFuZXdUb2RvLnRpdGxlKSB7XG5cdFx0XHRcdHJldHVybjtcblx0XHRcdH1cblxuXHRcdFx0JHNjb3BlLnNhdmluZyA9IHRydWU7XG5cdFx0XHRzdG9yZS5pbnNlcnQobmV3VG9kbylcblx0XHRcdFx0LnRoZW4oZnVuY3Rpb24gc3VjY2VzcygpIHtcblx0XHRcdFx0XHQkc2NvcGUubmV3VG9kbyA9ICcnO1xuXHRcdFx0XHR9KVxuXHRcdFx0XHQuZmluYWxseShmdW5jdGlvbiAoKSB7XG5cdFx0XHRcdFx0JHNjb3BlLnNhdmluZyA9IGZhbHNlO1xuXHRcdFx0XHR9KTtcblx0XHR9O1xuXG5cdFx0JHNjb3BlLmVkaXRUb2RvID0gZnVuY3Rpb24gKHRvZG8pIHtcblx0XHRcdCRzY29wZS5lZGl0ZWRUb2RvID0gdG9kbztcblx0XHRcdC8vIENsb25lIHRoZSBvcmlnaW5hbCB0b2RvIHRvIHJlc3RvcmUgaXQgb24gZGVtYW5kLlxuXHRcdFx0JHNjb3BlLm9yaWdpbmFsVG9kbyA9IGFuZ3VsYXIuZXh0ZW5kKHt9LCB0b2RvKTtcblx0XHR9O1xuXG5cdFx0JHNjb3BlLnNhdmVFZGl0cyA9IGZ1bmN0aW9uICh0b2RvLCBldmVudCkge1xuXHRcdFx0Ly8gQmx1ciBldmVudHMgYXJlIGF1dG9tYXRpY2FsbHkgdHJpZ2dlcmVkIGFmdGVyIHRoZSBmb3JtIHN1Ym1pdCBldmVudC5cblx0XHRcdC8vIFRoaXMgZG9lcyBzb21lIHVuZm9ydHVuYXRlIGxvZ2ljIGhhbmRsaW5nIHRvIHByZXZlbnQgc2F2aW5nIHR3aWNlLlxuXHRcdFx0aWYgKGV2ZW50ID09PSAnYmx1cicgJiYgJHNjb3BlLnNhdmVFdmVudCA9PT0gJ3N1Ym1pdCcpIHtcblx0XHRcdFx0JHNjb3BlLnNhdmVFdmVudCA9IG51bGw7XG5cdFx0XHRcdHJldHVybjtcblx0XHRcdH1cblxuXHRcdFx0JHNjb3BlLnNhdmVFdmVudCA9IGV2ZW50O1xuXG5cdFx0XHRpZiAoJHNjb3BlLnJldmVydGVkKSB7XG5cdFx0XHRcdC8vIFRvZG8gZWRpdHMgd2VyZSByZXZlcnRlZC0tIGRvbid0IHNhdmUuXG5cdFx0XHRcdCRzY29wZS5yZXZlcnRlZCA9IG51bGw7XG5cdFx0XHRcdHJldHVybjtcblx0XHRcdH1cblxuXHRcdFx0dG9kby50aXRsZSA9IHRvZG8udGl0bGUudHJpbSgpO1xuXG5cdFx0XHRpZiAodG9kby50aXRsZSA9PT0gJHNjb3BlLm9yaWdpbmFsVG9kby50aXRsZSkge1xuXHRcdFx0XHQkc2NvcGUuZWRpdGVkVG9kbyA9IG51bGw7XG5cdFx0XHRcdHJldHVybjtcblx0XHRcdH1cblxuXHRcdFx0c3RvcmVbdG9kby50aXRsZSA/ICdwdXQnIDogJ2RlbGV0ZSddKHRvZG8pXG5cdFx0XHRcdC50aGVuKGZ1bmN0aW9uIHN1Y2Nlc3MoKSB7fSwgZnVuY3Rpb24gZXJyb3IoKSB7XG5cdFx0XHRcdFx0dG9kby50aXRsZSA9ICRzY29wZS5vcmlnaW5hbFRvZG8udGl0bGU7XG5cdFx0XHRcdH0pXG5cdFx0XHRcdC5maW5hbGx5KGZ1bmN0aW9uICgpIHtcblx0XHRcdFx0XHQkc2NvcGUuZWRpdGVkVG9kbyA9IG51bGw7XG5cdFx0XHRcdH0pO1xuXHRcdH07XG5cblx0XHQkc2NvcGUucmV2ZXJ0RWRpdHMgPSBmdW5jdGlvbiAodG9kbykge1xuXHRcdFx0dG9kb3NbdG9kb3MuaW5kZXhPZih0b2RvKV0gPSAkc2NvcGUub3JpZ2luYWxUb2RvO1xuXHRcdFx0JHNjb3BlLmVkaXRlZFRvZG8gPSBudWxsO1xuXHRcdFx0JHNjb3BlLm9yaWdpbmFsVG9kbyA9IG51bGw7XG5cdFx0XHQkc2NvcGUucmV2ZXJ0ZWQgPSB0cnVlO1xuXHRcdH07XG5cblx0XHQkc2NvcGUucmVtb3ZlVG9kbyA9IGZ1bmN0aW9uICh0b2RvKSB7XG5cdFx0XHRzdG9yZS5kZWxldGUodG9kbyk7XG5cdFx0fTtcblxuXHRcdCRzY29wZS5zYXZlVG9kbyA9IGZ1bmN0aW9uICh0b2RvKSB7XG5cdFx0XHRzdG9yZS5wdXQodG9kbyk7XG5cdFx0fTtcblxuXHRcdCRzY29wZS50b2dnbGVDb21wbGV0ZWQgPSBmdW5jdGlvbiAodG9kbywgY29tcGxldGVkKSB7XG5cdFx0XHRpZiAoYW5ndWxhci5pc0RlZmluZWQoY29tcGxldGVkKSkge1xuXHRcdFx0XHR0b2RvLmNvbXBsZXRlZCA9IGNvbXBsZXRlZDtcblx0XHRcdH1cblx0XHRcdHN0b3JlLnB1dCh0b2RvLCB0b2Rvcy5pbmRleE9mKHRvZG8pKVxuXHRcdFx0XHQudGhlbihmdW5jdGlvbiBzdWNjZXNzKCkge30sIGZ1bmN0aW9uIGVycm9yKCkge1xuXHRcdFx0XHRcdHRvZG8uY29tcGxldGVkID0gIXRvZG8uY29tcGxldGVkO1xuXHRcdFx0XHR9KTtcblx0XHR9O1xuXG5cdFx0JHNjb3BlLmNsZWFyQ29tcGxldGVkVG9kb3MgPSBmdW5jdGlvbiAoKSB7XG5cdFx0XHRzdG9yZS5jbGVhckNvbXBsZXRlZCgpO1xuXHRcdH07XG5cblx0XHQkc2NvcGUubWFya0FsbCA9IGZ1bmN0aW9uIChjb21wbGV0ZWQpIHtcblx0XHRcdHRvZG9zLmZvckVhY2goZnVuY3Rpb24gKHRvZG8pIHtcblx0XHRcdFx0aWYgKHRvZG8uY29tcGxldGVkICE9PSBjb21wbGV0ZWQpIHtcblx0XHRcdFx0XHQkc2NvcGUudG9nZ2xlQ29tcGxldGVkKHRvZG8sIGNvbXBsZXRlZCk7XG5cdFx0XHRcdH1cblx0XHRcdH0pO1xuXHRcdH07XG5cdH0pO1xuXHJcbmNsYXNzIExvZ1NlcnZpY2Vcclxue1xyXG4gICAgY29uc3RydWN0b3IoJGh0dHApXHJcbiAgICB7XHJcbiAgICAgICAgdGhpcy4kaHR0cCA9ICRodHRwO1xyXG4gICAgfVxyXG5cclxuICAgIHN0YXRpYyBsb2dGYWN0b3J5KCRodHRwKSB7XHJcbiAgICAgICAgcmV0dXJuIG5ldyBMb2dTZXJ2aWNlKCRodHRwKTtcclxuICAgIH1cclxuXHJcbiAgICBnZXRBbGxTZXR0aW5ncygpIHtcclxuICAgICAgICByZXR1cm4gdGhpcy4kaHR0cC5nZXQoJ2h0dHBzOi8vbG9jYWxob3N0OjQ0MzAwL2xvZ2dlci9zZXR0aW5ncycpLnRoZW4ocmVzdWx0ID0+IHJlc3VsdC5kYXRhKTtcclxuICAgIH1cclxuXHJcbiAgICBnZXRBY3RpdmVCb29rcygpe1xyXG4gICAgICAgIHJldHVybiB0aGlzLiRodHRwLmdldCgnL2FwaS9hY3RpdmVCb29rcycpLnRoZW4ocmVzdWx0ID0+IHJlc3VsdC5kYXRhICk7XHJcbiAgICB9XHJcblxyXG4gICAgZ2V0QXJjaGl2ZWRCb29rcygpe1xyXG4gICAgICAgIHJldHVybiB0aGlzLiRodHRwLmdldCgnL2FwaS9hcmNoaXZlZEJvb2tzJykudGhlbihyZXN1bHQgPT4gcmVzdWx0LmRhdGEgKTtcclxuICAgIH1cclxuXHJcbiAgICBtYXJrQm9va1JlYWQoYm9va0lkLCBpc0Jvb2tSZWFkKXtcclxuICAgICAgICByZXR1cm4gdGhpcy4kaHR0cC5wdXQoYC9hcGkvbWFya1JlYWQvJHtib29rSWR9YCwge2Jvb2tJZDogYm9va0lkLCByZWFkOiBpc0Jvb2tSZWFkfSk7XHJcbiAgICB9XHJcblxyXG4gICAgYWRkVG9BcmNoaXZlKGJvb2tJZCl7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMuJGh0dHAucHV0KGAvYXBpL2FkZFRvQXJjaGl2ZS8ke2Jvb2tJZH1gLHt9KTtcclxuICAgIH1cclxuXHJcbiAgICBjaGVja0lmQm9va0V4aXN0cyh0aXRsZSl7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMuJGh0dHAuZ2V0KGAvYXBpL2Jvb2tFeGlzdHMvJHt0aXRsZX1gKS50aGVuKHJlc3VsdCA9PiAgcmVzdWx0LmRhdGEgKTtcclxuICAgIH1cclxuXHJcbiAgICBhZGRCb29rKGJvb2spe1xyXG4gICAgICAgIHJldHVybiB0aGlzLiRodHRwLnBvc3QoJy9hcGkvYm9va3MnLCBib29rKTtcclxuICAgIH1cclxufVxyXG5cclxuTG9nU2VydmljZS5sb2dGYWN0b3J5LiRpbmplY3QgPSBbJyRodHRwJ107XHJcblxyXG5hbmd1bGFyLm1vZHVsZShtb2R1bGVOYW1lKVxyXG4gIC5mYWN0b3J5KCdsb2dTZXJ2aWNlJywgTG9nU2VydmljZS5sb2dGYWN0b3J5KTtcclxuLypnbG9iYWwgYW5ndWxhciAqL1xuXG4vKipcbiAqIFNlcnZpY2VzIHRoYXQgcGVyc2lzdHMgYW5kIHJldHJpZXZlcyB0b2RvcyBmcm9tIGxvY2FsU3RvcmFnZSBvciBhIGJhY2tlbmQgQVBJXG4gKiBpZiBhdmFpbGFibGUuXG4gKlxuICogVGhleSBib3RoIGZvbGxvdyB0aGUgc2FtZSBBUEksIHJldHVybmluZyBwcm9taXNlcyBmb3IgYWxsIGNoYW5nZXMgdG8gdGhlXG4gKiBtb2RlbC5cbiAqL1xuYW5ndWxhci5tb2R1bGUobW9kdWxlTmFtZSlcblx0LmZhY3RvcnkoJ3RvZG9TdG9yYWdlJywgZnVuY3Rpb24gKCRodHRwLCAkaW5qZWN0b3IpIHtcblx0XHQndXNlIHN0cmljdCc7XG5cblx0XHQvLyBEZXRlY3QgaWYgYW4gQVBJIGJhY2tlbmQgaXMgcHJlc2VudC4gSWYgc28sIHJldHVybiB0aGUgQVBJIG1vZHVsZSwgZWxzZVxuXHRcdC8vIGhhbmQgb2ZmIHRoZSBsb2NhbFN0b3JhZ2UgYWRhcHRlclxuXHRcdHJldHVybiAkaHR0cC5nZXQoJy9hcGknKVxuXHRcdFx0LnRoZW4oZnVuY3Rpb24gKCkge1xuXHRcdFx0XHRyZXR1cm4gJGluamVjdG9yLmdldCgnYXBpJyk7XG5cdFx0XHR9LCBmdW5jdGlvbiAoKSB7XG5cdFx0XHRcdHJldHVybiAkaW5qZWN0b3IuZ2V0KCdsb2NhbFN0b3JhZ2UnKTtcblx0XHRcdH0pO1xuXHR9KVxuXG5cdC5mYWN0b3J5KCdhcGknLCBmdW5jdGlvbiAoJGh0dHApIHtcblx0XHQndXNlIHN0cmljdCc7XG5cblx0XHR2YXIgc3RvcmUgPSB7XG5cdFx0XHR0b2RvczogW10sXG5cblx0XHRcdGNsZWFyQ29tcGxldGVkOiBmdW5jdGlvbiAoKSB7XG5cdFx0XHRcdHZhciBvcmlnaW5hbFRvZG9zID0gc3RvcmUudG9kb3Muc2xpY2UoMCk7XG5cblx0XHRcdFx0dmFyIGNvbXBsZXRlVG9kb3MgPSBbXTtcblx0XHRcdFx0dmFyIGluY29tcGxldGVUb2RvcyA9IFtdO1xuXHRcdFx0XHRzdG9yZS50b2Rvcy5mb3JFYWNoKGZ1bmN0aW9uICh0b2RvKSB7XG5cdFx0XHRcdFx0aWYgKHRvZG8uY29tcGxldGVkKSB7XG5cdFx0XHRcdFx0XHRjb21wbGV0ZVRvZG9zLnB1c2godG9kbyk7XG5cdFx0XHRcdFx0fSBlbHNlIHtcblx0XHRcdFx0XHRcdGluY29tcGxldGVUb2Rvcy5wdXNoKHRvZG8pO1xuXHRcdFx0XHRcdH1cblx0XHRcdFx0fSk7XG5cblx0XHRcdFx0YW5ndWxhci5jb3B5KGluY29tcGxldGVUb2Rvcywgc3RvcmUudG9kb3MpO1xuXG5cdFx0XHRcdHJldHVybiAkaHR0cC5kZWxldGUoJy9hcGkvdG9kb3MnKVxuXHRcdFx0XHRcdC50aGVuKGZ1bmN0aW9uIHN1Y2Nlc3MoKSB7XG5cdFx0XHRcdFx0XHRyZXR1cm4gc3RvcmUudG9kb3M7XG5cdFx0XHRcdFx0fSwgZnVuY3Rpb24gZXJyb3IoKSB7XG5cdFx0XHRcdFx0XHRhbmd1bGFyLmNvcHkob3JpZ2luYWxUb2Rvcywgc3RvcmUudG9kb3MpO1xuXHRcdFx0XHRcdFx0cmV0dXJuIG9yaWdpbmFsVG9kb3M7XG5cdFx0XHRcdFx0fSk7XG5cdFx0XHR9LFxuXG5cdFx0XHRkZWxldGU6IGZ1bmN0aW9uICh0b2RvKSB7XG5cdFx0XHRcdHZhciBvcmlnaW5hbFRvZG9zID0gc3RvcmUudG9kb3Muc2xpY2UoMCk7XG5cblx0XHRcdFx0c3RvcmUudG9kb3Muc3BsaWNlKHN0b3JlLnRvZG9zLmluZGV4T2YodG9kbyksIDEpO1xuXG5cdFx0XHRcdHJldHVybiAkaHR0cC5kZWxldGUoJy9hcGkvdG9kb3MvJyArIHRvZG8uaWQpXG5cdFx0XHRcdFx0LnRoZW4oZnVuY3Rpb24gc3VjY2VzcygpIHtcblx0XHRcdFx0XHRcdHJldHVybiBzdG9yZS50b2Rvcztcblx0XHRcdFx0XHR9LCBmdW5jdGlvbiBlcnJvcigpIHtcblx0XHRcdFx0XHRcdGFuZ3VsYXIuY29weShvcmlnaW5hbFRvZG9zLCBzdG9yZS50b2Rvcyk7XG5cdFx0XHRcdFx0XHRyZXR1cm4gb3JpZ2luYWxUb2Rvcztcblx0XHRcdFx0XHR9KTtcblx0XHRcdH0sXG5cblx0XHRcdGdldDogZnVuY3Rpb24gKCkge1xuXHRcdFx0XHRyZXR1cm4gJGh0dHAuZ2V0KCcvYXBpL3RvZG9zJylcblx0XHRcdFx0XHQudGhlbihmdW5jdGlvbiAocmVzcCkge1xuXHRcdFx0XHRcdFx0YW5ndWxhci5jb3B5KHJlc3AuZGF0YSwgc3RvcmUudG9kb3MpO1xuXHRcdFx0XHRcdFx0cmV0dXJuIHN0b3JlLnRvZG9zO1xuXHRcdFx0XHRcdH0pO1xuXHRcdFx0fSxcblxuXHRcdFx0aW5zZXJ0OiBmdW5jdGlvbiAodG9kbykge1xuXHRcdFx0XHR2YXIgb3JpZ2luYWxUb2RvcyA9IHN0b3JlLnRvZG9zLnNsaWNlKDApO1xuXG5cdFx0XHRcdHJldHVybiAkaHR0cC5wb3N0KCcvYXBpL3RvZG9zJywgdG9kbylcblx0XHRcdFx0XHQudGhlbihmdW5jdGlvbiBzdWNjZXNzKHJlc3ApIHtcblx0XHRcdFx0XHRcdHRvZG8uaWQgPSByZXNwLmRhdGEuaWQ7XG5cdFx0XHRcdFx0XHRzdG9yZS50b2Rvcy5wdXNoKHRvZG8pO1xuXHRcdFx0XHRcdFx0cmV0dXJuIHN0b3JlLnRvZG9zO1xuXHRcdFx0XHRcdH0sIGZ1bmN0aW9uIGVycm9yKCkge1xuXHRcdFx0XHRcdFx0YW5ndWxhci5jb3B5KG9yaWdpbmFsVG9kb3MsIHN0b3JlLnRvZG9zKTtcblx0XHRcdFx0XHRcdHJldHVybiBzdG9yZS50b2Rvcztcblx0XHRcdFx0XHR9KTtcblx0XHRcdH0sXG5cblx0XHRcdHB1dDogZnVuY3Rpb24gKHRvZG8pIHtcblx0XHRcdFx0dmFyIG9yaWdpbmFsVG9kb3MgPSBzdG9yZS50b2Rvcy5zbGljZSgwKTtcblxuXHRcdFx0XHRyZXR1cm4gJGh0dHAucHV0KCcvYXBpL3RvZG9zLycgKyB0b2RvLmlkLCB0b2RvKVxuXHRcdFx0XHRcdC50aGVuKGZ1bmN0aW9uIHN1Y2Nlc3MoKSB7XG5cdFx0XHRcdFx0XHRyZXR1cm4gc3RvcmUudG9kb3M7XG5cdFx0XHRcdFx0fSwgZnVuY3Rpb24gZXJyb3IoKSB7XG5cdFx0XHRcdFx0XHRhbmd1bGFyLmNvcHkob3JpZ2luYWxUb2Rvcywgc3RvcmUudG9kb3MpO1xuXHRcdFx0XHRcdFx0cmV0dXJuIG9yaWdpbmFsVG9kb3M7XG5cdFx0XHRcdFx0fSk7XG5cdFx0XHR9XG5cdFx0fTtcblxuXHRcdHJldHVybiBzdG9yZTtcblx0fSlcblxuXHQuZmFjdG9yeSgnbG9jYWxTdG9yYWdlJywgZnVuY3Rpb24gKCRxKSB7XG5cdFx0J3VzZSBzdHJpY3QnO1xuXG5cdFx0dmFyIFNUT1JBR0VfSUQgPSAndG9kb3MtYW5ndWxhcmpzJztcblxuXHRcdHZhciBzdG9yZSA9IHtcblx0XHRcdHRvZG9zOiBbXSxcblxuXHRcdFx0X2dldEZyb21Mb2NhbFN0b3JhZ2U6IGZ1bmN0aW9uICgpIHtcblx0XHRcdFx0cmV0dXJuIEpTT04ucGFyc2UobG9jYWxTdG9yYWdlLmdldEl0ZW0oU1RPUkFHRV9JRCkgfHwgJ1tdJyk7XG5cdFx0XHR9LFxuXG5cdFx0XHRfc2F2ZVRvTG9jYWxTdG9yYWdlOiBmdW5jdGlvbiAodG9kb3MpIHtcblx0XHRcdFx0bG9jYWxTdG9yYWdlLnNldEl0ZW0oU1RPUkFHRV9JRCwgSlNPTi5zdHJpbmdpZnkodG9kb3MpKTtcblx0XHRcdH0sXG5cblx0XHRcdGNsZWFyQ29tcGxldGVkOiBmdW5jdGlvbiAoKSB7XG5cdFx0XHRcdHZhciBkZWZlcnJlZCA9ICRxLmRlZmVyKCk7XG5cblx0XHRcdFx0dmFyIGNvbXBsZXRlVG9kb3MgPSBbXTtcblx0XHRcdFx0dmFyIGluY29tcGxldGVUb2RvcyA9IFtdO1xuXHRcdFx0XHRzdG9yZS50b2Rvcy5mb3JFYWNoKGZ1bmN0aW9uICh0b2RvKSB7XG5cdFx0XHRcdFx0aWYgKHRvZG8uY29tcGxldGVkKSB7XG5cdFx0XHRcdFx0XHRjb21wbGV0ZVRvZG9zLnB1c2godG9kbyk7XG5cdFx0XHRcdFx0fSBlbHNlIHtcblx0XHRcdFx0XHRcdGluY29tcGxldGVUb2Rvcy5wdXNoKHRvZG8pO1xuXHRcdFx0XHRcdH1cblx0XHRcdFx0fSk7XG5cblx0XHRcdFx0YW5ndWxhci5jb3B5KGluY29tcGxldGVUb2Rvcywgc3RvcmUudG9kb3MpO1xuXG5cdFx0XHRcdHN0b3JlLl9zYXZlVG9Mb2NhbFN0b3JhZ2Uoc3RvcmUudG9kb3MpO1xuXHRcdFx0XHRkZWZlcnJlZC5yZXNvbHZlKHN0b3JlLnRvZG9zKTtcblxuXHRcdFx0XHRyZXR1cm4gZGVmZXJyZWQucHJvbWlzZTtcblx0XHRcdH0sXG5cblx0XHRcdGRlbGV0ZTogZnVuY3Rpb24gKHRvZG8pIHtcblx0XHRcdFx0dmFyIGRlZmVycmVkID0gJHEuZGVmZXIoKTtcblxuXHRcdFx0XHRzdG9yZS50b2Rvcy5zcGxpY2Uoc3RvcmUudG9kb3MuaW5kZXhPZih0b2RvKSwgMSk7XG5cblx0XHRcdFx0c3RvcmUuX3NhdmVUb0xvY2FsU3RvcmFnZShzdG9yZS50b2Rvcyk7XG5cdFx0XHRcdGRlZmVycmVkLnJlc29sdmUoc3RvcmUudG9kb3MpO1xuXG5cdFx0XHRcdHJldHVybiBkZWZlcnJlZC5wcm9taXNlO1xuXHRcdFx0fSxcblxuXHRcdFx0Z2V0OiBmdW5jdGlvbiAoKSB7XG5cdFx0XHRcdHZhciBkZWZlcnJlZCA9ICRxLmRlZmVyKCk7XG5cblx0XHRcdFx0YW5ndWxhci5jb3B5KHN0b3JlLl9nZXRGcm9tTG9jYWxTdG9yYWdlKCksIHN0b3JlLnRvZG9zKTtcblx0XHRcdFx0ZGVmZXJyZWQucmVzb2x2ZShzdG9yZS50b2Rvcyk7XG5cblx0XHRcdFx0cmV0dXJuIGRlZmVycmVkLnByb21pc2U7XG5cdFx0XHR9LFxuXG5cdFx0XHRpbnNlcnQ6IGZ1bmN0aW9uICh0b2RvKSB7XG5cdFx0XHRcdHZhciBkZWZlcnJlZCA9ICRxLmRlZmVyKCk7XG5cblx0XHRcdFx0c3RvcmUudG9kb3MucHVzaCh0b2RvKTtcblxuXHRcdFx0XHRzdG9yZS5fc2F2ZVRvTG9jYWxTdG9yYWdlKHN0b3JlLnRvZG9zKTtcblx0XHRcdFx0ZGVmZXJyZWQucmVzb2x2ZShzdG9yZS50b2Rvcyk7XG5cblx0XHRcdFx0cmV0dXJuIGRlZmVycmVkLnByb21pc2U7XG5cdFx0XHR9LFxuXG5cdFx0XHRwdXQ6IGZ1bmN0aW9uICh0b2RvLCBpbmRleCkge1xuXHRcdFx0XHR2YXIgZGVmZXJyZWQgPSAkcS5kZWZlcigpO1xuXG5cdFx0XHRcdHN0b3JlLnRvZG9zW2luZGV4XSA9IHRvZG87XG5cblx0XHRcdFx0c3RvcmUuX3NhdmVUb0xvY2FsU3RvcmFnZShzdG9yZS50b2Rvcyk7XG5cdFx0XHRcdGRlZmVycmVkLnJlc29sdmUoc3RvcmUudG9kb3MpO1xuXG5cdFx0XHRcdHJldHVybiBkZWZlcnJlZC5wcm9taXNlO1xuXHRcdFx0fVxuXHRcdH07XG5cblx0XHRyZXR1cm4gc3RvcmU7XG5cdH0pO1xuXHJcbi8vY29uc3QgSFRUUCA9IG5ldyBXZWFrTWFwKCk7XHJcblxyXG4vL2NsYXNzIEJvb2tTaGVsZlNlcnZpY2VcclxuLy97XHJcbi8vICAgIGNvbnN0cnVjdG9yKCRodHRwKVxyXG4vLyAgICB7XHJcbi8vICAgICAgICBIVFRQLnNldCh0aGlzLCAkaHR0cCk7XHJcbi8vICAgIH1cclxuXHJcbi8vICAgIGdldEFjdGl2ZUJvb2tzKCl7XHJcbi8vICAgICAgICByZXR1cm4gSFRUUC5nZXQodGhpcykuZ2V0KCcvYXBpL2FjdGl2ZUJvb2tzJykudGhlbihyZXN1bHQgPT4gcmVzdWx0LmRhdGEgKTtcclxuLy8gICAgfVxyXG5cclxuLy8gICAgZ2V0QXJjaGl2ZWRCb29rcygpe1xyXG4vLyAgICAgICAgcmV0dXJuIEhUVFAuZ2V0KHRoaXMpLmdldCgnL2FwaS9hcmNoaXZlZEJvb2tzJykudGhlbihyZXN1bHQgPT4gcmVzdWx0LmRhdGEgKTtcclxuLy8gICAgfVxyXG5cclxuLy8gICAgbWFya0Jvb2tSZWFkKGJvb2tJZCwgaXNCb29rUmVhZCl7XHJcbi8vICAgICAgICByZXR1cm4gSFRUUC5nZXQodGhpcykucHV0KGAvYXBpL21hcmtSZWFkLyR7Ym9va0lkfWAsIHtib29rSWQ6IGJvb2tJZCwgcmVhZDogaXNCb29rUmVhZH0pO1xyXG4vLyAgICB9XHJcblxyXG4vLyAgICBhZGRUb0FyY2hpdmUoYm9va0lkKXtcclxuLy8gICAgICAgIHJldHVybiBIVFRQLmdldCh0aGlzKS5wdXQoYC9hcGkvYWRkVG9BcmNoaXZlLyR7Ym9va0lkfWAse30pO1xyXG4vLyAgICB9XHJcblxyXG4vLyAgICBjaGVja0lmQm9va0V4aXN0cyh0aXRsZSl7XHJcbi8vICAgICAgICByZXR1cm4gSFRUUC5nZXQodGhpcykuZ2V0KGAvYXBpL2Jvb2tFeGlzdHMvJHt0aXRsZX1gKS50aGVuKHJlc3VsdCA9PiAgcmVzdWx0LmRhdGEgKTtcclxuLy8gICAgfVxyXG5cclxuLy8gICAgYWRkQm9vayhib29rKXtcclxuLy8gICAgICAgIHJldHVybiBIVFRQLmdldCh0aGlzKS5wb3N0KCcvYXBpL2Jvb2tzJywgYm9vayk7XHJcbi8vICAgIH1cclxuXHJcbi8vICAgIHN0YXRpYyBib29rU2hlbGZGYWN0b3J5KCRodHRwKXtcclxuLy8gICAgICAgIHJldHVybiBuZXcgQm9va1NoZWxmU2VydmljZSgkaHR0cCk7XHJcbi8vICAgIH1cclxuLy99XHJcblxyXG4vL0Jvb2tTaGVsZlNlcnZpY2UuYm9va1NoZWxmRmFjdG9yeS4kaW5qZWN0ID0gWyckaHR0cCddO1xyXG5cclxuLy9hbmd1bGFyLm1vZHVsZShtb2R1bGVOYW1lLCBbXSlcclxuLy8gIC5mYWN0b3J5KCdib29rU2hlbGZTdmMnLCBCb29rU2hlbGZTZXJ2aWNlLmJvb2tTaGVsZkZhY3RvcnkpO1xyXG4vKmdsb2JhbCBhbmd1bGFyICovXG5cbi8qKlxuICogRGlyZWN0aXZlIHRoYXQgZXhlY3V0ZXMgYW4gZXhwcmVzc2lvbiB3aGVuIHRoZSBlbGVtZW50IGl0IGlzIGFwcGxpZWQgdG8gZ2V0c1xuICogYW4gYGVzY2FwZWAga2V5ZG93biBldmVudC5cbiAqL1xuYW5ndWxhci5tb2R1bGUobW9kdWxlTmFtZSlcblx0LmRpcmVjdGl2ZSgndG9kb0VzY2FwZScsIGZ1bmN0aW9uICgpIHtcblx0XHQndXNlIHN0cmljdCc7XG5cblx0XHR2YXIgRVNDQVBFX0tFWSA9IDI3O1xuXG5cdFx0cmV0dXJuIGZ1bmN0aW9uIChzY29wZSwgZWxlbSwgYXR0cnMpIHtcblx0XHRcdGVsZW0uYmluZCgna2V5ZG93bicsIGZ1bmN0aW9uIChldmVudCkge1xuXHRcdFx0XHRpZiAoZXZlbnQua2V5Q29kZSA9PT0gRVNDQVBFX0tFWSkge1xuXHRcdFx0XHRcdHNjb3BlLiRhcHBseShhdHRycy50b2RvRXNjYXBlKTtcblx0XHRcdFx0fVxuXHRcdFx0fSk7XG5cblx0XHRcdHNjb3BlLiRvbignJGRlc3Ryb3knLCBmdW5jdGlvbiAoKSB7XG5cdFx0XHRcdGVsZW0udW5iaW5kKCdrZXlkb3duJyk7XG5cdFx0XHR9KTtcblx0XHR9O1xuXHR9KTtcblxyXG4vKmdsb2JhbCBhbmd1bGFyICovXG5cbi8qKlxuICogRGlyZWN0aXZlIHRoYXQgcGxhY2VzIGZvY3VzIG9uIHRoZSBlbGVtZW50IGl0IGlzIGFwcGxpZWQgdG8gd2hlbiB0aGVcbiAqIGV4cHJlc3Npb24gaXQgYmluZHMgdG8gZXZhbHVhdGVzIHRvIHRydWVcbiAqL1xuYW5ndWxhci5tb2R1bGUobW9kdWxlTmFtZSlcblx0LmRpcmVjdGl2ZSgndG9kb0ZvY3VzJywgZnVuY3Rpb24gdG9kb0ZvY3VzKCR0aW1lb3V0KSB7XG5cdFx0J3VzZSBzdHJpY3QnO1xuXG5cdFx0cmV0dXJuIGZ1bmN0aW9uIChzY29wZSwgZWxlbSwgYXR0cnMpIHtcblx0XHRcdHNjb3BlLiR3YXRjaChhdHRycy50b2RvRm9jdXMsIGZ1bmN0aW9uIChuZXdWYWwpIHtcblx0XHRcdFx0aWYgKG5ld1ZhbCkge1xuXHRcdFx0XHRcdCR0aW1lb3V0KGZ1bmN0aW9uICgpIHtcblx0XHRcdFx0XHRcdGVsZW1bMF0uZm9jdXMoKTtcblx0XHRcdFx0XHR9LCAwLCBmYWxzZSk7XG5cdFx0XHRcdH1cblx0XHRcdH0pO1xuXHRcdH07XG5cdH0pOyJdfQ==

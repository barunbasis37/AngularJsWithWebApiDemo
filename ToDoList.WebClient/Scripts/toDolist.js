angular.module('toDolist', ['ngRoute', 'ngResource']).config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', { templateUrl: 'Views/Dashboard/dashboard.tpl.html', controller: 'dashboardController' })
    .when('/dashboard', { templateUrl: 'Views/Dashboard/dashboard.tpl.html', controller: 'dashboardController' })
    .when('/projects', { templateUrl: 'Views/Project/projects.tpl.html', controller: 'projectsController' })
    .when('/creatproject', { templateUrl: 'Views/Project/creatproject.tpl.html', controller: 'projectcreateController' })
    .when('/projectdetail/:id', { templateUrl: 'Views/Project/creatproject.tpl.html', controller: 'projectdetailController' })
    .when('/tasks', { templateUrl: 'Views/Task/tasks.tpl.html', controller: 'tasksController' })
    .when('/taskdetail/:id', { templateUrl: 'Views/Task/taskcreat.tpl.html', controller: 'taskdetailController' })
    .when('/creatTask', { templateUrl: 'Views/Task/taskcreat.tpl.html', controller: 'taskcreateController' })
    .otherwise({ redirectTo: '/' });

}]);
angular.module('toDolist').value('baseUrl', 'http://localhost/ToDoListApi/api/');

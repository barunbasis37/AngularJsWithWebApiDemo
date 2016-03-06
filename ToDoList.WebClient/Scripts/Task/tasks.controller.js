angular.module('toDolist').controller('tasksController', ['$scope', 'taskcreateService', function ($scope, taskcreateService) {
    var errorFunction = function (error) {
        console.log(error);
    };

    $scope.loadTasks = function () {
        taskcreateService.getTaskByProjectId(0).then(function (response) {
            console.log(response);
            if (response.IsSuccess) {
                $scope.tasks = response.Data;
            } else {
                alert(response.Message);
            }
        }, errorFunction);
    };

    $scope.deleteTask = function (t) {
        taskcreateService.deleteTaskById(t.TaskId).then(function (response) {
            if (response.IsSuccess) {
                $scope.loadTasks();
            } else {
                alert(response.Message + '\n Details: ' + response.Exception.Message);
            }

        }, errorFunction);

    };

    function init() {
        $scope.loadTasks();
    }

    init();
}]);
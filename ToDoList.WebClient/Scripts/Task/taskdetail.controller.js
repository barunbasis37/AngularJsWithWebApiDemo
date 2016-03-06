angular.module('toDolist').controller('taskdetailController', ['$scope', '$location', '$routeParams', 'taskcreateService', 'projectcreateService', function ($scope, $location, $routeParams, taskcreateService, projectcreateService) {
    var taskSearchId = $routeParams.id;
    //$scope.pagename = "Project Detail  " + taskSearchId;

    $scope.buttonName = 'Update';
    $scope.task = {};

    $scope.saveTask = function () {
        $scope.task.ProjectRefId = $scope.task.Project.ProjectId;
        taskcreateService.savetask($scope.task).then(function (response) {
            console.log(response);
            alert(response.message);
            $location.path('/tasks');
        }, function (error) {
            alert(error.statusText);
        });
    };
    
    var loadProjects = function () {
        projectcreateService.getAllProjects().then(function (response) {
           if (response.IsSuccess) {
                $scope.projects = response.Data;
                taskcreateService.getTaskByTaskId(taskSearchId).then(function (r) {
                    $scope.task = r.Data;
                    $scope.task.DueDate = new Date(r.Data.DueDate);
                    for (var i = 0; i < $scope.projects.length; i++) {
                        if ($scope.projects[i].ProjectId === $scope.task.ProjectRefId) {
                            $scope.task.Project = $scope.projects[i];
                            break;
                        }
                    }
                }, function (error) {
                    alert(error.statusText);
                });


            } else {
                alert(response.Message);
            }
        }, function (error) {
            console.log(error);
        });
    };
    function init() {
        loadProjects();
    };
    init();

}]);
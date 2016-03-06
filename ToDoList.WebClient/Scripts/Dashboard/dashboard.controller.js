angular.module('toDolist').controller('dashboardController', ['$scope', 'dashboardService', 'projectcreateService', 'taskcreateService', function ($scope, dashboardService, projectcreateService, taskcreateService) {
    $scope.pagename = "Dashboard Page";
    $scope.projects = [];
    $scope.selectedProject = {};
    $scope.tasks = [];
    $scope.sortOn = 'date';
    $scope.sortBy = 'desc';
    $scope.filterText = '';
    var errorFunction = function(error) {
        console.log(error);
    };

    $scope.complete = function (t) {
        taskcreateService.markComplete(t).then(function (response) {
            if (response.IsSuccess) {
                $scope.loadTasks($scope.selectedProject.ProjectId);
            }
        }, errorFunction);
    };

    $scope.loadTasks = function (p) {
        $scope.tasks = [];
        $scope.selectedProject = p;
        taskcreateService.getTaskByProjectId(p.ProjectId, $scope.sortOn, $scope.sortBy).then(function (response) {
            if (response.IsSuccess) {
                $scope.tasks = response.Data;
                if ($scope.tasks.length===0) {
                    alert("No Task Create For this Project");
                }
            } else {
                alert(response.Message);
            }
        }, errorFunction);
    };



    $scope.loadProjects = function() {
        projectcreateService.getAllProjects().then(function (response) {
            console.log(response);
            if (response.IsSuccess) {
                $scope.projects = response.Data;
                if ($scope.projects.length>0) {
                    $scope.loadTasks( $scope.projects[0]);
                }
            } else {
                alert(response.Message);
            }
        }, errorFunction);
    };

    function init() {
        $scope.loadProjects();
    }

    init();

}]);
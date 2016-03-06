angular.module('toDolist').controller('projectsController', ['$scope', 'projectcreateService', function ($scope, projectcreateService) {
    
    var errorFunction = function (error) {
        console.log(error);
    };

    $scope.loadProjects = function () {
        projectcreateService.getAllProjects().then(function (response) {
            console.log(response);
            if (response.IsSuccess) {
                $scope.projects = response.Data;
            } else {
                alert(response.Message);
            }
        }, errorFunction);
    };

    $scope.deleteProject = function (project) {
        projectcreateService.deleteProjectById(project.ProjectId).then(function (response) {
            if (response.IsSuccess) {
                $scope.loadProjects();
            } else {
                alert(response.Message +'\n Details: ' + response.Exception.Message);
            }
            
        }, errorFunction);

    };

    function init() {
        $scope.loadProjects();
    }

    init();
}]);
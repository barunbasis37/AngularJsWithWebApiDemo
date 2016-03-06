angular.module('toDolist').controller('taskcreateController', ['$scope', '$location', 'taskcreateService', 'projectcreateService', function ($scope,$location, taskcreateService, projectcreateService) {

    $scope.task = {Project : null};
    $scope.projects = [];
    $scope.buttonName = 'Create';
    

    $scope.saveTask = function () {
        $scope.task.DueDate = $scope.task.DueDate.toDateString();
        //$scope.task.DueDate = new Date($scope.task.DueDate);
        $scope.task.ProjectRefId = $scope.task.Project.ProjectId;
        console.log($scope.task);
        taskcreateService.savetask($scope.task).then(function (response) {
            console.log(response);
            //alert(response.message);
            $scope.project = {};
            $location.path('/');
        },function(error) {
            alert(error.statusText);
        });
    };

    var loadProjects = function() {
        projectcreateService.getAllProjects().then(function(response) {
            console.log(response);
            if (response.IsSuccess) {
                $scope.projects = response.Data;
            } else {
                alert(response.Message);
            }
        }, function(error) {
            console.log(error);
        });
    };
     function init() {
          loadProjects();
     };
    init();

}]);
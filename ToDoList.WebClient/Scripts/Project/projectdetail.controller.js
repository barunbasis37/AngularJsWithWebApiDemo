angular.module('toDolist').controller('projectdetailController', ['$scope', '$location', '$routeParams', 'projectcreateService', function ($scope, $location, $routeParams, projectcreateService) {
    var projectSearchId = $routeParams.id;
    $scope.pagename = "Project Detail  " + projectSearchId;

    $scope.buttonName = 'Update';
    $scope.project = {};
    
    projectcreateService.getProjectById(projectSearchId).then(function (response) {
        console.log(response);
        $scope.project = response.Data;
    }, function (error) {
        alert(error.statusText);
    });
    
    $scope.saveproject = function () {
        projectcreateService.saveProject($scope.project).then(function (response) {
            console.log(response);
            alert(response.Message);
            $location.path('/projects');
        }, function (error) {
            alert(error.statusText);
        });
    };



}]);
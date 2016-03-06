angular.module('toDolist').controller('projectcreateController', ['$scope', 'projectcreateService', function ($scope, projectcreateService) {
    $scope.buttonName = 'Create';
    $scope.project = {};
    $scope.saveproject = function () {
        projectcreateService.saveProject($scope.project).then(function (response) {
            console.log(response);
            $scope.project = {};
        },function(error) {
            alert(error.statusText);
        });
    };
}]);
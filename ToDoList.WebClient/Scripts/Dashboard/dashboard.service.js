angular.module('toDolist').service('dashboardService', ['$resource', '$q', function ($resource, $q) {
    //var baseUrl = 'http://localhost:43387/api/';

    //var getAllProjects = function () {
    //    var defer = $q.defer();
    //    var resource = $resource(baseUrl + 'Project');
    //    resource.get(function(response) {
    //        return defer.resolve(response);
    //    }, function(error) {
    //        return defer.reject(error);
    //    });
    //    return defer.promise;
    //};
    //return {
    //    getAllProjects: getAllProjects
    //};

}]);
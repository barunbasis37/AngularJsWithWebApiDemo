angular.module('toDolist').service('projectcreateService', ['$resource', '$q', 'baseUrl', function ($resource, $q, baseUrl) {
    //var baseUrl = 'http://localhost/ToDoListApi/api/';

    var saveProject = function (project) {
        var defer = $q.defer();
        var resource = $resource(baseUrl + 'Project');
        resource.save(project, function (response) {
            return defer.resolve(response);
        }, function (error) {
            return defer.reject(error);
        });
        return defer.promise;
    };
    
    var getAllProjects = function () {
        var defer = $q.defer();
        var resource = $resource(baseUrl + 'Project');
        resource.get(function (response) {
            return defer.resolve(response);
        }, function (error) {
            return defer.reject(error);
        });
        return defer.promise;
    };

    var getProjectById = function (projectId) {
        var defer = $q.defer();
        var resource = $resource(baseUrl + 'Project?projectId=' + projectId);
        resource.get(function (response) {
            return defer.resolve(response);
        }, function (error) {
            return defer.reject(error);
        });
        return defer.promise;
    };

    var deleteProjectById = function (projectId) {
        var defer = $q.defer();
        var resource = $resource(baseUrl + 'Project?projectId=' + projectId);
        resource.delete(function (response) {
            return defer.resolve(response);
        }, function (error) {
            return defer.reject(error);
        });
        return defer.promise;
    };

    return {
        saveProject: saveProject,
        getAllProjects: getAllProjects,
        getProjectById: getProjectById,
        deleteProjectById: deleteProjectById,
    };

}]);
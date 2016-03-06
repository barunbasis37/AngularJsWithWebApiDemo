angular.module('toDolist').service('taskcreateService', ['$resource', '$q', 'baseUrl', function ($resource, $q, baseUrl) {
    //var baseUrl = 'http://localhost/ToDoListApi/api/';

    var savetask = function (task) {
        var defer = $q.defer();
        var resource = $resource(baseUrl + 'Task');
        resource.save(task, function (response) {
            return defer.resolve(response);
        }, function (error) {
            return defer.reject(error);
        });
        return defer.promise;
    };
    
    var markComplete = function (task) {
        var defer = $q.defer();
        var resource = $resource(baseUrl + 'TaskComplete');
        resource.save(task, function (response) {
            return defer.resolve(response);
        }, function (error) {
            return defer.reject(error);
        });
        return defer.promise;
    };
    
    var getTaskByProjectId = function (projectId, sortOn, sortBy) {
        var defer = $q.defer();
        var resource = $resource(baseUrl + 'Task?projectId=' + projectId +'&sortOn=' + sortOn +'&sortBy=' + sortBy);
        resource.get(projectId, function (response) {
            return defer.resolve(response);
        }, function (error) {
            return defer.reject(error);
        });
        return defer.promise;
    };

    var getTaskByTaskId = function (taskId) {
        var defer = $q.defer();
        var resource = $resource(baseUrl + 'Task?taskId=' + taskId);
        resource.get(function (response) {
            return defer.resolve(response);
        }, function (error) {
            return defer.reject(error);
        });
        return defer.promise;
    };

    var deleteTaskById = function (taskId) {
        var defer = $q.defer();
        var resource = $resource(baseUrl + 'Task?taskId=' + taskId);
        resource.delete(function (response) {
            return defer.resolve(response);
        }, function (error) {
            return defer.reject(error);
        });
        return defer.promise;
    };

    
    return {
        savetask: savetask,
        getTaskByProjectId: getTaskByProjectId,
        getTaskByTaskId:getTaskByTaskId,
        deleteTaskById: deleteTaskById,
        markComplete: markComplete,
    };

}]);
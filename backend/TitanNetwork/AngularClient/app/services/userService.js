'use strict';
app.factory('userService', ['$http', function ($http) {

    var serviceBase = 'http://localhost/WebApiTier/';
    var usersServiceFactory = {};

    var _getUsers = function () {

        return $http.get(serviceBase + 'api/users').then(function (results) {
            return results;
        });
    };

    usersServiceFactory.getUsers = _getUsers;

    return usersServiceFactory;

}]);
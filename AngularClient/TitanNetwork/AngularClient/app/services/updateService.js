'use strict'
app.factory('updateService', ['$http', 'localStorageService', '$q', function ($http, localStorageService, $q) {
    var updateServiceFactory = {};

    var serviceBase = 'http://titanapidev.azurewebsites.net/';

    var userID = localStorageService.get("userID");

    var _updateUser = function (updateData) {
        var deferred = $q.defer();

        var userName = localStorageService.get("authorizationData").userName;

        var data = {
            about: updateData.about,
            age: updateData.age,
            firstName: updateData.firstName,
            userID: userID,
            lastName: updateData.lastName,
            midleName: updateData.middleName,
            userName: userName
        };

        $http.put(serviceBase + 'api/users/' + userID, data).then(function (responce) {
            deferred.resolve(responce);
        },
        function (err) {
            deferred.reject(err);
        });

        return deferred.promise;
    }

    updateServiceFactory.updateUser = _updateUser;

    return updateServiceFactory;
}])


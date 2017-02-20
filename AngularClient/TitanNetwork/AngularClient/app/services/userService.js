'use strict';
app.factory('userService', ['$http','localStorageService','$q', function ($http,localStorageService,$q) {

    var serviceBase = 'http://titanapidev.azurewebsites.net/';
    var usersServiceFactory = {};
    var userId = localStorageService.get('userID');

    var _getUsers = function (page,pageSize) {

        return $http.get(serviceBase + 'api/users?page=' + page + '&pageSize=' + pageSize).then(function (results) {
            return results;
        });
    };

    var _createChat = function (newUserID,newUserName) {
        
        var chatID = 0;

        var deferred = $q.defer();

        var chatTitle = newUserName + '_' + localStorageService.get("authorizationData").userName;

        var chatData = "id=0&title=" + chatTitle;

        $http.post(serviceBase + 'api/users/' + userId + '/chats', chatData, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(
            function (responce) {
                chatID = responce;
                var nullData = {};
                $http.post(serviceBase + 'api/users/' + userId + '/chats/' + chatID + '/users/' + newUserID, nullData).then(
                    function (innerResponce) {
                        deferred.resolve(innerResponce);
                    },
                    function (err, status) {
                        deferred.reject(err);
                    }
                );
                deferred.resolve(responce)
            }
        ).error(function (err, status) {
                    deferred.reject(err);
                }
        );

        
        
        return deferred.promise;
    }

    var _getUserInfo = function (id) {
        return $http.get(serviceBase + 'api/users/'+ id).then(function (results) {
            return results;
        });
    }

    var _getMutualFriends = function (id) {
        return $http.get(serviceBase + 'api/users/' + userId + '/mutual/' + id + '?page=0&pageSize=5').then(function (results) {
            return results;
        });
    }

    var _getWayBeetweenUsers = function (id) {
        return $http.get(serviceBase + 'api/users/' + userId + '/way/' + id + '?page=0&pageSize=5').then(function (results) {
            return results;
        });
    }

    usersServiceFactory.getUserInfo = _getUserInfo;
    usersServiceFactory.getUsers = _getUsers;
    usersServiceFactory.createChat = _createChat;
    usersServiceFactory.getMutual = _getMutualFriends;
    usersServiceFactory.getWay = _getWayBeetweenUsers;

    return usersServiceFactory;

}]);
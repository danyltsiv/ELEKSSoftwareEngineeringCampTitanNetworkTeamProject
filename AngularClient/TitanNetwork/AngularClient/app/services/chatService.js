'use strict';
app.factory('chatService', ['$http', 'localStorageService','$q', function ($http, localStorageService,$q) {

    var serviceBase = 'http://titanapidev.azurewebsites.net/';

    var chatServiceFactory = {};

    var userID = localStorageService.get("userID");

    var _getMessages = function (chatID,page,pageSize) {
        return $http.get(serviceBase + 'api/users/' + userID + '/chats/' + chatID + '/messages?page=' + page + '&pageSize=' + pageSize)
            .then(function (results) {
                return results;
        });
    }

    var _sendMessage = function (chatID, content) {
        var deferred = $q.defer();

        var data = "id=0&newContent=" + content + "&userId=" + userID;

        $http.post(serviceBase + 'api/users/' + userID + '/chats/' + chatID + '/messages', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
        .success(function (responce) {
            deferred.resolve(responce);
        }).error(function (err) {
            deferred.reject(err);
        });

        return deferred.promise;
    }

    var _getChats = function (page,pageSize) {
        return $http.get(serviceBase + 'api/users/' + userID + '/chats?page=' + page + '&pageSize=' + pageSize).then(
            function (results) {
                return results;
            }
        );
    }

    var _getUserById = function (id) {
        return $http.get(serviceBase + 'api/users/' + id).then(function (results) {
            return results;
        });
    };

    chatServiceFactory.getMessages = _getMessages;
    chatServiceFactory.sendMessage = _sendMessage;
    chatServiceFactory.getChats = _getChats;
    chatServiceFactory.userID = userID;
    chatServiceFactory.getUserById = _getUserById;

    return chatServiceFactory;
}]);
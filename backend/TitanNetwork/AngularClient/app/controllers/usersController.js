'use strict';
app.controller('usersController', ['$scope', 'userService', function ($scope, userService) {

    $scope.users = [];

    userService.getUsers().then(function (results) {

        $scope.users = results.data;


    }, function (error) {
        alert(error.data.message);
    });

}]);
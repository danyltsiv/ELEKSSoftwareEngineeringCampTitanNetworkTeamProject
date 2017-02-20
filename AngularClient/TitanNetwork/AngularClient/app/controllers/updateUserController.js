'use strict'
app.controller('updateUserController', ['$scope', 'updateService', function ($scope, updateService) {
    $scope.updateData = {
        about: "",
        age: 0,
        firstName: "",
        lastName: "",
        middleName:""
    }

    $scope.message = "";

    $scope.update = function () {
        updateService.updateUser($scope.updateData).then(function (responce) {

        }, function (err) {
            $scope.message = err.error_description;
        })
    }
}])
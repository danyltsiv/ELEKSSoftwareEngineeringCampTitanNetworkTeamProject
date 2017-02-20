'use strict';
app.controller('usersController', ['$scope', 'userService','$location', function ($scope, userService,$location) {
    $scope.users = [];
    $scope.currentIndex = 0;
    $scope.userInfo = {
        about: "",
        age: 0,
        firstName: "",
        id: 0,
        lastName: "",
        midleName: "",
        userName: ""
    };
    $scope.popoverIsVisible = false;

    $scope.showPopover = function () {
        $scope.popoverIsVisible = true;
    };

    $scope.hidePopover = function () {
        $scope.popoverIsVisible = false;
    };

    userService.getUsers(0,20).then(function (results) {
        $scope.users = results.data;
        $scope.currentIndex = 0;

    }, function (error) {
        alert(error.data.message);
    });

    $scope.getUsers = function () {
        userService.getUsers($scope.currentIndex, 20).then(function (results) {
            var tempArray = results.data;
            for (var i = 0; i < tempArray.length; i++) {
                $scope.users.push(tempArray[i]);
            }
            
        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.createChat = function(userID,userName){
        userService.createChat(userID, userName).then(
            function (responce) {
                $location.path('/chats')
            },
            function (err) {

            }
        );
    }

    $scope.showInfo = function (id) {
        userService.getUserInfo(id).then(function (results) {
            $scope.userInfo = results.data;
        },
        function (error) {
            alert(error.data.message);
        });
    }

    $scope.getMutual = function (id) {
        userService.getMutual(id).then(function (results) {
            $scope.users = results.data;
        },
        function (error) {
            alert(error.data.message);
        });
    }

    $scope.getWay = function (id) {
        userService.getWay(id).then(function (results) {
            $scope.users = results.data;
        },
        function (error) {
            alert(error.data.message);
        });
    }
    
}]);

app.directive('whenScrollEnds', function () {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            var visibleHeight = element.height();
            var threshold = 80;

            element.scroll(function () {
                var scrollableHeight = element.prop('scrollHeight');
                var hiddenContentHeight = scrollableHeight - visibleHeight;

                if (hiddenContentHeight - element.scrollTop() <= threshold) {
                    // Scroll is almost at the bottom. Loading more rows
                    scope.$apply(attrs.whenScrollEnds);
                }
            });
        }
    };
});

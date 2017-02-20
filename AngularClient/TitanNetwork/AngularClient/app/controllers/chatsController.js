'use strict';
app.controller('chatsController', ['$scope', 'chatService','$timeout', function ($scope, chatService,$timeout) {
    $scope.chats = [];
    $scope.messages = [];
    $scope.currentChatId;
    $scope.receiver;
    $scope.message = '';
    $scope.userID = chatService.userID;
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
    $scope.receiverInfo = {
        about: "",
        age: 0,
        firstName: "",
        id: 0,
        lastName: "",
        midleName: "",
        userName: ""
    };

    chatService.getChats(0, 10).then(function (results) {
        $scope.chats = results.data;
        $scope.currentChatId = results.data.reverse()[0].id;
        $scope.receiver = $scope.currentChatId;
        $scope.getMessages($scope.currentChatId);
    }, function (error) {
        alert(error.data.message);
    });

    $scope.getMessages = function (chatID) {
        chatService.getMessages(chatID, $scope.currentIndex, 5).then(function (results) {
            //$scope.messages.unshift(results.data.reverse());
            var tempArray = results.data;
            for(var i = 0; i < tempArray.length; i++){
                $scope.messages.unshift(tempArray[i]);
            }
            $scope.currentChatId = chatID;
            $scope.receiver = chatID;
        }, function (error) {
            alert(error.data.message);
        });
    };

    $scope.selectChat = function (chatID, title) {
        chatService.getMessages(chatID, 0, 20).then(function (results) {
            $scope.messages = results.data.reverse();
            $scope.currentChatId = chatID;
            $scope.currentIndex = 0;
            if (title != 'TitanBot chat')
                $scope.receiver = chatID;
            else $scope.receiver = 'bot';
        },function (error) {
            alert(error.data.message);
        });
    };

    $scope.sendMessage = function (chatID) {
        chatService.sendMessage(chatID, $scope.message).then(function () {
            chatService.getMessages(chatID, 0, 20).then(function (results) {
                $scope.messages = results.data.reverse();
                $scope.currentChatId = chatID;
                $scope.currentIndex = 0;
            }, function (error) {
                alert(error.data.message);
            });
            $scope.message = '';
        }); 
    };

    $scope.getUserById = function (id) {
        chatService.getUserById(id).then(function (results) {
            $scope.userInfo = results.data;
        },
        function (error) {
            alert(error.data.message);
        });
    }

    $scope.getReceiverById = function (id) {
        chatService.getUserById(id).then(function (results) {
            $scope.receiverInfo = results.data;
        },
        function (error) {
            alert(error.data.message);
        });
    }
    
}]);

app.directive('upwardsScroll', function ($timeout) {
    return {
        link: function (scope, elem, attr, ctrl) {
            var raw = elem[0];

            elem.bind('scroll', function () {
                if (raw.scrollTop <= 0) {
                    var sh = raw.scrollHeight;
                    scope.$apply(attr.upwardsScroll);

                    $timeout(function () {
                        elem.animate({
                            scrollTop: raw.scrollHeight - sh
                        }, 500);
                    }, 0);
                }
            });

            //scroll to bottom
            $timeout(function () {
                scope.$apply(function () {
                    elem.scrollTop(raw.scrollHeight);
                });
            }, 0);
        }
    }
});

app.directive('scrollbot', ['$document', function ($timeout) {
    return {
        link: function (scope, element, attrs) {
            element.on('mouseup', function (event) {
                $timeout(() => {
                    var el = element.parent().parent().next();
                    el[0].scrollTop = el[0].scrollHeight;
                }, 200);
            });
        }
    };
}]);

app.directive('scrollbotsend', ['$document', function ($document) {
    return {
        link: function (scope, element, attrs) {
            element.on('keyup', function (event) {
                if (event.which == 13) {
                    setTimeout(() => {
                        var el = element.parent().prev();
                        el[0].scrollTop = el[0].scrollHeight;
                    }, 200);
                }
            });
        }
    };
}]);


app.directive('onloadscrollbot', ['$document', function ($document) {
    return {
        link: function (scope, element, attrs) {
            setTimeout(() => {
                element[0].scrollTop = element[0].scrollHeight;
            }, 300);
        }
    };
}]);
var app = angular.module('app', []);
app.value('$', $);

app.service('signalRService', function ($, $rootScope) {

    var initialize = function () {
        connection = $.hubConnection("http://localhost:1337/");

        proxy = connection.createHubProxy('balderdashHub');

        connection.start().done( function() {

            proxy.invoke('gameboardConnected')

            proxy.on('gameStarted', function () {
                $rootScope.$emit("newQuestion");
            });

            proxy.on('newQuestion', function (question) {
                $rootScope.$emit("newQuestion", question);
            });

            proxy.on('publishAnswers', function (answers) {
                $rootScope.$emit("publishAnswers", answers);
            });

            proxy.on('gameEnded', function () {
                $rootScope.$emit("gameEnded");
            });

            proxy.on('timerTick', function (currentTime) {
                $rootScope.$emit("timerTick", currentTime);
            });
        });
    };

    return {
        initialize: initialize,
    };

});

app.controller("signalrController", function ($scope, signalRService, $rootScope) {
    $scope.text = "";

    updateGreetingMessage = function (text) {
        $scope.text = text;
    }

    signalRService.initialize();

    $scope.$parent.$on("gameStarted", function (e, message) {
        $scope.$apply(function () {
            updateGreetingMessage(message)
        });
    });
});
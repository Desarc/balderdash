var app = angular.module('app', []);
app.value('$', $);

app.service('signalRService', function ($, $rootScope) {
    var proxy = null;

    var initialize = function () {
        connection = $.hubConnection("http://localhost:1337/");

        this.proxy = connection.createHubProxy('balderdashHub');

        connection.start();

        this.proxy.on('gameStarted', function () {
            $rootScope.$emit("newQuestion");
        });

        this.proxy.on('newQuestion', function (question) {
            $rootScope.$emit("newQuestion", question);
        });

        this.proxy.on('publishAnswers', function (answers) {
            $rootScope.$emit("publishAnswers", answers);
        });

        this.proxy.on('gameEnded', function () {
            $rootScope.$emit("gameEnded");
        });
    };

    var playerLogon = function (playername) {
        this.proxy.invoke('playerLogon', playername);
    };

    var startGame = function () {
        this.proxy.invoke('startGame');
    };

    var submitAnswer = function (answer) {
        this.proxy.invoke('submitAnswer', answer);
    };

    var chooseAnswer = function (answerId) {
        this.proxy.invoke('chooseAnswer', answerId);
    };

    var upvoteAnswer = function (answerId) {
        this.proxy.invoke('upvoteAnswer', answerId);
    };

    var continuePlaying = function () {
        this.proxy.invoke('continuePlaying');
    };

    var changePlayers = function () {
        this.proxy.invoke('changePlayers');
    };

    return {
        initialize: initialize,
        playerLogon: playerLogon,
        startGame: startGame,
        submitAnswer: submitAnswer,
        chooseAnswer: chooseAnswer,
        upvoteAnswer: upvoteAnswer,
        continuePlaying: continuePlaying,
        changePlayers: changePlayers
    };

});

app.controller("signalrController", function ($scope, signalRService, $rootScope) {
    $scope.text = "";

    $scope.logon = function (playerName) {
        signalRService.playerLogon(playername);
    }

    $scope.startGame = function () {
        signalRService.startGame();
    }

    updateGreetingMessage = function (text) {
        $scope.text = text;
    }

    signalRService.initialize();

    //Updating greeting message after receiving a message through the event

    $scope.$parent.$on("acceptGreet", function (e, message) {
        $scope.$apply(function () {
            updateGreetingMessage(message)
        });
    });
});
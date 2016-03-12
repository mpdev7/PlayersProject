(function () {
    'use strict';

    angular
        .module('app', [])
        .controller('ctrlPlayersList', ctrlPlayersList);

    ctrlPlayersList.$inject = ['$scope', 'sList','$interval']; 

    function ctrlPlayersList($scope, sList, $interval) {
        $scope.title = 'Players List';        
        
        var playerlist = function() {
            var promise = sList.async();
            promise.then(function (response) {
                $scope.players = response;
            });
        }
        
        playerlist();

        var AddPlayer = function AddPlayer (){
            var promise = sList.post($scope.obj.name, $scope.obj.surname, $scope.obj.position, $scope.obj.team);
            promise.then(function (response) {
                playerlist();
                $scope.insertForm.$setPristine();
                $scope.obj = {};
                $scope.ErrorView = false;
            }, function (error) {
                if (error == 409) (
                    $scope.ErrorPost = "Wrong Request: " + $scope.surname + " it's already in List!",
                    $scope.ErrorView = true,
                    $interval(function(){
                        $scope.ErrorView = false;
                    },3000,1)                   
                )                
            });
        }

        var MyPlayersList = function () {
            var promise = sList.GetMyPlayers();
            promise.then(function (response) {
                $scope.MyPlayers = response;
            });
        }

        MyPlayersList();

        function ToMyList(name, surname, position, team) {
            var promise = sList.MyListPost(name, surname, position, team);
            promise.then(function (response) {
                MyPlayersList();
            }, function (error) {
                if (error == 409) (
                $scope.ErrorPost = "Wrong Request: " + surname + " it's already in List!",
                $scope.ErrorView = true,
                $interval(function () {
                    $scope.ErrorView = false;
                }, 3000, 1)
            )
            });
        }

        function RemovePlayer(player) {
            var promise = sList.Remove(player);
            promise.then(function (response) {
                MyPlayersList();
            });
        }

        function filter(name) {
            if ($scope.filterOf == name) {
                $scope.filterOf = "-" + name;
            }
            else $scope.filterOf = name;
        }

        $scope.AddPlayer = AddPlayer;
        $scope.ToMyList = ToMyList;
        $scope.RemovePlayer = RemovePlayer;
        $scope.ErrorView = false;
        $scope.filter = filter;
        $scope.filterOf = '';
        $scope.obj = {};
    }
})();

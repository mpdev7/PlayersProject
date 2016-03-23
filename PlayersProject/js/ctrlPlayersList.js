(function () {
    'use strict';

    angular
        .module('app', [])
        .controller('ctrlPlayersList', ctrlPlayersList);

    ctrlPlayersList.$inject = ['$scope', 'sList','$interval']; 

    function ctrlPlayersList($scope, sList, $interval) {
        $scope.title = 'Players List';        
        
        var playerlist = function() {
            var promise = sList.GetPlayersList();
            promise.then(function (response) {
                $scope.players = response;
            }, function (error) {
                if (error == 500) (
                    $scope.ErrorPost = "Wrong Get Request, status: 500",
                    $scope.ErrorView = true,
                    $interval(function () {
                        $scope.ErrorView = false;
                    }, 3000, 1)
                )
            });
        }
              
        var AddPlayer = function AddPlayer (){
            var promise = sList.ListPost($scope.obj.name, $scope.obj.surname, $scope.obj.position, $scope.obj.team);
            promise.then(function (response) {
                playerlist();
                $scope.insertForm.$setPristine();
                $scope.obj = {};
                $scope.ErrorView = false;
            }, function (error) {
                if (error == 409) (
                    $scope.ErrorPost = "Wrong Request: " + $scope.obj.surname + " it's already in List!",
                    $scope.ErrorView = true,
                    $interval(function(){
                        $scope.ErrorView = false;
                    },3000,1)                   
                )
                if (error == 400) (
                    $scope.ErrorPost = "Bad Request!",
                    $scope.ErrorView = true,
                    $interval(function () {
                        $scope.ErrorView = false;
                    }, 3000, 1)
                )
            });
        }

        var MyPlayersList = function (id) {
            var promise = sList.GetMyPlayers(id);
            promise.then(function (response) {
                $scope.MyPlayers = response;
            });
        }
        
        function ToMyList(idPlayer, idList, surname) {
            var promise = sList.MyListPost(idPlayer, idList);
            promise.then(function (response) {
                MyPlayersList(idList);
                playerlist();
            }, function (error) {
                    if (error == 409) (
                    $scope.ErrorPost = "Wrong Request: " + surname + " it's already in List!",
                    $scope.ErrorView = true,
                    $interval(function () {
                        $scope.ErrorView = false;
                    }, 3000, 1)
                    )
                    if (error == 400) (
                        $scope.ErrorPost = "Bad Request!",
                        $scope.ErrorView = true,
                        $interval(function () {
                            $scope.ErrorView = false;
                        }, 3000, 1)
                    )
            });
        }

        function AddList() {
            var promise = sList.AddList($scope.newlist);

            promise.then(function (response) {
                $scope.newlist = '';
                $scope.listForm.$setPristine();
                GetLists();
            }, function (error) {
                if (error == 409) {
                    $scope.ErrorPost = "Wrong request " + $scope.newlist + " it's already your list!";
                    $scope.ErrorView = true;
                    $interval(function () {
                        $scope.ErrorView = false;
                    }, 3000, 1);
                }
            });
        }

        function GetLists() {
            var promise = sList.GetLists();

            promise.then(function (response) {
                $scope.lists = response;
                defaultSelect();
            }, function (error) {
                if (error == 500) {
                    $scope.ErrorPost = "Wrong Get Request, status: 500",
                    $scope.ErrorView = true,
                    $interval(function () {
                        $scope.ErrorView = false;
                    }, 3000, 1);
                }
            });
        }

        function RemovePlayer(idPlayer, idList) {
            var promise = sList.RemoveMyPlayer(idPlayer, idList);
            promise.then(function (response) {
                MyPlayersList(idList);
                playerlist();
            });
        }

        function filter(name) {
            if ($scope.filterOf == name) {
                $scope.filterOf = "-" + name;
            }
            else $scope.filterOf = name;
        }

        function defaultSelect() {
            if ($scope.lists != undefined) {
                $scope.lSelect = $scope.lists[0].Id;
                MyPlayersList($scope.lists[0].Id);
            }
        }

        playerlist();
        GetLists();
        
        $scope.lSelect = null;
        $scope.AddPlayer = AddPlayer;
        $scope.ToMyList = ToMyList;
        $scope.RemovePlayer = RemovePlayer;
        $scope.AddList = AddList;
        $scope.MyPlayerList = MyPlayersList;
        $scope.ErrorView = false;
        $scope.filter = filter;
        $scope.filterOf = '';
        $scope.obj = {};
    }
})();

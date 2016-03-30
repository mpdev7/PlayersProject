(function () {
    'use strict';

    angular
        .module('app', [])
        .controller('ctrlPlayersList', ctrlPlayersList);

    ctrlPlayersList.$inject = ['$scope', 'sList','$interval']; 

    function ctrlPlayersList($scope, sList, $interval) {
        $scope.title = 'Players List';
        $scope.position = ["Goalkeeper", "Cornerback", "Midfielder", "Wing", "Forward"];

        //Fetch player list from the server
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
              
        //Add new player to list 
        var AddPlayer = function AddPlayer (){
            var promise = sList.ListPost($scope.obj.name, $scope.obj.surname, $scope.obj.position, $scope.obj.team);
            promise.then(function (response) {
                playerlist();
                $scope.insertForm.$setPristine();
                $scope.obj = {};
                defaultPositionSelect();
                $scope.ErrorView = false;
            }, function (error) {
                if (error == 409) (
                    $scope.ErrorPost = "Wrong Request: this player it's already in List!",
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

        //Fetch player of the selected list
        var MyPlayersList = function (id) {
            var promise = sList.GetMyPlayers(id);
            promise.then(function (response) {
                $scope.MyPlayers = response;
            });
        }
        
        //Select the player from the list and put it on the personal list selected
        function ToMyList(idPlayer, idList, surname) {
            var promise = sList.MyListPost(idPlayer, idList);
            promise.then(function (response) {
                MyPlayersList(idList);
                playerlist();
            }, function (error) {
                    if (error == 409) (
                    $scope.ErrorPost = "Wrong Request: this player it's already in List!",
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

        //Add new personal list
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

        //Fetch the personal lists
        function GetLists() {
            var promise = sList.GetLists();

            promise.then(function (response) {
                $scope.lists = response;
                defaultListSelect();
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

        //Remove player from the personal list
        function RemovePlayer(idPlayer, idList) {
            var promise = sList.RemoveMyPlayer(idPlayer, idList);
            promise.then(function (response) {
                MyPlayersList(idList);
                playerlist();
            });
        }

        //Filter the player list 
        function filter(name) {
            if ($scope.filterOf == name) {
                $scope.filterOf = "-" + name;
            }
            else $scope.filterOf = name;
        }

        //Selected the first list from the personal lists
        function defaultListSelect() {
            if ($scope.lists != undefined) {
                $scope.lSelect = $scope.lists[0].Id;
                MyPlayersList($scope.lists[0].Id);
            }
        }

        //Select the first position from the position options 'Goalkeeper'
        function defaultPositionSelect() {
            $scope.obj = { "position": $scope.position[0] };
        }

        playerlist();
        GetLists();
        defaultPositionSelect();
        
        //Dinamically change of the button class 'Add player'
        function changeMyListClass() {
            if ($scope.myListClass == "btn btn-danger") {
                $scope.myListClass = "btn btn-success";
                $scope.myListSpanClass = "glyphicon glyphicon-plus-sign";
            }
            else
            {
                $scope.myListClass = 'btn btn-danger';
                $scope.myListSpanClass = "glyphicon glyphicon-remove-sign";
            }
        }

        //Dinamically change of the button class 'Add list'
        function changeListClass() {
            if ($scope.btnListClass == 'btn btn-success') {
                $scope.btnListClass = 'btn btn-danger';
                $scope.btnListSpanClass = 'glyphicon glyphicon-remove-sign';
            }
            else {
                $scope.btnListClass = 'btn btn-success';
                $scope.btnListSpanClass = 'glyphicon glyphicon-plus-sign';
            }
        }

        $scope.lSelect = null;
        $scope.AddPlayer = AddPlayer;
        $scope.ToMyList = ToMyList;
        $scope.RemovePlayer = RemovePlayer;
        $scope.AddList = AddList;
        $scope.MyPlayerList = MyPlayersList;
        $scope.ErrorView = false;
        $scope.filter = filter;
        $scope.filterOf = '';

        $scope.myListClass = 'btn btn-success';
        $scope.myListSpanClass = 'glyphicon glyphicon-plus-sign';
        $scope.btnListClass = 'btn btn-success';
        $scope.btnListSpanClass = 'glyphicon glyphicon-plus-sign';
        $scope.changeMyListClass = changeMyListClass;
        $scope.changeListClass = changeListClass;
    }
})();

(function () {
    'use strict';

    angular
        .module('app')
        .service('sList', sList);

    sList.$inject = ['$http','$q'];

    function sList($http, $q) {

        this.GetPlayersList = function() {
            var deferred = $q.defer();

            $http.get("http://localhost:52861/api/Players/").then(function (response) {
                deferred.resolve(response.data);
            });

            return deferred.promise;
        }

        this.GetMyPlayers = function () {
            var deferred = $q.defer();

            $http.get("http://localhost:52861/api/MyPlayers").then(function (response) {
                deferred.resolve(response.data);
            });

            return deferred.promise;
        }

        this.ListPost = function (name, surname, position, team) {
            
            var deferred = $q.defer();

            var NewPlayer = {Name: name, Surname: surname, Position: position, Team: team};

            $http.post("http://localhost:52861/api/Players/", NewPlayer).then(function (response) {
                deferred.resolve(response.data);
            }, function (error) {
                deferred.reject(error.status);
            });

            return deferred.promise;
        }

        this.MyListPost = function (name, surname, position, team) {
            
            var deferred = $q.defer();

            var NewPlayer = { Name: name, Surname: surname, Position: position, Team: team };

            $http.post("http://localhost:52861/api/MyPlayers/", NewPlayer).then(function (response) {
                deferred.resolve(response.data);
            }, function (error) {
                deferred.reject(error.status);
            });

            return deferred.promise;
        }

        this.RemoveMyPlayer = function (player) {
            var deferred = $q.defer();

            $http.put("http://localhost:52861/api/MyPlayers/", player).then(function (response) {
                deferred.resolve(response.data);
            });

            return deferred.promise;
        }
    }
})();
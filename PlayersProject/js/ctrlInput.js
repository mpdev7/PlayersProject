(function () {
    'use strict';

    angular.module('app').controller('ctrlInput', ctrlInput);

    ctrlInput.$inject = ['$scope']; 

    function ctrlInput($scope) {
        console.log("name: " + $scope.name);

    }
})();

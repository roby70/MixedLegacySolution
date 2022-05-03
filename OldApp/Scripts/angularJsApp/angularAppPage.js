'use strict';
(function(angular) {
    var app = angular.module('mlsApp');
    app.controller('mainPageController', mainPageController);

    mainPageController.$inject = ['$scope', '$http'];
    function mainPageController($scope, $http) {
        $scope.title = "Secured AngularJS page";

        $scope.loading = true;
        $scope.error = false;

        var mvcGet = $http.get("/cars/get");
        var apiGet = $http.get("/api/cars");

        Promise.all([mvcGet, apiGet])
            .then(function(results) {
                $scope.cars = results[0].data;
                $scope.cars.push(...results[1].data);
                $scope.error = false;
                $scope.loading = false;
                $scope.$apply();
            })
            .catch(error => {
                $scope.error = true;
                $scope.loading = false;
                $scope.$apply();
            });
    }
})(angular);
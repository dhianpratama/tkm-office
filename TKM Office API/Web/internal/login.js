angular.module('SmartShelve')
    .controller('LoginCtrl', function ($scope, $window, AuthService) {

        $scope.loginData = {
            userName: "",
            password: ""
        };

        $scope.message = "";

        $scope.login = function () {
            AuthService.login($scope.loginData).then(function (response) {
                    $window.location = "/Web/index.html#/";
                },
                function (err) {
                    $scope.message = err.error_description;
                });
        };
    });
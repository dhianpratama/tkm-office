angular.module('SmartShelve')
    .controller('NavigationCtrl', function ($scope) {
        $scope.toggleMenu = {
            masterData: false,
            transMonitor: false,
            report: false,
            user: false,
            setting: false
        };
    });

angular.module('SmartShelve')
    .controller('BreadcrumbCtrl', function ($scope, BreadcrumbService) {
        var update = function () {
            $scope.crumbs = BreadcrumbService.getCrumbs();
        };
        BreadcrumbService.setUpdateCrumbsCallback(update);
    });
angular.module('SmartShelve').controller('DashboardCtrl', function ($scope, BreadcrumbService, $window, $rootScope) {
    BreadcrumbService.addCrumb('Dashboard');
    BreadcrumbService.updateCrumbs();
    $scope.brandQuantity = {
        labels: [],
        series: [],
        data: [],
        total: 0
    };

    $scope.chartOptions = {
        scaleGridLineColor: "rgba(255,255,255,1)",
        scaleFontColor: "rgba(255,255,255,1)",
        animation: false
    };

    Chart.defaults.global.colours = [
        '#97BBCD', // blue
        '#DCDCDC', // light grey
        '#F7464A', // red
        '#46BFBD', // green
        '#FDB45C', // yellow
        '#949FB1', // grey
        '#66B760'  // dark grey
    ];

    var updateChart = function (data) {
        $rootScope.safeApply(function () {
            $scope.brandQuantity.labels = data.Labels;
            $scope.brandQuantity.series = data.Series;
            $scope.brandQuantity.data = data.Data;
            $scope.brandQuantity.total = data.Total;
        });
    };

    var itemQtyHub = $.connection.itemQuantityHub;
    itemQtyHub.client.updateBrandQuantity = updateChart;
    //$.connection.hub.logging = true;
    //$.connection.hub.start({ transport: 'longPolling' }).done(function () {
    $.connection.hub.start().done(function () {
        itemQtyHub.server.getData().done(updateChart);
    });

    $scope.$on("$destroy", function () {
        $.connection.hub.stop();
    });
});
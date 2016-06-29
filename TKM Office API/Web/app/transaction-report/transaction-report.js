angular.module('SmartShelve').controller('TransactionReportCtrl', function ($scope, BreadcrumbService) {
    BreadcrumbService.addCrumb('Report');
    BreadcrumbService.addCrumb('Transaction');
    BreadcrumbService.updateCrumbs();
    $scope.today = new Date();
    $scope.dtFrom = { opened: false };
    $scope.dtTo = { opened: false };

    $scope.param = {
        DateFrom: $scope.today,
        DateTo: $scope.today
    };

    $scope.onGenerateReport = function () {
        $window.open('/Web/sample_report/sales-general.xlsx', '_blank');
    };

});
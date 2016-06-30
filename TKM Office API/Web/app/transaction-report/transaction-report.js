angular.module('SmartShelve').controller('TransactionReportCtrl', function ($scope, $uibModal, toastr, NumberService, BreadcrumbService, SysMessageService, TransactionReportService) {
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

    $scope.types = {
        1: 'Income',
        2: 'Outcome'
    };

    $scope.fetchData = function () {
        var jsonResult = TransactionReportService.GetReportData($scope.param, function () {
            $scope.transactions = [];
            var listData = jsonResult.Data;
            $scope.transactions = listData;
        }, function (error) {
            toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
        });
    };

    $scope.formatMoney = function (v, c, d, t) {
        return NumberService.formatMoney(v, c, d, t);
    };

    var openFormModal = function (data) {
        $uibModal.open({
            templateUrl: 'app/transaction-report/transaction-approval/transaction-approval.html',
            controller: 'TransactionApprovalCtrl',
            size: 'md',
            windowClass: 'padding-top-modal',
            backdrop: 'static',
            resolve: {
                transactionData: function () {
                    return data;
                }
            }
        }).result.then(function (result) {
            if (result.IsSuccess) {
                toastr.success(SysMessageService.getSaveSuccessMsg());
            } else {
                toastr.error(SysMessageService.getSaveErrorMsg(result.Message));
            }
            $scope.fetchData();
        });
    };

    $scope.onView = function (data) {
        openFormModal(data);
    };
});
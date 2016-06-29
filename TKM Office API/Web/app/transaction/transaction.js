angular.module('SmartShelve').controller('TransactionCtrl', function ($scope, $uibModal, toastr, BreadcrumbService, SearchQueryService, SysMessageService, TransactionService) {
    BreadcrumbService.addCrumb('Transaction');
    BreadcrumbService.updateCrumbs();
    $scope.brands = [];
    $scope.searchQuery = SearchQueryService.init('TransactionCode', ['Remarks', 'CreatedBy']);

    var openFormModal = function (data) {
        $uibModal.open({
            templateUrl: 'app/transaction/transaction-form/transaction-form.html',
            controller: 'TransactionFormCtrl',
            size: 'lg',
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

    $scope.onAdd = function () {
        openFormModal({});
    };

    $scope.onEdit = function (data) {
        openFormModal(data);
    };

    $scope.onDelete = function (data) {
        if (confirm(SysMessageService.getDeleteConfirmationMsg(data.BrandName))) {
            var jsonResult = TransactionService.Delete(data, function () {
                toastr.success(SysMessageService.getDeleteSuccessMsg());
                $scope.fetchData();
            }, function (error) {
                toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
            });
        }
    };

    $scope.fetchData = function () {
        var jsonResult = TransactionService.FetchAllWithPagination($scope.searchQuery, function () {
            $scope.brands = [];
            var listData = jsonResult.Data;
            $scope.searchQuery.TotalData = jsonResult.TotalData;
            $scope.transactions = listData;
        }, function (error) {
            toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
        });
    };

    $scope.fetchData();

    $scope.displayDate = function (date) {
        return moment(date).format('DD/MM/YYYY');
    };

    $scope.types = {
        1: 'Income',
        2: 'Outcome'
    };

    $scope.formatMoney = function (v, c, d, t) {
        var n = v,
            c = isNaN(c = Math.abs(c)) ? 2 : c,
            d = d == undefined ? "." : d,
            t = t == undefined ? "," : t,
            s = n < 0 ? "-" : "",
            i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
            j = (j = i.length) > 3 ? j % 3 : 0;
        return 'Rp. ' + s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
    };
});
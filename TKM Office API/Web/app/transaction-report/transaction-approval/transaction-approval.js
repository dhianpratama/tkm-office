angular.module('SmartShelve').controller('TransactionApprovalCtrl', function ($scope, $uibModalInstance, toastr, transactionData, SysMessageService, TransactionService) {
    $scope.data = transactionData;

    $scope.onApprove = function () {
        var jsonResult = TransactionService.ApproveTransaction($scope.data.TransactionId,
            function () {
                $uibModalInstance.close({ IsSuccess: true });
            }, function (error) {
                $uibModalInstance.close({ IsSuccess: false, Message: error.data.Message });
            })
    };

});
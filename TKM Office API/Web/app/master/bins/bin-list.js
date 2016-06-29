angular.module('SmartShelve')
    .controller('BinListCtrl', function($scope, $uibModal, BinService, toastr, SysMessageService, SearchQueryService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Bin');
        BreadcrumbService.updateCrumbs();
        $scope.bins = [];
        $scope.searchQuery = SearchQueryService.init('BinCode', ['BinCode']);

        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/bins/bin-form/bin-form.html',
                controller: 'BinFormCtrl',
                windowClass: 'padding-top-modal',
                size: 'lg',
                backdrop: 'static',
                resolve: {
                    binData: function(){
                        return data;
                    }
                }
            }).result.then(function(result){
                if (result.IsSuccess){
                    toastr.success(SysMessageService.getSaveSuccessMsg());
                } else {
                    toastr.error(SysMessageService.getSaveErrorMsg(result.Message));
                }
                $scope.fetchData();
            });
        };

        $scope.onAdd = function(){
            openFormModal({});
        };

        $scope.onEdit = function(data) {
            openFormModal(data);
        };

        $scope.onDelete = function(data) {
            if (confirm(SysMessageService.getDeleteConfirmationMsg(data.BinCode))) {
                var jsonResult = BinService.Delete(data, function () {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.fetchData = function(){
            var jsonResult = BinService.FetchAllWithPagination($scope.searchQuery, function(){
                $scope.bins = [];
                var listData = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.bins = listData;
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();

    });
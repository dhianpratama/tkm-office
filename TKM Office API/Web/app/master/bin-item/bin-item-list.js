angular.module('SmartShelve')
    .controller('BinItemListCtrl', function($scope, $uibModal, BinItemService, toastr, SysMessageService, SearchQueryService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Bin Item Mapping');
        BreadcrumbService.updateCrumbs();
        $scope.binItems = [];
        $scope.searchQuery = SearchQueryService.init('Bin.BinCode', ['Bin.BinCode', 'Item.ItemCode']);

        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/bin-item/bin-item-form/bin-item-form.html',
                controller: 'BinItemFormCtrl',
                windowClass: 'padding-top-modal',
                size: 'lg',
                backdrop: 'static',
                resolve: {
                    binItemData: function(){
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
            if (confirm(SysMessageService.getDeleteConfirmationMsg(binItem.Bin.BinCode + " - " + binItem.Item.ItemCode))) {
                var jsonResult = BinItemService.Delete(data, function () {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.fetchData = function(){
            var jsonResult = BinItemService.FetchAllWithPagination($scope.searchQuery, function(){
                $scope.binItems = [];
                var listData = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.binItems = listData;
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();

    });
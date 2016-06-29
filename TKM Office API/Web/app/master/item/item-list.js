angular.module('SmartShelve')
    .controller('ItemListCtrl', function($scope, $uibModal, ItemService, toastr, SysMessageService, SearchQueryService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Item');
        BreadcrumbService.updateCrumbs();
        $scope.items = [];
        $scope.searchQuery = SearchQueryService.init('ItemCode', ['ItemCode', 'ItemName']);

        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/item/item-form/item-form.html',
                controller: 'ItemFormCtrl',
                size: 'lg',
                windowClass: 'padding-top-modal',
                backdrop: 'static',
                resolve: {
                    itemData: function(){
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
            var data = {
                ItemId: 0
            };
            openFormModal(data);
        };

        $scope.onEdit = function(data) {
            openFormModal(data);
        };

        $scope.onDelete = function(data) {
            if (confirm(SysMessageService.getDeleteConfirmationMsg(data.ItemCode))) {
                var jsonResult = ItemService.Delete(data, function () {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.fetchData = function(){
            var jsonResult = ItemService.FetchAllWithPagination($scope.searchQuery, function(){
                $scope.items = [];
                var listData = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.items = listData;
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();
    });
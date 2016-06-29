angular.module('SmartShelve')
    .controller('UomListCtrl', function($scope, $uibModal, UomService, toastr, SysMessageService, SearchQueryService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('UOM');
        BreadcrumbService.updateCrumbs();
        $scope.uoms = [];
        $scope.searchQuery = SearchQueryService.init('UomCode', ['UomCode', 'UomDescription']);

        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/uom/uom-form/uom-form.html',
                controller: 'UomFormCtrl',
                size: 'lg',
                windowClass: 'padding-top-modal',
                backdrop: 'static',
                resolve: {
                    uomData: function(){
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
            if (confirm(SysMessageService.getDeleteConfirmationMsg(data.UomCode))) {
                var jsonResult = UomService.Delete(data, function () {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.fetchData = function(){
            var jsonResult = UomService.FetchAllWithPagination($scope.searchQuery, function(){
                $scope.uoms = [];
                var listData = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.uoms = listData;
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();

    });
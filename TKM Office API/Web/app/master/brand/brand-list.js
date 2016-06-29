angular.module('SmartShelve')
    .controller('BrandListCtrl', function($scope, $uibModal, BrandService, toastr, SysMessageService, SearchQueryService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Brand');
        BreadcrumbService.updateCrumbs();
        $scope.brands = [];
        $scope.searchQuery = SearchQueryService.init('BrandCode', ['BrandCode', 'BrandName']);

        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/brand/brand-form/brand-form.html',
                controller: 'BrandFormCtrl',
                size: 'md',
                windowClass: 'padding-top-modal',
                backdrop: 'static',
                resolve: {
                    brandData: function(){
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
            if (confirm(SysMessageService.getDeleteConfirmationMsg(data.BrandName))) {
                var jsonResult = BrandService.Delete(data, function () {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.fetchData = function(){
            var jsonResult = BrandService.FetchAllWithPagination($scope.searchQuery, function(){
                $scope.brands = [];
                var listData = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.brands = listData;
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();

    });
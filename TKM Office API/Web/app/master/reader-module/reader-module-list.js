angular.module('SmartShelve')
    .controller('ReaderModuleListCtrl', function($scope, $uibModal, ReaderModuleService, toastr, SysMessageService, SearchQueryService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Reader Module');
        BreadcrumbService.updateCrumbs();
        $scope.modules = [];
        $scope.searchQuery = SearchQueryService.init('ReaderModuleCode', ['ReaderModuleCode']);

        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/reader-module/reader-module-form/reader-module-form.html',
                controller: 'ReaderModuleFormCtrl',
                windowClass: 'padding-top-modal',
                size: 'lg',
                backdrop: 'static',
                resolve: {
                    moduleData: function(){
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
            var data = {};
            data.NoOfRow = 2;
            openFormModal(data);
        };

        $scope.onEdit = function(data) {
            openFormModal(data);
        };

        $scope.onDelete = function(data) {
            if (confirm(SysMessageService.getDeleteConfirmationMsg(data.ReaderModuleCode))) {
                var jsonResult = ReaderModuleService.Delete(data, function () {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.fetchData = function(){
            var jsonResult = ReaderModuleService.FetchAllWithPagination($scope.searchQuery, function(){
                $scope.modules = [];
                var listData = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.modules = listData;
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();

    });
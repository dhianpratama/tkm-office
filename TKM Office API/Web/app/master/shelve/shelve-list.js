angular.module('SmartShelve')
    .controller('ShelveListCtrl', function($scope, $uibModal, ShelveService, toastr, SysMessageService, SearchQueryService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Shelve');
        BreadcrumbService.updateCrumbs();
        $scope.shelves = [];
        $scope.searchQuery = SearchQueryService.init('ShelveCode', ['ShelveCode']);

        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/shelve/shelve-form/shelve-form.html',
                controller: 'ShelveFormCtrl',
                windowClass: 'padding-top-modal',
                size: 'lg',
                backdrop: 'static',
                resolve: {
                    shelveData: function(){
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
            if (confirm(SysMessageService.getDeleteConfirmationMsg(data.ShelveCode))) {
                var jsonResult = ShelveService.Delete(data, function () {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.fetchData = function(){
            var jsonResult = ShelveService.FetchAllWithPagination($scope.searchQuery, function(){
                $scope.shelves = [];
                var listData = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.shelves = listData;
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();

    });
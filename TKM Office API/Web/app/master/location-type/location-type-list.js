angular.module('SmartShelve')
    .controller('LocationTypeListCtrl', function($scope, $uibModal, LocationTypeService, toastr, SysMessageService, SearchQueryService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Location Type');
        BreadcrumbService.updateCrumbs();
        $scope.locationTypes = [];
        $scope.searchQuery = SearchQueryService.init('LocationTypeCode', ['LocationTypeCode', 'LocationTypeDescription']);

        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/location-type/location-type-form/location-type-form.html',
                controller: 'LocationTypeFormCtrl',
                size: 'md',
                windowClass: 'padding-top-modal',
                backdrop: 'static',
                resolve: {
                    locTypeData: function(){
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
            if (confirm(SysMessageService.getDeleteConfirmationMsg(data.LocationTypeDescription))) {
                var jsonResult = LocationTypeService.Delete(data, function () {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.fetchData = function(){
            var jsonResult = LocationTypeService.FetchAllWithPagination($scope.searchQuery, function(){
                $scope.locationTypes = [];
                var listData = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.locationTypes = listData;
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();

    });
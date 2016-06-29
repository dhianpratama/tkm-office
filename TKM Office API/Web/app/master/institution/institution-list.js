angular.module('SmartShelve')
    .controller('InstitutionListCtrl', function($scope, $uibModal, SearchQueryService, InstitutionService, toastr, SysMessageService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Institution');
        BreadcrumbService.updateCrumbs();
        $scope.institutions = [];
        $scope.searchQuery = SearchQueryService.init('InstitutionName', ['InstitutionName', 'InstitutionDescription']);

        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/institution/institution-form/institution-form.html',
                controller: 'InstitutionFormCtrl',
                size: 'lg',
                windowClass: 'padding-top-modal',
                backdrop: 'static',
                resolve: {
                    institutionData: function(){
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
            if (confirm(SysMessageService.getDeleteConfirmationMsg(data.InstitutionName))) {
                var jsonResult = InstitutionService.Delete(data, function() {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.fetchData = function(){
            var jsonResult = InstitutionService.FetchAllWithPagination($scope.searchQuery, function(){
                $scope.institutions = [];
                var listData = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.institutions = listData;
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();

    });
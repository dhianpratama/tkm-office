angular.module('SmartShelve')
    .controller('UserListCtrl', function($scope, $uibModal, UserService, toastr, SysMessageService, SearchQueryService, BreadcrumbService){
    BreadcrumbService.addCrumb('User Management');
    BreadcrumbService.addCrumb('User');
    BreadcrumbService.updateCrumbs();

    $scope.users = [];
    $scope.searchQuery = SearchQueryService.init('UserName', ['UserName', 'FullName']);

    var openEditFormModal = function(data){
        $uibModal.open({
            templateUrl: 'app/user-mgt/user/user-form/user-form.html',
            controller: 'UserFormCtrl',
            size: 'lg',
            windowClass: 'padding-top-modal',
            backdrop: 'static',
            resolve: {
                userData: function(){
                    return angular.copy(data);
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

    var openAddFormModal = function(){
        $uibModal.open({
            templateUrl: 'app/user-mgt/user/create-user-form/create-user-form.html',
            controller: 'CreateUserFormCtrl',
            size: 'lg',
            windowClass: 'padding-top-modal',
            backdrop: 'static'
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
        openAddFormModal();
    };

    $scope.onEdit = function(data) {
        openEditFormModal(data);
    };

    $scope.onDelete = function(data) {
        if (confirm(SysMessageService.getDeleteConfirmationMsg(data.UserName))) {
            var jsonResult = UserService.Delete(data, function () {
                toastr.success(SysMessageService.getDeleteSuccessMsg());
                $scope.fetchData();
            }, function(error){
                toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
            });
        }
    };

    $scope.fetchData = function(){
        var jsonResult = UserService.FetchAllWithPagination($scope.searchQuery, function(){
            $scope.users = [];
            var listData = jsonResult.Data;
            $scope.searchQuery.TotalData = jsonResult.TotalData;
            $scope.users = listData;
        }, function(error){
            toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
        });
    };

    $scope.fetchData();

});
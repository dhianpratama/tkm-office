angular.module('SmartShelve')
    .controller('LocationListCtrl',function($scope, $uibModal, LocationService, toastr, SysMessageService, BreadcrumbService){
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Location');
        BreadcrumbService.updateCrumbs();
        $scope.flatLocations = [];
        var openFormModal = function(data){
            $uibModal.open({
                templateUrl: 'app/master/location/location-form/location-form.html',
                controller: 'LocationFormCtrl',
                size: 'md',
                windowClass: 'padding-top-modal',
                backdrop: 'static',
                resolve: {
                    locData: function(){
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

        $scope.onAdd = function(parentLocationId){
            var loc = {
                LocationId: 0,
                ParentLocationId: parentLocationId,
                LocationCode: "",
                LocationName: "",
                LocationTypeId: 0
            };
            openFormModal(loc);
        };

        $scope.onEdit = function(data) {
            openFormModal(data);
        };

        $scope.onDelete = function(data) {
            if (confirm(SysMessageService.getDeleteConfirmationMsg(data.LocationName))) {
                var jsonResult = LocationService.Delete(data, function () {
                    toastr.success(SysMessageService.getDeleteSuccessMsg());
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getDeleteErrorMsg(error.data.Message));
                });
            }
        };

        $scope.flatToHierarchy =  function (flat) {
            var roots = [];
            var all = {};

            flat.forEach(function(item) {
                item.ShowDescendant = false;
                item.Descendants = [];
                all[item.LocationId] = item;
            });

            // connect childrens to its parent, and split roots apart
            Object.keys(all).forEach(function (LocationId) {
                var item = all[LocationId];
                if (item.ParentLocationId === null){
                    roots.push(item);
                } else if (item.ParentLocationId in all) {
                    var p = all[item.ParentLocationId];
                    if (!('Descendants' in p)) {
                        p.Descendants = [];
                    }
                    p.Descendants.push(item);
                }
            });

            // done!
            return roots;
        };

        $scope.fetchData = function(){
            var jsonResult = LocationService.FetchAllByInstitution(function(){
                $scope.flatLocations = [];
                $scope.flatLocations = jsonResult;
                $scope.locations = $scope.flatToHierarchy($scope.flatLocations);
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData();

    });
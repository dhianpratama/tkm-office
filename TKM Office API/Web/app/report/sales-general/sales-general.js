angular.module('SmartShelve')
    .controller('SalesGeneralCtrl', function($scope, $window, toastr, SysMessageService, BreadcrumbService,
                                             LocationService){
        BreadcrumbService.addCrumb('Report');
        BreadcrumbService.addCrumb('Sales');
        BreadcrumbService.updateCrumbs();
        $scope.today = new Date();
        $scope.dtFrom = {opened:false};
        $scope.dtTo = {opened:false};

        $scope.param = {
            LocationId: null,
            DateFrom: $scope.today,
            DateTo: $scope.today
        };

        var jsonLocation = LocationService.FetchAllForDropdownList(function(){
            $scope.locations = jsonLocation;
            $scope.locations.splice(0,0,{LocationId: null, Description:"[ALL]"});
        }, function(error){
            toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
        });

        $scope.onGenerateReport = function(){
            $window.open('/Web/sample_report/sales-general.xlsx', '_blank');
        };

    });
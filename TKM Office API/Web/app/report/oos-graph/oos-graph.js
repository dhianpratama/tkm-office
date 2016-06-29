angular.module('SmartShelve')
    .controller('OosGraphCtrl',function($scope, $rootScope, toastr, SysMessageService, SearchQueryService, BreadcrumbService,
                                        LocationService, ShelveService, ReaderModuleService, BinService, ItemService, BrandService,
                                        StockCardOutOfStockService){
        $scope.advSearchPanel = {open: false};

        BreadcrumbService.addCrumb('Report');
        BreadcrumbService.addCrumb('Out of Stock Graph');
        BreadcrumbService.updateCrumbs();
        $scope.today = new Date();
        $scope.dtFrom = {opened:false};
        $scope.dtTo = {opened:false};

        $scope.scaleTypeOptions = [
            {value:1, text:"Daily"},
            {value:2, text:"Weekly"},
            {value:3, text:"Monthly"},
            {value:4, text:"Yearly"}
        ];

        $scope.oos = {
            Labels: [],
            Series: [],
            Data: [],
            Total: 0,
            HourPeriod: 0,
            MinutePeriod: 0
        };

        $scope.chartOptions = {
            scaleGridLineColor: "rgba(255,255,255,1)",
            scaleFontColor: "rgba(255,255,255,1)",
            animation: false
        };

        Chart.defaults.global.colours = [
            '#97BBCD', // blue
            '#DCDCDC', // light grey
            '#F7464A', // red
            '#46BFBD', // green
            '#FDB45C', // yellow
            '#949FB1', // grey
            '#66B760'  // dark grey
        ];
        $scope.param = {
            LocationId: null,
            ShelveId: null,
            ReaderModuleId: null,
            BinId: null,
            ItemId: null,
            BrandId: null,
            DateFrom: $scope.today,
            DateTo: $scope.today,
            ScaleType: 1
        };

        $scope.fetchParamData = function(){
            var jsonLocation = LocationService.FetchAllForDropdownList(function(){
                $scope.locations = jsonLocation;
                $scope.locations.splice(0,0,{LocationId: null, Description:"[ALL]"});
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });

            var jsonShelve = ShelveService.FetchAll(function(){
                $scope.shelves = jsonShelve;
                $scope.shelves.splice(0,0,{ShelveId: null, ShelveCode:"[ALL]"});
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });

            var jsonReaderModule = ReaderModuleService.FetchAll(function(){
                $scope.modules = jsonReaderModule;
                $scope.modules.splice(0,0,{ReaderModuleId: null, ReaderModuleCode:"[ALL]"});
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });

            var jsonBin = BinService.FetchAll(function(){
                $scope.bins = jsonBin;
                $scope.bins.splice(0,0,{BinId: null, BinCode:"[ALL]"});
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });

            var jsonItem = ItemService.FetchAll(function(){
                $scope.items = jsonItem;
                $scope.items.splice(0,0,{ItemId: null, ItemCode:"[ALL]", ItemName:"[ALL]"});
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });

            var jsonBrand = BrandService.FetchAll(function(){
                $scope.brands = jsonBrand;
                $scope.brands.splice(0, 0, {BrandId: null, BrandName: "[ALL]"});
            }, function(error){
                toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
            });
        };

        $scope.fetchData = function (){
            var jsonResult = StockCardOutOfStockService.GetGraphOutOfStockData($scope.param, function(){
                var data = jsonResult;
                $rootScope.safeApply(function () {
                    if (data.Total > 0) {
                        $scope.oos.Labels = data.Labels;
                        $scope.oos.Series = data.Series;
                        $scope.oos.Data = data.Data;
                        $scope.oos.Total = data.Total;
                        $scope.oos.HourPeriod = Math.floor(data.Total / 60);
                        $scope.oos.MinutePeriod = data.Total % 60;
                    }
                    else {
                        $scope.oos = {
                            Labels: [],
                            Series: [],
                            Data: [],
                            Total: 0,
                            HourPeriod: 0,
                            MinutePeriod: 0
                        };
                    }
                });
            });
        };

        $scope.fetchParamData();

    });
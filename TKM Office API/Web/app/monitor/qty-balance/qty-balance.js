angular.module('SmartShelve')
    .controller('QtyBalanceCtrl',function($scope, toastr, SysMessageService, SearchQueryService, BreadcrumbService,
                                          LocationService, ShelveService, ReaderModuleService, BinService, ItemService, BrandService,
                                          StockCardService){
        BreadcrumbService.addCrumb('Transaction Monitor');
        BreadcrumbService.addCrumb('Inventory Monitor');
        BreadcrumbService.updateCrumbs();

        $scope.perPagesOptions = [10, 20, 50];
        $scope.searchQuery = SearchQueryService.init('ItemCode', ['ItemCode', 'ItemName', 'BrandName']);
        $scope.searchQuery.IsSortAsc = false;

        $scope.balances = [];
        $scope.param = {
            LocationId: null,
            ShelveId: null,
            ReaderModuleId: null,
            BinId: null,
            ItemId: null,
            BrandId: null,
            SearchQuery: $scope.searchQuery
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
            var jsonResult = StockCardService.GetQtyBalance($scope.param, function(){
                var data = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.balances = data.Details;
                $scope.totalQty = data.TotalBalanceQty;
            })
        };

        $scope.fetchParamData();

        $scope.onResetTrx = function(){
            if (confirm("Are you sure want to delete all transaction data?")){
                var jsonResult = StockCardService.ResetAllTrx(function(){
                    toastr.info("All transaction data deleted");
                    $scope.fetchData();
                }, function(error){
                    toastr.error(SysMessageService.getLoadErrorMsg(error.data.Message));
                });
            }
        };
    });
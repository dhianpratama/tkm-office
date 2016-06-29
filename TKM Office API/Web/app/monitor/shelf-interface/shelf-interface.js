angular.module('SmartShelve')
    .controller('ShelfInterfaceCtrl', function($scope, ShelveService, ItemService, BinService, $uibModal, $rootScope, $timeout, BreadcrumbService, StockCardService, SearchQueryService, $rootScope, $window) {
        BreadcrumbService.addCrumb('Monitor');
        BreadcrumbService.addCrumb('Shelf Interface');
        BreadcrumbService.updateCrumbs();
        $scope.ShelveId = 2;

        $scope.onFetchShelf = function () {
            if (!$scope.ShelveId) {
                return;
            }

            var jsonResult = ShelveService.FetchCompleteShelve({
                ShelveId: $scope.ShelveId
            }, function () {
                var data = jsonResult;
                $scope.shelf = data;
                angular.forEach($scope.shelf.Modules, function (module, key) {
                    angular.forEach(module.Bins, function (bin, key) {
                        bin.SalesQty = 0;
                        bin.InventoryQty = 0;
                    });
                });
            });
        };

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

        $scope.fetchInventoryQty = function (){
            var jsonResult = StockCardService.GetQtyBalance($scope.param, function(){
                var data = jsonResult.Data;
                $scope.searchQuery.TotalData = jsonResult.TotalData;
                $scope.balances = data.Details;
                $scope.totalQty = data.TotalBalanceQty;


                angular.forEach($scope.shelf.Modules, function(module, key){
                    angular.forEach(module.Bins, function(bin, key){
                        angular.forEach($scope.balances, function(bal, key){
                                if (bin.BinId == bal.BinId){
                                    bin.InventoryQty = bal.QtyBalance;
                                }
                        });
                    });
                });



            })
        };

        $scope.fetchSalesQty = function (){
            var jsonResult = StockCardService.GetBinQtySales(function(){
                var data = jsonResult;
                $scope.sales = data.Details;
                angular.forEach($scope.shelf.Modules, function(module, key){
                    angular.forEach(module.Bins, function (bin, key) {
                        bin.SalesQty = 0;
                        angular.forEach($scope.sales, function(sls, key){
                            if (bin.BinId == sls.BinId){
                                bin.SalesQty = sls.QtySales;
                            }
                        });
                    });
                });
            })
        };

        $scope.onFetchShelf();
        $scope.fetchInventoryQty();
        $scope.fetchSalesQty();

        var updateInventoryQty = function (data) {
            $rootScope.safeApply(function () {
                $scope.balances = data.Details;
                $scope.totalQty = data.TotalBalanceQty;
                angular.forEach($scope.shelf.Modules, function(module, key){
                    angular.forEach(module.Bins, function (bin, key) {
                        bin.InventoryQty = 0;
                        angular.forEach($scope.balances, function(bal, key){
                            if (bin.BinId == bal.BinId){
                                bin.InventoryQty = bal.QtyBalance;
                            }
                        });
                    });
                });
            });
        };

        var updateSalesQty = function (data) {
            $rootScope.safeApply(function () {
                $scope.sales = data.Details;
                angular.forEach($scope.shelf.Modules, function(module, key){
                    angular.forEach(module.Bins, function (bin, key) {
                        bin.SalesQty = 0;
                        angular.forEach($scope.sales, function(sls, key){
                            if (bin.BinId == sls.BinId){
                                bin.SalesQty = sls.QtySales;
                            }
                        });
                    });
                });
            });
        };

        var itemQtyHub = $.connection.itemQuantityHub;
        itemQtyHub.client.updateBinItemQuantity = updateInventoryQty;
        itemQtyHub.client.updateBinSalesQuantity = updateSalesQty;
        //$.connection.hub.logging = true;
        //$.connection.hub.start({ transport: 'longPolling' }).done(function () {
        $.connection.hub.start().done(function () {
            itemQtyHub.server.getBinItemQuantityData().done(updateInventoryQty);
            itemQtyHub.server.getBinItemSalesData().done(updateSalesQty);
        });

        $scope.$on("$destroy", function () {
            $.connection.hub.stop();
        });
    });
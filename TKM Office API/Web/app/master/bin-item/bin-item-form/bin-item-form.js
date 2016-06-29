angular.module('SmartShelve')
    .controller('BinItemFormCtrl', function($scope, $uibModalInstance, binItemData, BinService, ItemService, BinItemService){
        $scope.binItem = binItemData;

        $scope.onSave = function(){
            var jsonResult = BinItemService.Save($scope.binItem, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };

        var jsonBinsResult = BinService.FetchAll(function(){
            $scope.bins = jsonBinsResult;
        });

        var jsonItemResult = ItemService.FetchAll(function(){
            $scope.items = jsonItemResult;
        });

    });
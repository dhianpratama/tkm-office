angular.module('SmartShelve')
    .controller('BinFormCtrl', function($scope, $uibModalInstance, binData, ReaderModuleService, BinService){
        $scope.bin = binData;

        $scope.onSave = function(){
            var jsonResult = BinService.Save($scope.bin, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };

        var jsonShelveResult = ReaderModuleService.FetchAll(function(){
            $scope.modules = jsonShelveResult;
        });

        $scope.onDelete = function () {
            var jsonResult = BinService.Delete($scope.bin, function () {
                $uibModalInstance.close();
            });
        };

    });
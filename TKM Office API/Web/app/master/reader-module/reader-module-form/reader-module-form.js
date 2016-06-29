angular.module('SmartShelve')
    .controller('ReaderModuleFormCtrl', function($scope, $uibModalInstance, moduleData, ReaderModuleService, ShelveService){
        $scope.module = moduleData;
        $scope.noOfRowOptions = [2,4,6,8,10];
        $scope.onSave = function(){
            var jsonResult = ReaderModuleService.Save($scope.module, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };

        var jsonShelveResult = ShelveService.FetchAll(function(){
            $scope.shelves = jsonShelveResult;
        });

        $scope.onDelete = function () {
            var jsonResult = ReaderModuleService.Delete($scope.module, function () {
                $uibModalInstance.close();
            });
        };

    });
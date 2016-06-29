angular.module('SmartShelve')
    .controller('LocationTypeFormCtrl', function($scope, $uibModalInstance, locTypeData, LocationTypeService){
        $scope.locType = locTypeData;

        $scope.onSave = function(){
            var jsonResult = LocationTypeService.Save($scope.locType, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };
    });
angular.module('SmartShelve')
    .controller('LocationFormCtrl', function($scope, $uibModalInstance, locData, LocationService, LocationTypeService){
        $scope.location = locData;

        $scope.onSave = function(){
            var jsonResult = LocationService.Save($scope.location, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };

        var jsonLocType = LocationTypeService.FetchAll(function(){
           $scope.locationTypes = jsonLocType;
        });

    });
angular.module('SmartShelve')
    .controller('ShelveFormCtrl', function($scope, $uibModalInstance, shelveData, ShelveService, LocationService){
        $scope.shelve = shelveData;


        $scope.onSave = function(){
            var jsonResult = ShelveService.Save($scope.shelve, function(){
				jsonResult.IsSuccess = true;
				$uibModalInstance.close(jsonResult);
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };

        var jsonLocation = LocationService.FetchAllForDropdownList(function(){
            $scope.locations = jsonLocation;
        });

        $scope.onDelete = function () {

            var jsonResult = ShelveService.Delete($scope.shelve, function () {
                $uibModalInstance.close(jsonResult);
            });
        };

    });
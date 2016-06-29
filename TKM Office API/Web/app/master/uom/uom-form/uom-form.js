angular.module('SmartShelve')
    .controller('UomFormCtrl',function($scope, $uibModalInstance, uomData, UomService){
        $scope.uom = uomData;


        $scope.onSave = function(){
            var jsonResult = UomService.Save($scope.uom, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };
    });
angular.module('SmartShelve')
    .controller('InstitutionFormCtrl',function($scope, $uibModalInstance, institutionData, InstitutionService){
        $scope.inst = institutionData;


        $scope.onSave = function(){
            var jsonResult = InstitutionService.Save($scope.inst, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };
    });
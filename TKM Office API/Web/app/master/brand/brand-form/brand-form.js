angular.module('SmartShelve')
    .controller('BrandFormCtrl' ,function($scope, $uibModalInstance, brandData, BrandService){
        $scope.brand = brandData;


        $scope.onSave = function(){
            var jsonResult = BrandService.Save($scope.brand, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };
    });
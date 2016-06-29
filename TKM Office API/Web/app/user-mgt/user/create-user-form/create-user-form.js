angular.module('SmartShelve')
    .controller('CreateUserFormCtrl', function($scope, $uibModalInstance, UserService){
        $scope.user = {};

        $scope.onSave = function(){
            var jsonResult = UserService.CreateNewUser($scope.user, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };
    });
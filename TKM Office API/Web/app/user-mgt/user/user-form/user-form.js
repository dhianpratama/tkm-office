angular.module('SmartShelve')
    .controller('UserFormCtrl', function($scope, $uibModalInstance, userData, UserService, InstitutionService){
        $scope.user = userData;
        var hashInstitution = {};
        $scope.userInstitutions = [];
        $scope.DefaultInstitutionOptions = [];

        angular.forEach($scope.user.Institutions, function(inst, key){
            hashInstitution[inst.InstitutionId] = inst;
            if (inst.IsActive){
                $scope.DefaultInstitutionOptions.push(inst);
            }
        });

        $scope.onInstitutionChange = function(changedInstitution){
            if (changedInstitution.IsActive){
                $scope.DefaultInstitutionOptions.push(changedInstitution);
            }
            else {
                var index = 0;
                var i = 0;
                angular.forEach($scope.DefaultInstitutionOptions, function(inst, key){
                    if (inst.InstitutionId == changedInstitution.InstitutionId){
                        index = i;
                    }
                    i++;
                });
                $scope.DefaultInstitutionOptions.splice(index, 1);
            }
        };

        $scope.onSave = function(){
            var tempInstitution = angular.copy($scope.user.Institutions);
            $scope.user.Institutions = [];

            angular.forEach(tempInstitution, function(inst, key){
                if (inst.UserInstitutionId > 0 || inst.IsActive){
                    $scope.user.Institutions.push(inst);
                }
            });

            var userUpdate = {};
            userUpdate.UserId = $scope.user.UserId;
            userUpdate.UserName = $scope.user.UserName;
            userUpdate.FullName = $scope.user.FullName;
            userUpdate.DefaultInstitutionId = $scope.user.DefaultInstitutionId;
            userUpdate.Institutions = angular.copy($scope.user.Institutions);

            var jsonResult = UserService.UpdateUserData(userUpdate, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };

        $scope.onResetPassword = function(){
            var jsonResult = UserService.ResetPassword($scope.user, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };

        var jsonInstitution = InstitutionService.FetchAll(function (){
            angular.forEach(jsonInstitution, function(inst, key){
                if (!hashInstitution[inst.InstitutionId]){
                    var data = {
                        UserInstitutionId: 0,
                        Institution: inst,
                        InstitutionId: inst.InstitutionId,
                        UserId: $scope.user.UserId,
                        IsActive: false
                    };
                    $scope.user.Institutions.push(data);
                }
            });
        });
    });
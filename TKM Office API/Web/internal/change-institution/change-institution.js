angular.module('SmartShelve')
    .controller('ChangeInstitutionCtrl', function($scope, InstitutionService, $uibModalInstance, currentInstitution){

        $scope.institution = {};
        $scope.institution.institutionId = currentInstitution.InstitutionId;

        var jsonInstitution = InstitutionService.FetchAll(function(){
            $scope.institutions = jsonInstitution;
            angular.forEach($scope.institutions, function (inst, key) {
                if (inst.InstitutionId == currentInstitution.InstitutionId) {
                    $scope.institution.institutionId = inst.InstitutionId;
                }
            });
        });

        $scope.onConfirm = function(){
            if (confirm("Are you sure you want to change Institution? This will refresh the application. Please save your work first.")) {
                $scope.chosenInstitution = {};
                angular.forEach($scope.institutions, function (inst, key) {
                    if (inst.InstitutionId == $scope.institution.institutionId) {
                        $scope.chosenInstitution = inst;
                    }
                });
                $uibModalInstance.close({IsConfirm: true, ChosenInstitution: $scope.chosenInstitution});
            }
        };

    });
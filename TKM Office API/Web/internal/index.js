angular.module('SmartShelve')
    .controller('IndexCtrl', function ($scope, $window, AuthService, UserService, InstitutionService, $location, $q, $uibModal) {

        $scope.toggleMenu = false;
        $scope.toggleMenuInstitution = false;
        $scope.logOut = function () {
            AuthService.logOut();
            $window.location = "/Web/login.html#/";
        };

        $scope.editProfile = function(){
            $location.path("/editProfile");
        };
        $scope.user = {};

        $scope.getCurrentUser = function(){
            var d = $q.defer();
            var jsonUser = UserService.GetCurrentUser(function(){
                var user = jsonUser;
                d.resolve(user);
            }, function(){
                d.resolve(null);
            });
            return d.promise;
        };

        $scope.getCurrentDefaultInstitution = function(){
            var d = $q.defer();
            var jsonInstitution = UserService.GetCurrentDefaultInstitution(function(){
                var defaultInstitution = jsonInstitution;
                d.resolve(defaultInstitution);
            }, function(){
                d.resolve(null);
            });
            return d.promise;
        };

        $q.all([
            $scope.getCurrentUser(),
            $scope.getCurrentDefaultInstitution()
        ]).then(function(data){
            $scope.user = data[0];
            $scope.user.CurrentInstitution = data[1];
        });

        var openFormModal = function(){
            $uibModal.open({
                templateUrl: 'internal/change-institution/change-institution.html',
                controller: 'ChangeInstitutionCtrl',
                size: 'lg',
                windowClass: 'padding-top-modal',
                backdrop: 'static',
                resolve: {
                    currentInstitution: function(){
                        return $scope.user.CurrentInstitution;
                    }
                }
            }).result.then(function(result){
                if (result.IsConfirm){
                    $scope.user.CurrentInstitution = result.ChosenInstitution;
                    $window.reload();
                }
            });
        };

        //$scope.institutions = [];


        $scope.onChangeInstitution = function(){
            $scope.toggleMenu = false;
            openFormModal();
        };
    });
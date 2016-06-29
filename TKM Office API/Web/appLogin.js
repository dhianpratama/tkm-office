angular.module('SmartShelve', [
    'ui.bootstrap',
    'ui.utils',
    'ngRoute',
    'angular-loading-bar',
    'ngAnimate',
    'ngResource',
    'LocalStorageModule']);

angular.module('SmartShelve').config(function($routeProvider) {
    /* Add New Routes Above */
    $routeProvider.otherwise({redirectTo:'/'});

});

angular.module('SmartShelve')
    .run(function($rootScope, UserService) {
        $rootScope.safeApply = function(fn) {
            var phase = $rootScope.$$phase;
            if (phase === '$apply' || phase === '$digest') {
                if (fn && (typeof(fn) === 'function')) {
                    fn();
                }
            } else {
                this.$apply(fn);
            }
        };
        UserService.GetCurrentUser(function () {
            window.location = '/Web/index.html';
        });
    })
    .config(function ($httpProvider) {
        $httpProvider.interceptors.push('AuthInterceptorService');
    })
    .config(function (localStorageServiceProvider) {
        localStorageServiceProvider.setPrefix('SmartShelve')
    });

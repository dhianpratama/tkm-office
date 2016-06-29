angular.module('SmartShelve', [
    'ui.bootstrap',
    'ui.utils',
    'ngRoute',
    'angular-loading-bar',
    'ngAnimate',
    'ngResource',
    'LocalStorageModule',
    'cropme',
    'chart.js',
    'ngDraggable',
    'toastr',
    'al-click',
    'ui.uploader'
]);

angular.module('SmartShelve').config(function($routeProvider) {

    $routeProvider.when('/master/item',{templateUrl: 'app/master/item/item-list.html'});
    $routeProvider.when('/master/institution',{templateUrl: 'app/master/institution/institution-list.html'});
    $routeProvider.when('/master/uom',{templateUrl: 'app/master/uom/uom-list.html'});
    $routeProvider.when('/master/location-type',{templateUrl: 'app/master/location-type/location-type-list.html'});
    $routeProvider.when('/master/location',{templateUrl: 'app/master/location/location-list.html'});
    $routeProvider.when('/master/shelve',{templateUrl: 'app/master/shelve/shelve-list.html'});
    $routeProvider.when('/master/reader-module',{templateUrl: 'app/master/reader-module/reader-module-list.html'});
    $routeProvider.when('/master/bin',{templateUrl: 'app/master/bins/bin-list.html'});
    $routeProvider.when('/master/bin-item',{templateUrl: 'app/master/bin-item/bin-item-list.html'});
    $routeProvider.when('/master/brand',{templateUrl: 'app/master/brand/brand-list.html'});
    $routeProvider.when('/dashboard',{templateUrl: 'app/dashboard/dashboard.html'});
    $routeProvider.when('/sys/config',{templateUrl: 'app/sys/config/config.html'});
    $routeProvider.when('/tools/shelve-builder',{templateUrl: 'app/tools/shelve-builder/shelve-builder.html'});
    $routeProvider.when('/user-mgt/user',{templateUrl: 'app/user-mgt/user/user-list.html'});
    $routeProvider.when('/user-mgt/role',{templateUrl: 'app/user-mgt/role/role-list.html'});
    $routeProvider.when('/user-mgt/user-role',{templateUrl: 'app/user-mgt/user-role/user-role-list.html'});
    $routeProvider.when('/monitor/qty-movement',{templateUrl: 'app/monitor/qty-movement/qty-movement-monitor.html'});
    $routeProvider.when('/monitor/qty-balance',{templateUrl: 'app/monitor/qty-balance/qty-balance.html'});
    $routeProvider.when('/under-construction',{templateUrl: 'app/under-construction/under-construction.html'});
    $routeProvider.when('/report/sales-general',{templateUrl: 'app/report/sales-general/sales-general.html'});
    $routeProvider.when('/report/sales-out-of-stock',{templateUrl: 'app/report/sales-oos/sales-oos.html'});
    $routeProvider.when('/monitor/shelf-interface',{templateUrl: 'app/monitor/shelf-interface/shelf-interface.html'});
    $routeProvider.when('/report/oos-inquiry',{templateUrl: 'app/report/oos-inquiry/oos-inquiry.html'});
    $routeProvider.when('/report/oos-graph',{templateUrl: 'app/report/oos-graph/oos-graph.html'});
    $routeProvider.when('/report/sales-graph',{templateUrl: 'app/report/sales-graph/sales-graph.html'});
    $routeProvider.when('/tkm/transaction',{templateUrl: 'app/transaction/transaction.html'});
    $routeProvider.when('/tkm/report/transactionReport',{templateUrl: 'app/transaction-report/transaction-report.html'});
    /* Add New Routes Above */
    $routeProvider.otherwise({redirectTo:'/tkm/transaction'});

});

angular.module('SmartShelve')
    .run(function($rootScope, AuthService) {

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
        AuthService.fillAuthData();
    })
    .config(function ($httpProvider) {
        $httpProvider.interceptors.push('AuthInterceptorService');
    })
    .config(function (localStorageServiceProvider) {
        localStorageServiceProvider.setPrefix('SmartShelve')
    })
    .config(function(toastrConfig) {
        angular.extend(toastrConfig, {
            autoDismiss: true,
            positionClass: 'toast-top-center',
            closeButton: true,
            closeHtml: '<button>&times;</button>',
            progressBar: true,
            tapToDismiss: true,
            timeOut: 3000,
            extendedTimeOut: 500
        });
    });



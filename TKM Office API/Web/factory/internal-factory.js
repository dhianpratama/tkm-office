angular.module('SmartShelve')
    .factory('AuthService', function ($http, $q, localStorageService, UserService) {
        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            userName: ""
        };

        var _saveRegistration = function (registration) {
            _logOut();
            var jsonResult = UserService.Register(registration, function () {
                return jsonResult.data;
            });

            //return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            //    return response;
            //});

        };

        var _login = function (loginData) {

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();

            $http.post('/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });
                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;
                deferred.resolve(response);
            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _logOut = function () {
            localStorageService.remove('authorizationData');
            _authentication.isAuth = false;
            _authentication.userName = "";
        };

        var _fillAuthData = function () {
            var user = UserService.GetCurrentUser(function () {
                _authentication.isAuth = true;
                _authentication.userName = user.UserName;
            });
        };

        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;

        return authServiceFactory;
    })
    .factory('AuthInterceptorService', function ($q, localStorageService, $location) {

        var authInterceptorServiceFactory = {};

        var _request = function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        };

        var _responseError = function (rejection) {
            if (rejection.status === 401 && $location.absUrl().indexOf('login.html') < 0) {
                window.location = '/Web/login.html';
            }
            return $q.reject(rejection);
        };

        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
    })
    .factory('SearchQueryService', function () {
        var searchQuery = {};
        return {
            init: function (sortBy, searchFields) {
                searchQuery = {
                    Page: 1,
                    DataPerPage: 10,
                    SortBy: '',
                    IsSortAsc: true,
                    TotalData: 0,
                    Search: {
                        Keyword: "",
                        Fields: []
                    }
                };
                searchQuery.SortBy = sortBy;
                if (angular.isArray(searchFields)) {
                    angular.forEach(searchFields, function (val, key) {
                        searchQuery.Search.Fields.push(val);
                    });
                }
                else {
                    searchQuery.Search.Fields.push(searchFields)
                }
                return searchQuery;
            },
            getQueryParam: function () {
                return searchQuery;
            },
            setQueryParam: function (val) {
                searchQuery = val;
            }
        };
    })
    .factory('SysMessageService', function () {
        return {
            getSaveSuccessMsg: function () {
                return "Data Saved Successfully";
            },
            getSaveErrorMsg: function (errMsg) {
                var msg = "Error Saving Data";
                if (errMsg != '' && errMsg != null) {
                    msg += ". Error: " + errMsg;
                }
                return msg;
            },
            getLoadErrorMsg: function (errMsg) {
                var msg = "Error Loading Data";
                if (errMsg != '' && errMsg != null) {
                    msg += ". Error: " + errMsg;
                }
                return msg;
            },
            getDeleteConfirmationMsg: function (dataId) {
                return "Are you sure you want to delete [" + dataId + "] ?";
            },
            getDeleteSuccessMsg: function () {
                return "Data Deleted Successfully";
            },
            getDeleteErrorMsg: function (errMsg) {
                var msg = "Error Deleting Data";
                if (errMsg != '' && errMsg != null) {
                    msg += ". Error: " + errMsg;
                }
                return msg;
            }
        };
    })
    .factory('BreadcrumbService', function ($rootScope) {
        var path = [];
        var callback = null;
        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            path = [];
        });
        return {
            addCrumb: function (crumb) {
                path.push(crumb);
            },
            getCrumbs: function () {
                return path;
            },
            setUpdateCrumbsCallback: function (cb) {
                callback = cb;
            },
            updateCrumbs: function () {
                if (callback)
                    callback();
            }
        };
    })

    .factory('NumberService', function () {
        return {
            formatMoney: function (v, c, d, t) {
                var n = v,
                    c = isNaN(c = Math.abs(c)) ? 2 : c,
                    d = d == undefined ? "." : d,
                    t = t == undefined ? "," : t,
                    s = n < 0 ? "-" : "",
                    i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
                    j = (j = i.length) > 3 ? j % 3 : 0;
                return 'Rp. ' + s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
            }
        }
    })


;
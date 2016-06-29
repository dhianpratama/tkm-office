angular.module('SmartShelve')
    .factory('UserService', [
        '$resource', function ($resource) {
            return $resource('/API/User/:verb', {}, {
                Register: {
                    method: 'POST',
                    params: {
                        verb: 'Register'
                    }
                },
                CreateNewUser: {
                    method: 'POST',
                    params: {
                        verb: 'CreateNewUser'
                    }
                },
                UpdateUserData: {
                    method: 'POST',
                    params: {
                        verb: 'UpdateUserData'
                    }
                },
                ChangePassword: {
                    method: 'POST',
                    params: {
                        verb: 'ChangePassword'
                    }
                },
                ResetPassword: {
                    method: 'POST',
                    params: {
                        verb: 'ResetPassword'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                GetCurrentUser: {
                    method: 'GET',
                    params: {
                        verb: 'GetCurrentUser'
                    }
                },
                GetCurrentDefaultInstitution: {
                    method: 'GET',
                    params: {
                        verb: 'GetCurrentDefaultInstitution'
                    }
                }
            });
        }
    ])
    .factory('SysConfigurationService', [
        '$resource', function ($resource) {
            return $resource('/API/SysConfiguration/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                BulkSave: {
                    method: 'POST',
                    params: {
                        verb: 'BulkSave'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAll'
                    }
                },
                GetConfig: {
                    method: 'POST',
                    params: {
                        verb: 'GetConfig'
                    }
                }
            });
        }
    ])
    .factory('InstitutionService', [
        '$resource', function ($resource) {
            return $resource('/API/Institution/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('UomService', [
        '$resource', function ($resource) {
            return $resource('/API/Uom/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('BrandService', [
        '$resource', function ($resource) {
            return $resource('/API/Brand/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('ItemService', [
        '$resource', function ($resource) {
            return $resource('/API/Item/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('LocationTypeService', [
        '$resource', function ($resource) {
            return $resource('/API/LocationType/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('LocationService', [
        '$resource', function ($resource) {
            return $resource('/API/Location/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchAllByInstitution: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAllByInstitution'
                    }
                },
                FetchAllForDropdownList: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAllForDropdownList'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('ShelveService', [
        '$resource', function ($resource) {
            return $resource('/API/Shelve/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                FetchCompleteShelve: {
                    method: 'POST',
                    params: {
                        verb: 'FetchCompleteShelve'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('ReaderModuleService', [
        '$resource', function ($resource) {
            return $resource('/API/ReaderModule/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchAllByShelve: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAllByShelve'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                FetchOneByCode: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOneByCode'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('BinService', [
        '$resource', function ($resource) {
            return $resource('/API/Bins/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                SaveList: {
                    method: 'POST',
                    params: {
                        verb: 'SaveList'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchAllByReaderModule: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAllByReaderModule'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('BinItemService', [
        '$resource', function ($resource) {
            return $resource('/API/BinItem/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                SaveList: {
                    method: 'POST',
                    params: {
                        verb: 'SaveList'
                    }
                },
                FetchAll: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAll'
                    }
                },
                FetchAllByBinId: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAllByBinId'
                    }
                },
                FetchAllByItemId: {
                    method: 'POST',
                    isArray: true,
                    params: {
                        verb: 'FetchAllByItemId'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                }
            });
        }
    ])
    .factory('FileUploadService', [
        '$resource', function($resource) {
            return $resource('/API/FileUpload/:verb', {}, {
                UploadItemImage: {
                    method: 'POST',
                    params: {
                        verb: 'UploadItemImage'
                    }
                }
            });
        }
    ])
    .factory('StockCardService', [
        '$resource', function($resource) {
            return $resource('/API/StockCard/:verb', {}, {
                GetQtyMovement: {
                    method: 'POST',
                    params: {
                        verb: 'GetQtyMovement'
                    }
                },
                GetQtyBalance: {
                    method: 'POST',
                    params: {
                        verb: 'GetQtyBalance'
                    }
                },
                GetBinQtySales: {
                    method: 'POST',
                    params: {
                        verb: 'GetBinQtySales'
                    }
                },
                ResetAllTrx: {
                    method: 'POST',
                    params: {
                        verb: 'ResetAllTrx'
                    }
                },
                GetGraphSalesData: {
                    method: 'POST',
                    params: {
                        verb: 'GetGraphSalesData'
                    }
                }
            });
        }
    ])
    .factory('StockCardOutOfStockService', [
        '$resource', function($resource) {
            return $resource('/API/StockCardOutOfStock/:verb', {}, {
                GetOutOfStockData: {
                    method: 'POST',
                    params: {
                        verb: 'GetOutOfStockData'
                    }
                },
                GetGraphOutOfStockData: {
                    method: 'POST',
                    params: {
                        verb: 'GetGraphOutOfStockData'
                    }
                }
            });
        }
    ])
    .factory('TransactionService', [
        '$resource', function($resource) {
            return $resource('/API/Transaction/:verb', {}, {
                Save: {
                    method: 'POST',
                    params: {
                        verb: 'Save'
                    }
                },
                Delete: {
                    method: 'POST',
                    params: {
                        verb: 'Delete'
                    }
                },
                FetchAllWithPagination: {
                    method: 'POST',
                    params: {
                        verb: 'FetchAllWithPagination'
                    }
                },
                FetchOne: {
                    method: 'POST',
                    params: {
                        verb: 'FetchOne'
                    }
                },
                GenerateReferenceNumber: {
                    method: 'POST',
                    params: {
                        verb: 'GenerateReferenceNumber'
                    }
                },
            });
        }
    ])
    
    ;


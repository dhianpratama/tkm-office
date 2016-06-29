angular.module('SmartShelve')
    .controller('ShelveBuilderCtrl', function ($scope, ShelveService, ItemService, BinService, $uibModal, $rootScope, $timeout, BreadcrumbService) {
        BreadcrumbService.addCrumb('Master');
        BreadcrumbService.addCrumb('Shelves Builder');
        BreadcrumbService.updateCrumbs();
        $scope.module = {
            title: "Module",
            isModule: true
        };

        $scope.bin = {
            title: "Bin",
            isBin: true
        };

        $scope.modules = [];

        var isDragging = false;

        $scope.moduleDraggable = true;

        var i = 1;

        $scope.onBrandDropComplete = function (data, evt, column) {
            if (data.ItemCode) {
                if (column.BinId) {
                    column.Item = data;
                    column.ItemId = data.ItemId;
                } else {
                    openBinModal({
                        ItemId: data.ItemId
                    });
                }
            } else if (data.isBin) {
                column.Item = null;
                column.ItemId = null;
            } else if (data.BinId) {
                var Item = data.Item;

                data.Item = column.Item;
                data.ItemId = (column.Item) ? column.Item.ItemId : null;

                column.Item = Item;
                column.ItemId = (Item) ? Item.ItemId : null;
            }

            $scope.saved = false;
        };

        $scope.onModuleMove = function (data) {
            if (data.isModule)
                $scope.hovered = true;
        };

        $scope.onModuleStop = function (data) {
            if (data.isModule)
                $scope.hovered = false;
        };

        $scope.onBinStart = function (data) {
            if (data.isBin || data.ItemId) {
                $scope.binHovered = true;
                $scope.moduleDraggable = false;
            }
        };

        $scope.onBinMove = function (data) {
            if (data.isBin || data.ItemId) {
                $scope.binHovered = true;
            }
        };

        $scope.onBinStop = function (data) {
            if (data.isBin || data.ItemId) {
                $scope.binHovered = false;

            }
        };

        $scope.onDropComplete = function (data, evt, row, column, cell) {
            if (data.isModule) {
                if (cell.ReaderModuleId) {
                    cell.StackNo = data.StackNo;
                    cell.RowNo = data.RowNo;
                }

                data.StackNo = row + 1;
                data.RowNo = column + 1;

                buildTable();
            }
        };

        var shiftOneRow = function () {
            angular.forEach($scope.modules, function (module, k) {
                module.row = module.row + 1;
            });
        };

        var shiftOneColumn = function () {
            angular.forEach($scope.modules, function (module, k) {
                module.column = module.column + 1;
            });
        };

        $scope.table = [];

        var buildTable = function () {
            $scope.table.length = 0;

            var maxRow = 0;
            var maxStack = 0;

            angular.forEach($scope.modules, function (module, k) {
                var ver = module.StackNo;
                var hor = module.RowNo;

                if (maxRow < hor) maxRow = hor;
                if (maxStack < ver) maxStack = ver;

                if (hor != 0 || ver != 0) {
                    var row = ver - 1;
                    var column = hor - 1;

                    addCell(row, column, module);

                    buildBinTable(module);
                }
            });

            if ($scope.shelve && (maxRow == 0 || maxStack == 0)) {
                $scope.table[0] = [];
                $scope.table[0][0] = {
                    isSlot: true,
                    row: 0,
                    column: 0
                };
            } else {

                for (var i = 0; i <= maxStack; i++) {
                    if (!$scope.table[i]) $scope.table[i] = [];
                    var row = $scope.table[i];
                    for (var j = 0; j <= maxRow; j++) {
                        var slot = {
                            isSlot: true,
                            row: i,
                            column: j
                        };

                        if (i == maxStack || j == maxRow) slot.transparent = true;

                        if (!row[j]) row[j] = slot;

                        if (i == maxStack && j == maxRow) row.splice(j, 1);
                    }
                }
            }
        };

        var buildBinTable = function (module) {
            module.binTable = [];

            angular.forEach(module.Bins, function (bin, k) {
                if (bin.StackNo > module.NoOfStack || bin.RowNo > module.NoOfRow) return;

                var ver = bin.StackNo;
                var hor = bin.RowNo;

                if (hor != 0 || ver != 0) {
                    var row = ver - 1;
                    var column = hor - 1;

                    addBinCell(module.binTable, row, column, bin);
                }
            });

            for (var i = 0; i < module.NoOfStack; i++) {
                if (!module.binTable[i]) module.binTable[i] = [];
                var row = module.binTable[i];
                for (var j = 0; j < module.NoOfRow; j++) {
                    var slot = {
                        isSlot: true,
                        row: i,
                        column: j
                    };

                    if (!row[j]) row[j] = slot;
                }
            }
        };

        var addCell = function (row, column, obj) {
            if (!$scope.table) $scope.table = [];
            if (!$scope.table[row]) $scope.table[row] = [];

            obj.row = row;
            obj.column = column;
            obj.isModule = true;

            $scope.table[row][column] = obj;
        };

        var addBinCell = function (table, row, column, obj) {
            if (!table) table = [];
            if (!table[row]) table[row] = [];

            obj.row = row;
            obj.column = column;

            if (obj.Item && obj.Item.ImageFilename) {
                obj.Item.imageExist = true;
                obj.Item.imgPath = "/Content/Item/" + obj.Item.ItemId + "/" + obj.Item.ImageFilename;
            }

            table[row][column] = obj;
        };

        $scope.deleteModule = function (mod) {
            angular.forEach($scope.modules, function (module, k) {
                if (mod.moduleId == module.moduleId) {
                    $scope.modules.splice(k, 1);
                    buildTable();
                }
            });
        };

        $scope.deleteColumn = function (mod) {
            if (mod.columns && mod.columns.length > 0) mod.columns.pop();
        };

        var fetchShelve = function () {
            var jsonShelveResult = ShelveService.FetchAll(function () {
                $scope.shelves = jsonShelveResult;
            });
        };

        fetchShelve();

        var jsonItemResult = ItemService.FetchAll(function () {
            $scope.items = jsonItemResult;

            $scope.items.unshift({
                isBin: true,
                ItemName: "Empty Bin",
                ItemCode: "",
            });

            angular.forEach($scope.items, function (item, k) {
                if (item.ImageFilename) {
                    item.imageExist = true;
                    item.imgPath = "/Content/Item/" + item.ItemId + "/" + item.ImageFilename;
                }
            });
        });

        $scope.onRefresh = function () {
            if (!$scope.ShelveId) {
                $scope.table = [];
                return;
            }

            var jsonResult = ShelveService.FetchCompleteShelve({
                ShelveId: $scope.ShelveId,
            }, function () {
                var data = jsonResult;
                $scope.shelve = data;
                $scope.modules = data.Modules;

                buildTable();
            });
        };

        $scope.save = function () {
            $scope.action = true;
            $scope.saved = true;

            var bins = [];

            angular.forEach($scope.modules, function (module, k) {
                angular.forEach(module.Bins, function (bin, l) {
                    if (bin.Item) bin.ItemId = bin.Item.ItemId;

                    bin.Item = null;

                    bins.push(bin);
                });
            });

            var jsonResult = ShelveService.Save($scope.shelve, function () {
                $scope.action = false;

                $scope.onRefresh();
            });
        };

        var openModuleModal = function (data) {
            data.openFromBuilder = true;

            $uibModal.open({
                templateUrl: 'app/master/reader-module/reader-module-form/reader-module-form.html',
                controller: 'ReaderModuleFormCtrl',
                windowClass: 'padding-top-modal',
                size: 'lg',
                backdrop: 'static',
                resolve: {
                    moduleData: function () {
                        return angular.copy(data);
                    }
                }
            }).result.then(function (result) {
                $scope.onRefresh();
            });
        };

        $scope.onAddModule = function (StackNo, RowNo) {
            var data = {};
            data.NoOfRow = 2;
            data.RowNo = RowNo + 1;
            data.StackNo = StackNo + 1;
            data.ShelveId = $scope.ShelveId;
            openModuleModal(data);
        };

        $scope.onEditModule = function (data) {
            openModuleModal(data);
        };

        var openShelveModal = function (data) {
            data.openFromBuilder = true;

            $uibModal.open({
                templateUrl: 'app/master/shelve/shelve-form/shelve-form.html',
                controller: 'ShelveFormCtrl',
                windowClass: 'padding-top-modal',
                size: 'lg',
                backdrop: 'static',
                resolve: {
                    shelveData: function () {
                        return data;
                    }
                }
            }).result.then(function (result) {
                $scope.ShelveId = result.ShelveId;

                fetchShelve();

                $scope.onRefresh();
            });
        };

        $scope.onAddShelve = function () {
            openShelveModal({});
        };

        $scope.onEditShelve = function (data) {
            openShelveModal(data);
        };

        var openBinModal = function (data) {
            data.openFromBuilder = true;

            $uibModal.open({
                templateUrl: 'app/master/bins/bin-form/bin-form.html',
                controller: 'BinFormCtrl',
                windowClass: 'padding-top-modal',
                size: 'lg',
                backdrop: 'static',
                resolve: {
                    binData: function () {
                        return data;
                    }
                }
            }).result.then(function (result) {
                $scope.onRefresh();
            });
        };

        $scope.onAddBin = function () {
            openBinModal({});
        };

        $scope.onEditBin = function (data) {
            openBinModal(data);
        };

        $rootScope.$on('draggable:end', function (data) {
            isDragging = false;
            $scope.moduleDraggable = true;
        });
    });
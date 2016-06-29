angular.module('SmartShelve')
    .controller('ItemFormCtrl',function($scope, $uibModalInstance, itemData, ItemService, UomService, BrandService, FileUploadService){
        $scope.item = itemData;

        if ($scope.item.ImageFilename == null || $scope.item.ImageFilename == ""){
            $scope.imageExist = false;
        }
        else {
            $scope.imageExist = true;
            $scope.imgPath = "/Content/Item/" + $scope.item.ItemId + "/" + $scope.item.ImageFilename;
        }

        $scope.src = null;
        $scope.onSave = function(){
            var jsonResult = ItemService.Save($scope.item, function(){
                $uibModalInstance.close({IsSuccess: true});
            }, function(error){
                $uibModalInstance.close({IsSuccess: false, Message: error.data.Message});
            });
        };

        var jsonUomResult = UomService.FetchAll(function(){
           $scope.uoms = jsonUomResult;
        });

        var jsonBrandResult = BrandService.FetchAll(function(){
            $scope.brands = jsonBrandResult;
        });

        $scope.$on("cropme:done", function(ev, result, cropmeEl) {
            var blob = result.croppedImage;
            var data = {};
            data.ItemId = $scope.item.ItemId;
            data.Blob = blob;

            var fd = new FormData();
            fd.append("itemId", $scope.item.ItemId);
            fd.append("blob", blob, result.filename);
            $scope.item.ImageFilename = result.filename.replace(/"/g,"");
            var url = "/API/FileUpload/UploadItemImage";
            var xhr = new XMLHttpRequest();
            xhr.open("POST", url, true);
            xhr.onreadystatechange = function(e) {
                if (this.readyState === 4 && this.status === 200) {
                    var itemImageFolder = "temp";
                    if ($scope.item.ItemId != 0)
                        itemImageFolder = "" + $scope.item.ItemId;

                    $scope.imageExist = true;
                    $scope.imgPath = "/Content/Item/" + itemImageFolder + "/" + $scope.item.ImageFilename;
                    return console.log("done");
                } else if (this.readyState === 4 && this.status !== 200) {
                    return console.log("failed");
                }
            };
            xhr.send(fd);
        });

        $scope.onDeleteImage = function(){
            $scope.imageExist = false;
            $scope.imgPath = null;

            $scope.$broadcast("cropme:cancel");
        }
    });
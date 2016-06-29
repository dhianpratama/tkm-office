angular.module('SmartShelve').controller('TransactionFormCtrl', function ($scope, $uibModalInstance, $log, transactionData, uiUploader, TransactionService) {
    $scope.data = transactionData;
    $scope.dt = { opened: false };
    $scope.today = new Date();
    var baseRelativePictureUrl = '/Content/Transaction/temp/';

    var referenceNumber = '';
    if (!$scope.data.TransactionId > 0) {
        var refJson = TransactionService.GenerateReferenceNumber(function () {
            referenceNumber = refJson.Data;
            $scope.data.TransactionCode = referenceNumber;
        });
        $scope.data.Type = 1;
    } else {
        $scope.data.TransactionDate = moment($scope.data.TransactionDate).toDate();
    }

    var generateDataToSave = function (data) {
        var result = data;
        result.TransactionDate = moment(data.TransactionDate).format('MM/DD/YYYY');
        result.Type = parseInt(data.Type);
        return result;
    }

    $scope.onSave = function () {
        var jsonResult = TransactionService.Save(generateDataToSave($scope.data), function () {
            $uibModalInstance.close({ IsSuccess: true });
        }, function (error) {
            $uibModalInstance.close({ IsSuccess: false, Message: error.data.Message });
        });
    };

    $scope.types = [
        {
            id: 1,
            label: 'Income'
        },
        {
            id: 2,
            label: 'Outcome'
        }
    ];

    $scope.btn_remove = function (file) {
        $log.info('deleting=' + file);
        uiUploader.removeFile(file);
    };
    $scope.btn_clean = function () {
        uiUploader.removeAll();
    };
    $scope.btn_upload = function () {
        $log.info('uploading...');
        uiUploader.startUpload({
            url: 'http://localhost:7777/api/fileupload/UploadTransactionImage',
            concurrency: 2,
            onProgress: function (file) {
                $log.info(file.name + '=' + file.humanSize);
                $scope.$apply();
            },
            onCompleted: function (file, response) {
                $log.info(file + 'response' + response);
                $scope.data.PictureUrl = baseRelativePictureUrl + file.name;
                $scope.$apply();
            }
        });
    };
    $scope.files = [];

    $scope.onChangeFile = function () {
        var element = document.getElementById('file1');
        var files = element.files;
        uiUploader.addFiles(files);
        $scope.files = uiUploader.getFiles();
        $scope.btn_upload();
    };

});
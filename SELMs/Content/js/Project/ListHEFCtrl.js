var app = angular.module("myApp", []);

app.controller('ListEAACtrl', function ($scope, $http, $sce) {

    var username = $('#username').val();
    var isadmin = $('#isadmin').val();
    var role = $('#role').val();

    $scope.username = $('#username').val();
    $scope.isadmin = $('#isadmin').val();
    $scope.role = $('#role').val();
    $scope.today = new Date();

    var dd = $scope.today.getDate();
    var mm = $scope.today.getMonth() + 1; //January is 0!

    var yyyy = $scope.today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    $scope.NgayHomNay = dd + '/' + mm + '/' + yyyy;


    $scope.ErrorSystem = function (errorMessage) {
        // This function handles errors and displays the error message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Error: ' + errorMessage;
        notificationElement.style.backgroundColor = '#f5aaaa';
        notificationElement.style.width = '600px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';
        notificationElement.style.zIndex = '10000';
        notificationElement.style.marginLeft = '100px';

        // You can customize the notification style and appearance here.
    }

    $scope.SuccessSystem = function (successMessage) {
        console.log("ok");
        // This function handles success messages and displays the success message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Success: ' + successMessage;
        notificationElement.style.backgroundColor = '#97c797';
        notificationElement.style.width = '600px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';
        notificationElement.style.zIndex = '10000';
        notificationElement.style.marginLeft = '100px';


        // You can customize the notification style and appearance here.
    }

    $scope.project_name = '';
    $scope.location_name = '';
    $scope.creater_name = '';
    $scope.application_code = '';
    $scope.LoadHEFList = function () {
        var data = {
            username: username
        }
        $http.post(origin + '/api/v1/equipment-handover/forms', data).then(function (response) {
            $scope.ListHEF = response.data.Result
            console.log($scope.ListHEF);
        });
    }
    $scope.LoadHEFList();

    $scope.SaveAttachFileHandover = function (item) {
        var data = {
            form_id: item.form_id,
            form_code: item.form_code,
            file_attach: document.getElementById('attach_file' + item.form_code).files[0]
        }
        console.log(data);
        var partialUrl = origin + '/api/v1/equipment-handover/new-attach_file';
        //Api lưu attach file
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Cập nhập biên bản bàn giao thành công!');

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.SaveFinishHandover = function (item) {
        var partialUrl = origin + '/api/v1/equipment-handover/finish-file/' + item.form_id;
        //Api updatetrangj thái trong bảng handover cột is_finish = 1
        $http.post(partialUrl)
            .then(function (response) {
                $scope.SuccessSystem('Kết thúc bàn giao thiết bị thành công!');

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.CancelHandover = function (item) {
        var partialUrl = origin + '/api/v1/equipment-handover/cancel/' + item.form_id;
        //Api này sẽ xóa đơn trong handover_form, xóa chi tiết handover_form, lấy ra các allocation_detail
        //, update về trạng thái 'Có sẵn trong kho'
        $http.post(partialUrl)
            .then(function (response) {
                $scope.SuccessSystem('Hủy đơn bàn giao thiết bị thành công!');

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
});
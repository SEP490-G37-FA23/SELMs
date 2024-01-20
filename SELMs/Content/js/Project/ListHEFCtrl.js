var app = angular.module("myApp", []);

app.controller('ListHEFCtrl', function ($scope, $http, $sce) {

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
            $scope.ListHEF.forEach(item => {
                if (item.attachment_id != null) {
                    var partialUrl = origin + '/api/v1/attachments/' + item.attachment_id;
                    //+ listImage[0].image_id;
                    $http.get(partialUrl).then(function (res) {
                        console.log(res)
                        item.url = res.config.url;
                    })
                }
               
            })
           
        });
    }
    $scope.LoadHEFList();

    $scope.GetDetailHandoverForm = function (item) {
        window.location.href = "/EquipmentAllocation/HEFDetail/" + item.form_id;
    }
    $scope.SaveAttachFileHandover = function (item) {
        console.log('attach_file' + item.form_code);

        console.log(document.getElementById('attach_file' + item.form_code).files[0]);

        var fileAttach = document.getElementById('attach_file' + item.form_code).files[0];
        let formData = new FormData();
        formData.append('file_attach', fileAttach);
        formData.append('form_id', item.form_id);
        formData.append('form_code', item.form_code);
        var partialUrl = origin + '/api/v1/equipment-handover/new-attach_file';

        $http.post(partialUrl, formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }  // Let AngularJS set the content type
        })
            .then(function (response) {
                $scope.SuccessSystem('Cập nhập biên bản bàn giao thành công!');
                $scope.LoadHEFList();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.SaveFinishHandover = function (item) {
        var userConfirmed = confirm("Đánh dấu hoàn thành quá trình bàn giao thiết bị?");

        // Check the user's response
        if (userConfirmed) {
            var partialUrl = origin + '/api/v1/equipment-handover/confirm-finish/' + item.form_id;
            //Api updatetrangj thái trong bảng handover cột is_finish = 1
            $http.post(partialUrl)
                .then(function (response) {
                    $scope.SuccessSystem('Đánh dấu hoàn thành quá trình bàn giao thiết bị thành công!');
                    $scope.LoadHEFList();
                }, function (error) {
                    $scope.ErrorSystem(error.data.Message);
                });
        }
    }
    $scope.CancelHandover = function (item) {
        var userConfirmed = confirm("Hủy đơn bàn giao thiết bị?");

        // Check the user's response
        if (userConfirmed) {
            var partialUrl = origin + '/api/v1/equipment-handover/cancel/' + item.form_id;
            //Api này sẽ xóa đơn trong handover_form, xóa chi tiết handover_form, lấy ra các allocation_detail
            //, update về trạng thái 'Có sẵn trong kho'
            $http.post(partialUrl)
                .then(function (response) {
                    $scope.SuccessSystem('Hủy đơn bàn giao thiết bị thành công!');
                    $scope.LoadHEFList();
                }, function (error) {
                    $scope.ErrorSystem(error.data.Message);
                });
        }
    }
});
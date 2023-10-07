var app = angular.module("myApp", []);


app.controller('DSNhomHangCtrl', function ($scope, $http) {
    var username = $('#username').val();
    var isadmin = $('#isadmin').val();
    var maphongban = $('#maphongban').val();

    $scope.username = $('#username').val();
    $scope.isadmin = $('#isadmin').val();
    $scope.maphongban = $('#maphongban').val();
    var sotrang = 1;
    $scope.tukhoa = "";
    $scope.marketing = "";
    $scope.loai = "";
    $scope.hang = '';

    $scope.ErrorSystem = function (errorMessage) {
        // This function handles errors and displays the error message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Error: ' + errorMessage;
        notificationElement.style.backgroundColor = '#f5aaaa';
        notificationElement.style.width = '300px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';

        // You can customize the notification style and appearance here.
    }

    $scope.SuccessSystem = function (successMessage) {
        // This function handles success messages and displays the success message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Success: ' + successMessage;
        notificationElement.style.backgroundColor = '#97c797';
        notificationElement.style.width = '300px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';


        // You can customize the notification style and appearance here.
    }
    //====================Danh sách nhóm hàng============
    $scope.LoadGetListNhomHangHoa = function () {
        data = {
            hang: $scope.hang,
            sotrang: 1
        }
        $http.post(origin + '/api/Api_HangHoa/GetListNhomHangHoa', data).then(function (response) {
            $scope.ListNhomHang = response.data;
        }, function errorCallback(response) {
            $scope.ErrorSystem('KhôngLấy được danh sách hãng');

        });
        $http.post(origin + '/api/Api_HangHoa/DemDanhSachVTHH', data).then(function (response) {
            $scope.tongsohang = response.data;
        });
    };
    $scope.LoadGetListNhomHangHoa();

    //====================Thêm nhóm hàng============

    $scope.AddNhomHang = function () {

        if ($scope.manhomhangchitiet == "" || $scope.manhomhangchitiet == null || $scope.manhomhangchitiet == undefined) {
            $scope.ErrorSystem("Bạn chưa điền mã nhóm hàng nên ko thể tạo hãng")
            return
        }
        var data_add = {
            MA_NHOM_HANG_CHI_TIET: $scope.manhomhangchitiet,
            CHUNG_LOAI_HANG: $scope.chungloaihang,
            MA_NHOM_HANG_CHA: $scope.manhomhangcha,
            GHI_CHU: $scope.ghichu,
        }
        $http.post(origin + '/api/Api_Hanghoa/AddNhomHangHoa', data_add).then(function (response) {
            if (response.status == 200) {
                $scope.SuccessSystem("Thêm thành công");
                $scope.manhomhangchitiet = '';
                $scope.chungloaihang = '';
                $scope.manhomhangcha = '';
                $scope.ghichu = '';
                $scope.LoadGetListNhomHangHoa();
            } else {
                $scope.ErrorSystem(response.data)

            }

        });
    }

    //====================Sửa nhóm hàng============
    $scope.edit = function (item) {
        $scope.item = item;
    }
    $scope.passing = function (item) {
        $scope.item = item;
    }
    $scope.save = function () {

        var data_update = {
            MA_NHOM_HANG_CHI_TIET: $scope.item.MA_NHOM_HANG_CHI_TIET,
            CHUNG_LOAI_HANG: $scope.item.CHUNG_LOAI_HANG,
            MA_NHOM_HANG_CHA: $scope.item.MA_NHOM_HANG_CHA,
            TRUC_THUOC: $scope.item.TRUC_THUOC,
            GHI_CHU: $scope.item.GHI_CHU,
        }
        $http.post(origin + '/api/Api_Hanghoa/SuaNhomHangHoa', data_update).then(function (response) {
            if (response.status == 200) {
                $scope.SuccessSystem("Sửa nhóm hàng thành công")
                $scope.LoadGetListNhomHangHoa();
            } else {
                $scope.ErrorSystem("Sửa nhóm hàng thất bại " + response.data);
            }
        });
    }

    //====================Xóa nhóm hàng============

    $scope.delete = function (hangsp) {
        var data_delete = {
            MA_NHOM_HANG_CHI_TIET: hangsp
        }
        $http.post(origin + '/api/Api_Hanghoa/DeleteNhomHangHoa', data_delete).then(function (response) {
            if (response.status == 200) {

                $scope.LoadGetListNhomHangHoa();
                $scope.SuccessSystem("Xóa nhóm hàng thành công");
            }
            else {
                $scope.ErrorSystem("Lỗi khi xóa hoặc đã có mã hàng sử dụng mã nhóm hàng này")
            }
        });
    };

});

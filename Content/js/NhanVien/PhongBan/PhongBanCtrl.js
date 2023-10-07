var app = angular.module("myApp", []);

app.controller('PhongBanCtrl', function ($scope, $http) {

    const username = $('#username').val();
    const macongty = $('#macongty').val();

    $scope.tukhoa1 = "";
    $scope.username = "";
    const data = {
        tukhoa1: '',
        username: username,
        sotrang: 1
    }
    //R: Danh sách phòng ban
    const GetListPhongBan = (sotrang) => {
        data.sotrang = sotrang
        $http.post(origin + '/api/Api_PhongBan/GetAllPhongBan', data).then(function (response) {
            $scope.ListPhongBan = response.data;
        })
    }
    GetListPhongBan(1)

    //C:Thêm phòng ban mới
    $scope.AddNewPhongBan = function () {
        var newPhongBan = {
            MA_PHONG_BAN: $scope.MA_PHONG_BAN,
            TEN_PHONG_BAN: $scope.TEN_PHONG_BAN,
            GHI_CHU: $scope.GHI_CHU,
            MA_CONG_TY: macongty
        }
        $http.post(origin + '/api/Api_PhongBan/AddNewPhongBan', newPhongBan).then(function (response) {
            if (response.status == 200) {
                //SuccessSystem('Thêm mới phòng ban thành công!')
                GetListPhongBan(1);
                $('#AddPhongBan').modal('hide');
            } else {
                ErrorSystem(response.Message)
            }

        });

    }
    //U:Sửa nhanh phòng ban
    $scope.UpdateNhanhPhongBan = (item) => {
        var data = {
            MA_PHONG_BAN: item.MA_PHONG_BAN,
            TEN_PHONG_BAN: item.TEN_PHONG_BAN,
            GHI_CHU: item.GHI_CHU
        }
        $http.post(origin + '/api/Api_PhongBan/SuaNhanhPhongBan', data).then(function (response) {
            if (response.status == 200) {
                GetListPhongBan(1);
                SuccessSystem('Cập nhật phòng ban thành công!')
            } else {
                ErrorSystem(response.Message)
            }
        });

    }
    //D: Xóa phòng ban
    $scope.XoaPhongBan = function (username) {
        var data = {
            username: username
        }
        $http.post(origin + '/api/Api_PhongBan/XoaPhongBan', data).then(function (response) {
            if (response.status == 200) {
                SuccessSystem('Xóa phòng ban thành công!')
                GetListPhongBan(1);
            } 


        })

    }
   
    //F: Tìm kiếm phòng ban
    $scope.SearchPhongBan = (field, value) => {
        data[field] = value;
        GetListPhongBan(1);
    }

})
var app = angular.module("myApp", []);

app.controller('DSKhachHangCtrl', function ($scope, $http, $sce) {
    var username = $('#username').val();
    var isadmin = $('#isadmin').val();
    $scope.username = $('#username').val();
    $scope.tukhoa1 = "";
    $scope.tukhoa2 = "";
    $scope.tukhoa3 = "";
    $scope.tukhoa4 = "";
    $scope.tukhoa5 = "";
    $scope.tukhoa6 = "";
    $scope.tukhoa7 = "";

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
        notificationElement.style.width = '300px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';

        // You can customize the notification style and appearance here.
    }

    $scope.SuccessSystem = function (successMessage) {
        console.log("ok");
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

    //===============Thêm mới khách hàng=====================
    $scope.ResetKH = function (kh) {
        kh.TEN_CONG_TY = '';
        kh.VAN_PHONG_GIAO_DICH = '';
        kh.DIA_CHI_XUAT_HOA_DON = '';
        kh.TINH = '';
        kh.QUOC_GIA = '';
        kh.MST = '';
        kh.HOTLINE = '';
        kh.EMAIL = '';
        kh.WEBSITE = '';
        kh.DIEU_KHOAN_THANH_TOAN = '';
        kh.GHI_CHU = '';
        kh.SALE_HIEN_THOI = '';
        kh.MA_LOAI_KHACH = '';
        kh.XUAT_HOA_DON_KHACH = '';
    }
    //Danh sách nhân viên sale
    $scope.LoadListNhanVienKD = function (tukhoa) {
        var data = {
            tukhoa4: tukhoa
        }
        $http.post(origin + '/api/Api_KhachHang/GetListNhanVienSales', data).then(function (response) {
            $scope.ListNVSales = response.data;
        });
    };
    $scope.SelectDataSalesHTCreate = function (item, nkh) {
        nkh.SALE_HIEN_THOI = item.USERNAME;
        nkh.TEN_SALE_HIEN_THOI = item.HO_VA_TEN;
    };
    $scope.CreateKhachHang = function (nkh) {
        var data_add = {
            TEN_CONG_TY: nkh.TEN_CONG_TY,
            VAN_PHONG_GIAO_DICH: nkh.VAN_PHONG_GIAO_DICH,
            DIA_CHI_XUAT_HOA_DON: nkh.DIA_CHI_XUAT_HOA_DON,
            TINH: nkh.TINH,
            QUOC_GIA: nkh.QUOC_GIA,
            MST: nkh.MST,
            HOTLINE: nkh.HOTLINE,
            EMAIL: nkh.EMAIL,
            WEBSITE: nkh.WEBSITE,
            DIEU_KHOAN_THANH_TOAN: nkh.DIEU_KHOAN_THANH_TOAN,
            GHI_CHU: nkh.GHI_CHU,
            SALES_TAO: username,
            NGAY_TAO: $scope.NgayHomNay,
            SALE_HIEN_THOI: nkh.SALE_HIEN_THOI,
            MA_LOAI_KHACH: nkh.MA_LOAI_KHACH,
            XUAT_HOA_DON_KHACH: nkh.XUAT_HOA_DON_KHACH
        }
        $http.post(origin + '/api/Api_KhachHang/CreateKhachHang', data_add).then(function (response) {
            if (response.status == 200) {
                $scope.SuccessSystem('Tạo khách hàng thành công!');

                $scope.LoadListKhachHang(1);
                $scope.ResetKH(nkh);
            }
            else {
                $scope.ErrorSystem('Tạo khách hàng thất bại!!!');
            }
        });

    };


    //===============Danh sách khách hàng=====================
    $scope.page = 1;
    const data = {
        tukhoa1: '',
        tukhoa2: '',
        tukhoa3: '',
        tukhoa4: '',
        tukhoa6: '',
        username: username,
        isadmin: 0,
        sotrang: 1
    }
    $scope.LoadListKhachHang = function (sotrang) {
        $scope.page = sotrang;
        data.sotrang = sotrang
        $http.post(origin + '/api/Api_KhachHang/GetListKhachHang', data).then(function (response) {
            $scope.ListKhachHang = response.data;
        });
    }
    $scope.LoadListKhachHang(1);
    $scope.SearchKhachHang = (field, value) => {
        data[field] = value;
        $scope.LoadListKhachHang(1);
    }
    //Danh sách khách hàng xuất excel
    $scope.LoadListKhachHangXuatExcel = function () {
        $http.post(origin + '/api/Api_KhachHang/GetListKhachHangXuatExcel', data).then(function (response) {
            $scope.ListKHXuatExcel = response.data
            $scope.SumKH = response.data.length;
        });
    };
    $scope.LoadListKhachHangXuatExcel();
    $scope.kh = {};

    //================Sửa khách hàng=============
    $scope.GetKH = function (item) {
        $scope.kh = item;
    };
    //Chọn sale hiện thời sửa khách hàng
    $scope.SelectDataSalesHTEdit = function (item) {
        $scope.kh.SALE_HIEN_THOI = item.USERNAME;
        $scope.kh.TEN_SALE_HIEN_THOI = item.HO_VA_TEN;
    };
    //Lưu thông tin sửa khách hàng
    $scope.SaveKhachHang = function (kh) {
        console.log(kh);
        var data_edit = {
            MA_KHACH_HANG: kh.MA_KHACH_HANG,
            TEN_CONG_TY: kh.TEN_CONG_TY,
            VAN_PHONG_GIAO_DICH: kh.VAN_PHONG_GIAO_DICH,
            DIA_CHI_XUAT_HOA_DON: kh.DIA_CHI_XUAT_HOA_DON,
            TINH: kh.TINH,
            QUOC_GIA: kh.QUOC_GIA,
            MST: kh.MST,
            HOTLINE: kh.HOTLINE,
            EMAIL: kh.EMAIL,
            WEBSITE: kh.WEBSITE,
            DIEU_KHOAN_THANH_TOAN: kh.DIEU_KHOAN_THANH_TOAN,
            GHI_CHU: kh.GHI_CHU,
            SALE_HIEN_THOI: kh.SALE_HIEN_THOI,
            MA_LOAI_KHACH: kh.MA_LOAI_KHACH,
            XUAT_HOA_DON_KHACH: kh.XUAT_HOA_DON_KHACH

        }
        console.log(data_edit);
        $http.post(origin + '/api/Api_KhachHang/UpdateKhachHang', data_edit).then(function (response) {
            if (typeof (response.data) == "object") {
                $scope.SuccessSystem('Cập nhật khách hàng thành công!')
                $scope.LoadListKhachHang(1);
                $scope.ResetKH(kh);
            }
            else {
                $scope.ErrorSystem(response.data);
            }
        }, function (error) {
            ConnectFail();
        });

    };

    //================Xóa khách hàng=============
    $scope.DeleteKH = function (kh) {
        console.log(kh);
        var data = {
            makhachhang: kh.MA_KHACH_HANG
        }
        $http.post(origin + '/api/Api_KhachHang/DeleteKhachHang', data).then(function (response) {
            if (response.status == 200) {
                $scope.SuccessSystem('Xóa khách hàng thành công!')
                $scope.LoadListKhachHang(1);
            }
            else {
                $scope.ErrorSystem(response.data);
            }
        });

    };

    //===================Xuất excel ======================
    $scope.XuatExcel = function () {
        var file_name = 'Danh sách khách hàng'
        var cancelstyle = {
            headers: true,
            column: { style: { Font: { Bold: "1" } } },
            columns: [
                { columnid: 'MA_KHACH_HANG', title: 'Mã khách hàng', width: 150 },
                { columnid: 'TEN_CONG_TY', title: 'Tên công ty', width: 150 },
                { columnid: 'VAN_PHONG_GIAO_DICH', title: 'Văn phòng giao dịch', width: 150 },
                { columnid: 'DIA_CHI_XUAT_HOA_DON', title: 'Địa chỉ xuất hóa đơn', width: 200 },
                { columnid: 'MST', title: 'MST ', width: 100 },
                { columnid: 'TINH', title: 'Tỉnh ', width: 100 },
                { columnid: 'HOTLINE', title: 'HOTLINE', width: 150 },
                { columnid: 'GHI_CHU', title: 'Mã chuẩn', width: 150 },
                { columnid: 'DIEU_KHOAN_THANH_TOAN', title: 'Điều khoản thanh toán', width: 150 },
                { columnid: 'TEN_SALE_HIEN_THOI', title: 'Sale hiện thời ', width: 100 },
                { columnid: 'TEN_SALE_TAO', title: 'Sale tạo ', width: 100 },


            ],
        };

        alasql('SELECT * INTO XLSXML("' + file_name + '",?) FROM ?', [cancelstyle, $scope.ListKHXuatExcel]);
    };

    // =======================Import khách hàng =====================
    $scope.ShowData = function () {
        $("textarea[name=listhang]").val(CKEDITOR.instances.listhang.getData());
        var listhang = $("[name=listhang]").val();
        var res = listhang.replace('<table', '<table id="tableupload" ');
        $scope.showdatacontent = res;
        $scope.htmlContent = $sce.trustAsHtml($scope.showdatacontent);
        $scope.clicked = true
    }
    $scope.GetData = function () {
        $("textarea[name=listhang]").val(CKEDITOR.instances.listhang.getData());
        var listhang = $("[name=listhang]").val();
        var res = listhang.replace('<table', '<table id="tableupload" ');
        $scope.showdatacontent = res;
        var tableUp = document.getElementById('tableupload');
        $scope.ListHangImport = []
        //gets rows of table
        var rowLength = tableUp.rows.length;
        if (rowLength > 2000) {
            rowLength = 2000;
        }

        for (i = 0; i < rowLength; i++) {
            $scope.ListHangImport.push({
                TEN_CONG_TY: (document.getElementById("tableupload").rows[i].cells.item(0).innerText),
                VAN_PHONG_GIAO_DICH: (document.getElementById("tableupload").rows[i].cells.item(1).innerText),
                TINH: (document.getElementById("tableupload").rows[i].cells.item(2).innerText),
                HOTLINE: (document.getElementById("tableupload").rows[i].cells.item(3).innerText),
                GHI_CHU: (document.getElementById("tableupload").rows[i].cells.item(4).innerText),
                SALES_TAO: username,

            })
        }
        $http.post(origin + '/api/Api_KhachHang/ImportListKhachHang', $scope.ListHangImport).then(function (response) {
            if (response.status == 200) {
                $scope.SuccessSystem(response.data)
                $('#importlisthang').modal('hide');
                $scope.LoadListKhachHang(1);
            } else {
                ErrorSystem(response.data)
            }
        })
    }
})
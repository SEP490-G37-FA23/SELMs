var app = angular.module("myApp", []);

app.controller('DSHangHoaCtrl', function ($scope, $http) {
    var isadmin = $("#isadmin").val();
    var username = $('#username').val();

    $scope.isadmin = $("#isadmin").val();
    $scope.username = $('#username').val();

    $scope.ma_chuan = "";

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
    var timkiem = {
        mahang: $scope.ma_chuan,
        sotrang: 1,
    }

    $scope.timkiemhanghoa = function (ma_chuan) {
        timkiem = {
            mahang: $scope.ma_chuan,
            sotrang: 1,
        }
        $scope.LoadListHangHoa();
    }
    $scope.kiemtranull = function (tukhoa) {
        if (tukhoa == "" || tukhoa == null) {
            timkiem = {
                mahang: $scope.ma_chuan,
                sotrang: 1
            }
            $scope.LoadListHangHoa();
        }
    };

    //====================================DANH SÁCH HÀNG HÓA ==========================================================
    $scope.LoadListHangHoa = function () {
        $http.post(origin + '/api/Api_HangHoa/GetListHangHoa', timkiem).then(function (response) {
            $scope.ListHangHoa = response.data;
        }, function (error) {
            ConnectFail();
        });

        $http.post(origin + '/api/Api_HangHoa/GetListHangHoaXuatExcel', timkiem).then(function (response) {
            $scope.tongsoHH = response.data.length;
            $scope.DanhSachHangHoaExport = response.data;
        });

    };
    $scope.LoadListHangHoa();


    //========================================THÊM HÀNG HÓA=====================================================================

    $scope.LoadListNhomHang = function (new_nhom_hang) {
        data = {
            hang: new_nhom_hang,
            sotrang: 1
        }
        $http.post(origin + '/api/Api_HangHoa/GetListNhomHangHoa', data).then(function (response) {
            $scope.ListNhomHang = response.data;
            console.log($scope.ListNhomHang)

        }, function errorCallback(response) {
            $scope.ErrorSystem('Không lấy được danh sách hãng');

        });

    }
    $scope.LoadListNhomHang('');
    $scope.selectDVT = ["Cái", "Set", "Bịch", "Cặp", "Cây", "Chai", "Cuộn", "Dây", "Hộp", "Kg", "Lít", "Lô", "Lốc", "Lon", "Mét", "Pack", "Tấm", "Tập", "Thùng", "Vỉ", "Tuýp", "Viên", "khác"];
    $scope.newhh = {
        ma_chuan: '',
        ten_hang: '',
        xuat_xu: '',
        dvt: '',
        nhom_hang: '',
        gia_list: '',
        gia_nhap:'',
        gia_ban1: '',
        gia_ban2: '',
        gia_ban3: '',
        gia_ban4: '',
        CK1: 0,
        CK2: 0,
        CK3: 0,
        CK4: 0,
        thong_so: '',
        discontinue: false,
        machuyendoi: '',
        ghi_chu: '',
        bao_hanh:''
    }

    $scope.GetGiaBan = function (CK, newhh, number) {
        switch (number) {
            case 1:
                newhh.GIA_BAN_1 = parseFloat(newhh.GIA_LIST) - parseFloat(CK) * parseFloat(newhh.GIA_LIST) / 100;
                break;
            case 2:
                newhh.GIA_BAN_2 = parseFloat(newhh.GIA_LIST) - parseFloat(CK) * parseFloat(newhh.GIA_LIST) / 100;
                break;
            case 3:
                newhh.GIA_BAN_3 = parseFloat(newhh.GIA_LIST) - parseFloat(CK) * parseFloat(newhh.GIA_LIST) / 100;
                break;
            case 4:
                newhh.GIA_BAN_4 = parseFloat(newhh.GIA_LIST) - parseFloat(CK) * parseFloat(newhh.GIA_LIST) / 100;
                break;
        }
    }


    $scope.inputNhomHang = function (product,hh) {
        hh.nhom_hang = product.MA_NHOM_HANG_CHI_TIET;
        $scope.show_nhom_hang = false;

    }


    $scope.CreateNewProduct = function (hh) {
        if (hh.ten_hang == "" || hh.ten_hang == null) {
            $scope.ErrorSystem("Cần phải điền tên hàng")
            return
        }

        if (hh.dvt == "" || hh.dvt == null) {
            $scope.ErrorSystem("Cần phải điền đơn vị tính")
            return
        }
        if (hh.nhom_hang == "" || hh.nhom_hang == null) {
            $("#ma_nhom_hang").focus();
            $scope.ErrorSystem("Cần phải chọn mã nhóm hàng !");
            return
        }


        var data_add = {
            MA_CHUAN: hh.MA_CHUAN,
            TEN_HANG: hh.TEN_HANG,
            XUAT_XU: hh.XUAT_XU,
            DON_VI_TINH: hh.DVT,
            MA_NHOM_HANG: hh.NHOM_HANG,
            GIA_LIST: parseFloat(hh.GIA_LIST),
            GIA_NHAP: parseFloat(hh.GIA_NHAP),
            GIA_BAN_1: parseFloat(hh.GIA_BAN_1),
            GIA_BAN_2: parseFloat(hh.GIA_BAN_2),
            GIA_BAN_3: parseFloat(hh.GIA_BAN_3),
            GIA_BAN_4: parseFloat(hh.GIA_BAN_4),
            CK1: parseFloat(hh.CK1),
            CK2: parseFloat(hh.CK2),
            CK3: parseFloat(hh.CK3),
            CK4: parseFloat(hh.CK4),
            THONG_SO_KY_THUAT: hh.THONG_SO,
            DISCONTINUE: hh.DISCONTINUE,
            MA_CHUYEN_DOI: hh.MA_CHUYEN_DOI,
            GHI_CHU: hh.GHI_CHU,
            BAO_HANH: hh.BAO_HANH,

        }
        $http.post(origin + '/api/Api_HanghoaHL/PostHH', data_add).then(function (response) {
            if (response.status == 200) {
                $scope.SuccessSystem("Thêm hàng hóa thành công")
                $scope.LoadListHangHoa();
                $('#newHH').modal('hide');
            } else {
                $scope.ErrorSystem('Không thêm được hàng hóa');
            }
        }
        );
    };

    //=================================XÓA HÀNG HÓA==================================================

    $scope.XoaHangHoa = function (item) {
        var x = confirm("Bạn có chắc muốn xóa mã hàng này không?")
        if (x) {
            var hanghoa = {
                MA_HANG: item.MA_HANG,
                NGUOI_CAP_NHAT: username
            }
            $http.post(origin + '/api/Api_HangHoa/DeleteHangHoa', hanghoa).then(function (res) {
                if (res.status == 200) {
                    $scope.SuccessSystem("Xóa thành công")
                    $scope.LoadListHangHoa();
                }
            }, function (error) {
                $scope.ErrorSystem("Xóa thất bại")
            });
        }

    };

    //=================================SỬA HÀNG HÓA==================================================
    $scope.GetEditHH = function (hh) {
        $scope.item = hh;
    }
    $scope.CapNhatNhanhHangHoa = function (hh) {
        var data = {
            MA_HANG: hh.MA_HANG,
            MA_CHUAN: hh.MA_CHUAN,
            TEN_HANG: hh.TEN_HANG,
            XUAT_XU: hh.XUAT_XU,
            DON_VI_TINH: hh.DVT,
            MA_NHOM_HANG: hh.MA_NHOM_HANG,
            GIA_LIST: parseFloat(hh.GIA_LIST),
            GIA_NHAP: parseFloat(hh.GIA_NHAP),
            GIA_BAN_1: parseFloat(hh.GIA_BAN_1),
            GIA_BAN_2: parseFloat(hh.GIA_BAN_2),
            GIA_BAN_3: parseFloat(hh.GIA_BAN_3),
            GIA_BAN_4: parseFloat(hh.GIA_BAN_4),
            CK1: parseFloat(hh.CK1),
            CK2: parseFloat(hh.CK2),
            CK3: parseFloat(hh.CK3),
            CK4: parseFloat(hh.CK4),
            THONG_SO_KY_THUAT: hh.THONG_SO,
            DISCONTINUE: hh.DISCONTINUE,
            MA_CHUYEN_DOI: hh.MA_CHUYEN_DOI,
            GHI_CHU: hh.GHI_CHU,
            BAO_HANH: hh.BAO_HANH
        }

        $http.post(origin + '/api/Api_HangHoa/UpdateHangHoa', data).then(function (response) {
            if (response.status == 200) {
                $scope.SuccessSystem("Bạn đã cập nhật thành công " + hh.MA_CHUAN);
                $scope.LoadListHangHoa();
            } else {
                $scope.ErrorSystem(response.data)
            }

        }, function () {
            $scope.ErrorSystem("Lỗi khi sửa")
        })

    };

    //==================================XUẤT EXCEL===========================================================================

    $scope.XuatExcel = function () {
        var file_name = 'Danh sách Hàng Hóa'
        var cancelstyle = {
            headers: true,
            column: { style: { Font: { Bold: "1" } } },
            columns: [
                { columnid: 'MA_HANG', title: 'Mã hệ thống', width: 150 },
                { columnid: 'MA_CHUAN', title: 'Mã hãng', width: 150 },
                { columnid: 'TEN_HANG', title: 'Tên hàng', width: 150 },
                { columnid: 'MA_NHOM_HANG', title: 'Hãng', width: 150 },
                { columnid: 'XUAT_XU', title: 'Xuất Xứ', width: 200 },
                { columnid: 'DON_VI_TINH', title: 'ĐVT ', width: 100 },
                { columnid: 'GIA_LIST', title: 'Giá list', width: 150 },
                { columnid: 'GIA_NHAP', title: 'Giá nhập', width: 150 },
                { columnid: 'GIA_BAN_1', title: 'Giá bán 1', width: 150 },
                { columnid: 'CK1', title: 'CK1', width: 150 },
                { columnid: 'GIA_BAN_2', title: 'Giá bán 2', width: 150 },
                { columnid: 'CK2', title: 'CK1', width: 150 },
                { columnid: 'GIA_BAN_3', title: 'Giá bán 3', width: 100 },
                { columnid: 'CK3', title: 'CK3', width: 150 },
                { columnid: 'GIA_BAN_4', title: 'Giá bán 4', width: 100 },
                { columnid: 'CK4', title: 'CK4', width: 150 },
                { columnid: 'DISCONTINUE', title: 'NSX', width: 150 },
                { columnid: 'GHI_CHU', title: 'Ghi chú', width: 100 },

            ],
        };

        alasql('SELECT * INTO XLSXML("' + file_name + '",?) FROM ?', [cancelstyle, $scope.DanhSachHangHoaExport]);
    };

  
    //=====================================Import nhanh Hàng hóa========================================================================
    $scope.ShowData = function () {
        $("textarea[name=listhang]").val(CKEDITOR.instances.listhang.getData());
        var listhang = $("[name=listhang]").val();
        var res = listhang.replace('<table', '<table id="tableupload" ');
        $scope.showdatacontent = res;
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
                MA_CHUAN: (document.getElementById("tableupload").rows[i].cells.item(0).innerText),
                TEN_HANG: (document.getElementById("tableupload").rows[i].cells.item(1).innerText),
                MA_NHOM_HANG: (document.getElementById("tableupload").rows[i].cells.item(2).innerText),
                DON_VI_TINH: (document.getElementById("tableupload").rows[i].cells.item(3).innerText),
                XUAT_XU: (document.getElementById("tableupload").rows[i].cells.item(4).innerText),
                GIA_LIST: parseFloat(document.getElementById("tableupload").rows[i].cells.item(5).innerText),
                GIA_NHAP: parseFloat(document.getElementById("tableupload").rows[i].cells.item(6).innerText),
                CK1: parseFloat(document.getElementById("tableupload").rows[i].cells.item(7).innerText),
                GIA_BAN_1: parseFloat(document.getElementById("tableupload").rows[i].cells.item(8).innerText),
                CK2: parseFloat(document.getElementById("tableupload").rows[i].cells.item(9).innerText),
                GIA_BAN_2: parseFloat(document.getElementById("tableupload").rows[i].cells.item(10).innerText),
                CK3: parseFloat(document.getElementById("tableupload").rows[i].cells.item(11).innerText),
                GIA_BAN_3: parseFloat(document.getElementById("tableupload").rows[i].cells.item(12).innerText),
                CK4: parseFloat(document.getElementById("tableupload").rows[i].cells.item(13).innerText),
                GIA_BAN_4: parseFloat(document.getElementById("tableupload").rows[i].cells.item(14).innerText),
                BAO_HANH: parseInt(document.getElementById("tableupload").rows[i].cells.item(15).innerText),
                GHI_CHU: parseInt(document.getElementById("tableupload").rows[i].cells.item(16).innerText),

            })
        }

        $http.post(origin + '/api/Api_HangHoa/ImportListHangHoa', $scope.ListHangImport).then(function (response) {
            if (response.status == 200) {
                $scope.SuccessSystem(response.data)
                $scope.LoadListHangHoa();
                $('#importlisthang').modal('hide');
            } else {
                $scope.ErrorSystem(response.data)
            }
        })
    }

    //=============================================================================================================
  
});
app.filter('unsafe', function ($sce) {
    return function (val) {
        return $sce.trustAsHtml(val);
    };
});
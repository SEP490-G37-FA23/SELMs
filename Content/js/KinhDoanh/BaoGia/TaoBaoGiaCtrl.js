var app = angular.module("myApp", []);

app.controller('TaoBaoGiaCtrl', function ($scope, $http, $sce) {
    var isadmin = $('#isadmin').val();
    var username = $('#username').val();
    var hovaten = $('#hovaten').val();


    $scope.isadmin = $('#isadmin').val();
    $scope.username = $('#username').val();

    $scope.ErrorSystem = function (errorMessage) {
        // This function handles errors and displays the error message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Error: ' + errorMessage;
        notificationElement.style.backgroundColor = '#f5aaaa';
        notificationElement.style.width = '500px';
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
        notificationElement.style.width = '500px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';


        // You can customize the notification style and appearance here.
    }

    function decimalAdjust(type, value, exp) {
        // Nếu exp có giá trị undefined hoặc bằng không thì...
        if (typeof exp === 'undefined' || +exp === 0) {
            return Math[type](value);
        }
        value = +value;
        exp = +exp;
        // Nếu value không phải là ố hoặc exp không phải là số nguyên thì...
        if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0)) {
            return NaN;
        }
        // Shift
        value = value.toString().split('e');
        value = Math[type](+(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp)));
        // Shift back
        value = value.toString().split('e');
        return +(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp));
    }

    // Làm tròn số thập phân (theo mốc số 5)
    if (!Math.round10) {
        Math.round10 = function (value, exp) {
            return decimalAdjust('round', value, exp);
        };
    }
    // Làm tròn số thập phân (về gần giá trị 0)
    if (!Math.floor10) {
        Math.floor10 = function (value, exp) {
            return decimalAdjust('floor', value, exp);
        };
    }
    // Làm tròn số thập phân (ra xa giá trị 0)
    if (!Math.ceil10) {
        Math.ceil10 = function (value, exp) {
            return decimalAdjust('ceil', value, exp);
        };
    }

    //------------------------------------------------

    $scope.BG = {
        TEN_CONG_TY: '',
        MA_KHACH_HANG: '',
        DIA_CHI_XUAT_HOA_DON: '',
        HOTLINE: '',
        DIA_CHI_GIAO_HANG: '',
        NGUOI_LIEN_HE: '',
        NGAY_GIAO_HANG: '',
        IS_HOI_HANG: false,
        IS_CM: false,
        IS_TINH_GIA: false,
        THUE_GTGT: 0,
        CM_DON_HANG: 0,
        ListDetail: [{
            NOI_DUNG_YEU_CAU_CUA_KHACH: '',
            THOI_GIAN_CAN_PHAN_HOI: '',
            MA_CHUAN: '',
            MA_HANG: '',
            TEN_HANG: '',
            HANG: '',
            MA_DIEU_CHINH: '',
            SO_LUONG: 0,
            DVT: '',
            GIA_LIST: '',
            CHIET_KHAU: '',
            GIA_NHAP: '',
            LOI_NHUAN: '',
            GIA_LIST: '',
            DON_GIA_BAN: '',
            THUE_GTGT: 0,
            VATGOC: 0,
            THANH_TIEN: 0,
            TIEN_THUE_GTGT: 0,
            TONG_THANH_TIEN: 0,
            GHI_CHU_BAO_GIA: '',
            GHI_CHU_HOI_HANG_THU_MUA: '',
            CM: 0,
            GIA_CM: 0,
            THUE_TNDN: 20,
            KHACH_NHAN: 0,
            GIA_BAO_DI_NET: 0,
            TIEN_THUE_TNDN: 0,
            THANH_TIEN_NET: 0
        }]
    };


    //===========================Thông tin khách hàng==============================
    //--------------------hàm tìm kiếm khách hàng ----------------------
    $scope.FindKHBG = function (tencongty) {
        var data_kh = {
            username: username,
            tukhoa1: tencongty,
            tukhoa2: '',
            tukhoa3: '',
            tukhoa4: '',
            tukhoa6: '',
            sotrang: 1
        }
        $http.post(origin + '/api/Api_KhachHang/GetListKhachHang', data_kh).then(function (response) {
            $scope.KhachHangBGCollection = response.data;
        });
    }

    // --------------------Hiện thị thông tin KH đã chọn-----------------
    $scope.showInfoKH_BaoGia = function (kh, BG) {
        $scope.MKH_SELLECT = kh.MA_KHACH_HANG;
        BG.TEN_CONG_TY = kh.TEN_CONG_TY;
        BG.MA_KHACH_HANG = kh.MA_KHACH_HANG;
        BG.VAN_PHONG_GIAO_DICH = kh.VAN_PHONG_GIAO_DICH;
        BG.HOTLINE = kh.HOTLINE;
        BG.DIA_CHI_XUAT_HOA_DON = kh.DIA_CHI_XUAT_HOA_DON;
        BG.DIEU_KHOAN_THANH_TOAN = kh.DIEU_KHOAN_THANH_TOAN;
        BG.MA_LOAI_KHACH = kh.MA_LOAI_KHACH
        if (kh.DIA_CHI_XUAT_HOA_DON == "" || kh.DIA_CHI_XUAT_HOA_DON == null) {
            BG.DIA_CHI_GIAO_HANG = kh.VAN_PHONG_GIAO_DICH;
        } else {
            BG.DIA_CHI_GIAO_HANG = kh.DIA_CHI_XUAT_HOA_DON;
        }
        $scope.showtable_KH_BaoGia = false;
        $scope.LoadListLienHeKH(kh.MA_KHACH_HANG, '');



    }

    //===========================Thông tin liên hệ khách hàng==============================

    // --------------- Danh sách liên hệ khách hàng ---------------
    $scope.LoadListLienHeKH = function (makhachhang, tukhoa) {
        var data = {
            tukhoa: makhachhang,
            tukhoa1: tukhoa
        }
        $http.post(origin + '/api/Api_KhachHang/GetListLienHeKH', data).then(function (response) {
            $scope.ListLienHeKH = response.data;
        });
    };

    //----------------Chọn liên hệ kh ---------------------
    $scope.SellectLHKH = function (Lh, BG) {
        BG.NGUOI_LIEN_HE = Lh.NGUOI_LIEN_HE;
        BG.EMAIL = Lh.EMAIL_CONG_TY;
        BG.HOTLINE_NGUOI_LH = Lh.SDT1;
    };

    //----------------Thêm liên hệ khách hàng------------------
    $scope.MKH_SELLECT = '';
    $scope.LienHeKH = {
        MA_KHACH_HANG: $scope.MKH_SELLECT,
        NGUOI_LIEN_HE: '',
        GIOI_TINH: true,
        SDT1: '',
        SDT2: '',
        EMAIL_CA_NHAN: '',
        EMAIL_CONG_TY: '',
        PHONG_BAN: '',
        CHUC_VU: '',
        NGAY_SINH: '',
        TEN_SALE_PHU_TRACH: hovaten,
        SALE_PHU_TRACH: username,
        TINH_TRANG_LAM_VIEC: 'Còn công tác',
        GHI_CHU: ''
    }
    $scope.CreateNewLienHeKH = function (lh) {
        if ($scope.MKH_SELLECT == '') {
            $scope.ErrorSystem('Phải chọn khách hàng trước khi thêm liên hệ!');
            return;
        } else {
            var ngaysinh = $("#ngaysinh").val();
            console.log(lh);
            var data = {
                MA_KHACH_HANG: $scope.MKH_SELLECT,
                NGUOI_LIEN_HE: lh.NGUOI_LIEN_HE,
                GIOI_TINH: lh.GIOI_TINH,
                SDT1: lh.SDT1,
                SDT2: lh.SDT2,
                EMAIL_CA_NHAN: lh.EMAIL_CA_NHAN,
                EMAIL_CONG_TY: lh.EMAIL_CONG_TY,
                PHONG_BAN: lh.PHONG_BAN,
                CHUC_VU: lh.CHUC_VU,
                NGAY_SINH_NEW: ngaysinh,
                GIOI_TINH: lh.GIOI_TINH,
                SALE_PHU_TRACH: $scope.LienHeKH.SALE_PHU_TRACH,
                TINH_TRANG_LAM_VIEC: lh.TINH_TRANG_LAM_VIEC,
                GHI_CHU: lh.GHI_CHU,
            }
            $http.post(origin + '/api/Api_KhachHang/AddNewLienHeKH', data).then(function (response) {
                if (response.status == 200) {
                    $scope.SuccessSystem('Thêm mới liên hệ khách hàng thành công');
                    $scope.LoadListLienHeKH($scope.MKH_SELLECT, '');
                } else {
                    $scope.ErrorSystem('Thêm mới liên hệ khách hàng thất bại!');
                }
            });
        }
    };
    //--------------- Danh sách nhân viên KD -----------------
    $scope.LoadListNhanVienKD = function (tukhoa) {
        var data = {
            tukhoa4: tukhoa
        }
        $http.post(origin + '/api/Api_KhachHang/GetListNhanVienSales', data).then(function (response) {
            $scope.ListNVSales = response.data;
        });
    };
    $scope.SelectDataSales = function (item, newLienHe) {
        $scope.LienHeKH.SALE_PHU_TRACH = item.USERNAME;
        newLienHe.TEN_SALE_PHU_TRACH = item.HO_VA_TEN;
    };

    //===========================Thông tin chi tiết thêm mới==============================
    //-----------------Tìm kiếm hàng hóa -----------------
    $scope.onHHFind = function (mh) {
        $scope.ListHH = []
        if (mh != "" && mh != null) {
            var thamso = {
                tukhoa: mh
            }
            $http.post(origin + "/api/Api_HangHoa/SearchListHangHoa", thamso).then(function (response) {
                $scope.ListHH = response.data;
            }, function (error) {
                console.log(error);
            })
        }
    }

    //----------------Chọn hàng hóa ------------------
    $scope.vat_default = 8 // mặc định thuế VAT ban đầu bằng 8, check tất cả các mã nhập vào trong báo giá
    $scope.SellectHH = function (hh, index, detail) {
        $scope.hh = hh;
        $scope.index = index;

        var isDuplicate = false;
        $scope.BG.ListDetail.forEach(function (element) {
            if (JSON.stringify(element.MA_HANG) === JSON.stringify(hh.MA_HANG)) {
                isDuplicate = true;
                return false;
            }
        });

        if (!isDuplicate) {
            detail.VATGOC = hh.VAT;
            detail.MA_HANG = hh.MA_HANG;
            detail.MA_CHUAN = hh.MA_CHUAN;
            detail.TEN_HANG = hh.TEN_HANG;
            detail.MA_DIEU_CHINH = hh.MA_CHUAN;
            detail.HANG = hh.MA_NHOM_HANG;
            detail.SO_LUONG = 0;
            detail.DVT = hh.DON_VI_TINH;
            detail.GIA_LIST = hh.GIA_LIST;
            detail.DON_GIA_BAN = hh.GIA_BAN_1;
            detail.GIA_NHAP = hh.GIA_NHAP;
            detail.CHIET_KHAU = 0;
            detail.THUE_GTGT = hh.VAT;
            detail.GIA_BAO_DI_NET = hh.GIA_LIST;
            detail.THONG_SO = hh.THONG_SO_KY_THUAT;
            detail.GIA_BAN_1_THUC_TE = hh.GIA_BAN_1;
            detail.GIA_BAN_2_THUC_TE = hh.GIA_BAN_2;
            detail.GIA_BAN_3_THUC_TE = hh.GIA_BAN_3;
            detail.GIA_BAN_4_THUC_TE = hh.GIA_BAN_4;

        } else {
            var x = confirm("Mã này đã có trong báo giá,bạn có chắc là vẫn muốn thêm mã mới hhông?");
            if (x) {
                detail.VATGOC = hh.VAT;
                detail.MA_HANG = hh.MA_HANG;
                detail.MA_CHUAN = hh.MA_CHUAN;
                detail.TEN_HANG = hh.TEN_HANG;
                detail.MA_DIEU_CHINH = hh.MA_CHUAN;
                detail.HANG = hh.MA_NHOM_HANG;
                detail.SO_LUONG = 0;
                detail.DVT = hh.DON_VI_TINH;
                detail.GIA_LIST = hh.GIA_LIST;
                detail.DON_GIA_BAN = hh.GIA_BAN_1;
                detail.GIA_NHAP = hh.GIA_NHAP;
                detail.CHIET_KHAU = 0;
                detail.THUE_GTGT = hh.VAT;
                detail.GIA_BAO_DI_NET = hh.GIA_LIST;
                detail.THONG_SO = hh.THONG_SO_KY_THUAT;
                detail.GIA_BAN_1_THUC_TE = hh.GIA_BAN_1;
                detail.GIA_BAN_2_THUC_TE = hh.GIA_BAN_2;
                detail.GIA_BAN_3_THUC_TE = hh.GIA_BAN_3;
                detail.GIA_BAN_4_THUC_TE = hh.GIA_BAN_4;
            } else {
                $scope.BG.ListDetail.splice(index, 1);
            }
        };
        detail.showtable_hanghoa = false;

    }
    //----------------Thêm dòng hàng hóa mới---------------------------------------------
    $scope.addNewProduct = function () {
        var hoahong = 0
        if ($scope.BG.CM_DON_HANG > 0) {
            hoahong = $scope.BG.CM_DON_HANG
        } else {
            hoahong = 0
        }
        $scope.BG.ListDetail.push({
            NOI_DUNG_YEU_CAU_CUA_KHACH: '',
            THOI_GIAN_CAN_PHAN_HOI: '',
            MA_CHUAN: '',
            MA_HANG: '',
            TEN_HANG: '',
            HANG: '',
            MA_DIEU_CHINH: '',
            SO_LUONG: 0,
            DVT: '',
            GIA_LIST: '',
            CHIET_KHAU: '',
            GIA_NHAP: '',
            LOI_NHUAN: '',
            GIA_LIST: '',
            DON_GIA_BAN: '',
            THUE_GTGT: 0,
            VATGOC: 0,
            THANH_TIEN: 0,
            TIEN_THUE_GTGT: 0,
            TONG_THANH_TIEN: 0,
            GHI_CHU_BAO_GIA: '',
            GHI_CHU_HOI_HANG_THU_MUA: '',
            CM: hoahong,
            GIA_CM: 0,
            THUE_TNDN: 20,
            KHACH_NHAN: 0,
            GIA_BAO_DI_NET: 0,
            TIEN_THUE_TNDN: 0,
            THANH_TIEN_NET: 0
        });
    };

    //-----------------Xóa dòng sản phẩm ----------------
    $scope.RemoveNew = function (detail, index) {
        $scope.BG.ListDetail.splice(index, 1);
    }
    //----------------Tính lại đơn giá, tiền trong bảng chi tiết hàng hóa----------------
    $scope.TinhLai = function (detail) {
        $scope.detail = detail;
        var bien_trung_gian = 0;
        $scope.detail.GIA_LIST = Math.round($scope.detail.GIA_LIST)
        $scope.detail.GIA_NHAP = Math.round($scope.detail.GIA_NHAP)
        $scope.detail.GIA_CM = Math.round($scope.detail.GIA_CM)
        $scope.detail.DON_GIA_BAN = Math.round($scope.detail.DON_GIA_BAN)
        $scope.detail.TIEN_THUE_TNDN = 0;
        //Bảng tính giá
        if (($scope.detail.GIA_LIST != null || $scope.detail.GIA_LIST != "") && $scope.detail.CHIET_KHAU != 0) {
            $scope.detail.GIA_BAO_DI_NET = Math.round10(($scope.detail.GIA_LIST) - ((($scope.detail.GIA_LIST) * ($scope.detail.CHIET_KHAU / 100))), 1);
        } else {
            $scope.detail.GIA_BAO_DI_NET = Math.round10(($scope.detail.GIA_NHAP) + ((($scope.detail.GIA_NHAP) * ($scope.detail.LOI_NHUAN / 100))), 1);
        };
        $scope.detail.GIA_CM = $scope.detail.GIA_BAO_DI_NET;

        //Bảng tính CM

        if ($scope.detail.CM > 0) {
            $scope.detail.GIA_CM = Math.round10((((100 - ($scope.detail.THUE_TNDN)) * $scope.detail.GIA_BAO_DI_NET) / (100 - ($scope.detail.CM) - ($scope.detail.THUE_TNDN))), 1);
            //Coi biến trung gian là phần chênh lệch
            bien_trung_gian = Math.round(($scope.detail.GIA_CM - $scope.detail.GIA_BAO_DI_NET) * $scope.detail.SO_LUONG);
            $scope.detail.KHACH_NHAN = Math.round(((100 - $scope.detail.THUE_TNDN) / 100) * bien_trung_gian); // Khách nhận  = số còn lại của Chênh lệch - tiền thuế tndn
            $scope.detail.TIEN_THUE_GTGT = Math.round(($scope.detail.THUE_TNDN / 100) * bien_trung_gian) || 0; // tiền thuế = % thu thuế TNDN * chênh lệch
        }
        else {
            if ($scope.detail.GIA_CM > 0) {
                $scope.detail.GIA_CM = Math.round($scope.detail.GIA_CM);
                $scope.detail.TIEN_THUE_TNDN = Math.round(((($scope.detail.GIA_CM) - $scope.detail.GIA_BAO_DI_NET) * ($scope.detail.THUE_TNDN / 100)) * $scope.detail.SO_LUONG);
                $scope.detail.KHACH_NHAN = Math.round(((($scope.detail.GIA_CM) - $scope.detail.GIA_BAO_DI_NET) - ($scope.detail.TIEN_THUE_TNDN / $scope.detail.SO_LUONG)) * $scope.detail.SO_LUONG);
            }
            else {
                $scope.detail.GIA_CM = Math.round10((((100 - ($scope.detail.THUE_TNDN)) * $scope.detail.GIA_BAO_DI_NET) / (100 - ($scope.detail.CM) - ($scope.detail.THUE_TNDN))), 1);
                bien_trung_gian = Math.round(($scope.detail.GIA_CM - $scope.detail.GIA_BAO_DI_NET) * $scope.detail.SO_LUONG);
                $scope.detail.KHACH_NHAN = Math.round(((100 - $scope.detail.THUE_TNDN) / 100) * bien_trung_gian); // Khách nhận  = số còn lại của Chênh lệch - tiền thuế tndn
                $scope.detail.TIEN_THUE_TNDN = Math.round(($scope.detail.THUE_TNDN / 100) * bien_trung_gian) || 0; // tiền thuế = % thu thuế TNDN * chênh lệch
            }
        }
        $scope.detail.DON_GIA_BAN = Math.round($scope.detail.GIA_CM);

        //Tính tiền
        $scope.detail.THANH_TIEN = Math.round($scope.detail.SO_LUONG * $scope.detail.DON_GIA_BAN);
        $scope.detail.THANH_TIEN_NET = Math.round($scope.detail.SO_LUONG * $scope.detail.GIA_BAO_DI_NET);
        $scope.detail.TIEN_THUE_GTGT = Math.round($scope.detail.SO_LUONG * $scope.detail.DON_GIA_BAN * ($scope.detail.THUE_GTGT / 100));
        $scope.detail.TONG_THANH_TIEN = Math.round($scope.detail.THANH_TIEN + $scope.detail.TIEN_THUE_GTGT);

        // Tính lại tổng tiền báo giá
        $scope.Resum();
    }



    //===========================Thông tin chung báo giá==============================
    //-----------------Hàm tính lại tiền báo giá----------------
    $scope.Resum = function () {
        var tong_gia_tri_thuc_te = 0;
        var tong_gia_tri_theo_hop_dong = 0;
        var tong_chi_phi_hoa_don = 0;
        var tong_khach_nhan = 0;
        var tien_hang_0 = 0;
        var tien_hang_8 = 0;
        var tien_hang_10 = 0;
        var tien_thue_gtgt_0 = 0;
        var tien_thue_gtgt_8 = 0;
        var tien_thue_gtgt_10 = 0;
        var tien_thu_dich_vu_vat = 0;
        for (var i = 0; i < $scope.BG.ListDetail.length; i++) {

            if ($scope.BG.ListDetail[i].THUE_GTGT == 8) {
                tien_hang_8 = Math.round(($scope.BG.ListDetail[i].THANH_TIEN + tien_hang_8));
                tien_thue_gtgt_8 = Math.round(($scope.BG.ListDetail[i].TIEN_THUE_GTGT + tien_thue_gtgt_8));
                tong_gia_tri_theo_hop_dong = Math.round(($scope.BG.ListDetail[i].THANH_TIEN + $scope.BG.ListDetail[i].TIEN_THUE_GTGT + tong_gia_tri_theo_hop_dong));
            }
            else if ($scope.BG.ListDetail[i].THUE_GTGT == 10) {
                tien_hang_10 = Math.round(($scope.BG.ListDetail[i].THANH_TIEN + tien_hang_10));
                tien_thue_gtgt_10 = Math.round(($scope.BG.ListDetail[i].TIEN_THUE_GTGT + tien_thue_gtgt_10));
                tong_gia_tri_theo_hop_dong = Math.round(($scope.BG.ListDetail[i].THANH_TIEN + $scope.BG.ListDetail[i].TIEN_THUE_GTGT + tong_gia_tri_theo_hop_dong));
            }
            else {
                tien_thu_dich_vu_vat = 0;
                tien_hang_0 = Math.round((($scope.BG.ListDetail[i].THANH_TIEN) + tien_hang_0));
                tien_thue_gtgt_0 = 0;
                tong_gia_tri_theo_hop_dong = Math.round(($scope.BG.ListDetail[i].THANH_TIEN + tong_gia_tri_theo_hop_dong));
            }
            tong_gia_tri_thuc_te = Math.round(($scope.BG.ListDetail[i].THANH_TIEN_NET + tong_gia_tri_thuc_te));
            tong_chi_phi_hoa_don = Math.round(($scope.BG.ListDetail[i].TIEN_THUE_TNDN + tong_chi_phi_hoa_don));
            tong_khach_nhan = Math.round(($scope.BG.ListDetail[i].KHACH_NHAN + tong_khach_nhan));
        }
        $scope.tien_thu_dich_vu_vat = tien_thu_dich_vu_vat;
        $scope.tong_gia_tri_thuc_te = tong_gia_tri_thuc_te;
        $scope.tong_gia_tri_theo_hop_dong = tong_gia_tri_theo_hop_dong;
        $scope.tong_chi_phi_hoa_don = tong_chi_phi_hoa_don;
        $scope.tong_khach_nhan = tong_khach_nhan;
        $scope.gia_tri_chenh_lech = Math.round($scope.tong_chi_phi_hoa_don + $scope.tong_khach_nhan);
        $scope.tien_hang_0 = tien_hang_0;
        $scope.tien_hang_8 = tien_hang_8;
        $scope.tien_hang_10 = tien_hang_10;
        $scope.tong_tien_hang = Math.round($scope.tien_hang_0 + $scope.tien_hang_8 + $scope.tien_hang_10);
        $scope.tien_thue_gtgt_0 = tien_thue_gtgt_0;
        $scope.tien_thue_gtgt_8 = tien_thue_gtgt_8;
        $scope.tien_thue_gtgt_10 = tien_thue_gtgt_10;
        $scope.thue_vat = Math.round($scope.tien_thue_gtgt_0 + $scope.tien_thue_gtgt_8 + $scope.tien_thue_gtgt_10);
        //$scope.tong_gia_tri_thu_cua_khach = Math.round($scope.tong_gia_tri_theo_hop_dong);
        $scope.tong_gia_tri_thu_cua_khach = Math.round(($scope.tong_gia_tri_thuc_te + $scope.thue_vat + $scope.tong_chi_phi_hoa_don + $scope.tien_thu_dich_vu_vat));
    }

    //-----------------Chọn điều khoản thanh toán báo giá

    $scope.SellectDKTT = function (BG, tukhoa) {
        BG.DIEU_KHOAN_THANH_TOAN = tukhoa;
    }

    //-----------------Lưu báo giá mới -------------------
    $scope.CreateNewBaoGia = function (BG) {
        $scope.ListChiTietBG = [];
        $scope.BG.ListDetail.forEach(element => {
            $scope.ChiTietBG = {
                MA_HANG: element.MA_HANG,
                MA_CHUAN: element.MA_CHUAN,
                TEN_HANG: element.TEN_HANG,
                HANG: element.HANG,
                MA_DIEU_CHINH: element.MA_DIEU_CHINH,
                DVT: element.DVT,
                SO_LUONG: element.SO_LUONG,
                GIA_LIST: element.GIA_LIST,
                DON_GIA_BAN: element.DON_GIA_BAN,
                CHIET_KHAU: element.CHIET_KHAU,
                LOI_NHUAN: element.LOI_NHUAN,
                DON_GIA_BAO_DI_NET: element.DON_GIA_BAO_DI_NET,
                CM: element.CM,
                GIA_CM: element.GIA_CM,
                KHACH_NHAN: element.KHACH_NHAN,
                THUE_TNDN: element.THUE_TNDN,
                TIEN_THUE_TNDN: element.TIEN_THUE_TNDN,
                THUE_GTGT: element.THUE_GTGT,
                THANH_TIEN_HANG: element.THANH_TIEN,
                TIEN_THUE_GTGT: element.TIEN_THUE_GTGT,
                TONG_THANH_TIEN: element.TONG_THANH_TIEN,
                GHI_CHU_BAO_GIA: element.GHI_CHU_BAO_GIA,
                GHI_CHU_HOI_HANG_THU_MUA: element.GHI_CHU_HOI_HANG_THU_MUA,
                DA_XUAT: false,
                SL_DA_XUAT: 0,
                GIA_BAN_1_THUC_TE: element.GIA_BAN_1_THUC_TE,
                GIA_BAN_2_THUC_TE: element.GIA_BAN_2_THUC_TE,
                GIA_BAN_3_THUC_TE: element.GIA_BAN_3_THUC_TE,
                GIA_BAN_4_THUC_TE: element.GIA_BAN_4_THUC_TE,
                IS_HOI_HANG: element.IS_HOI_HANG

            }
           $scope.ListChiTietBG.push($scope.ChiTietBG);
        })

        console.log(BG);
        $scope.NEW_BAO_GIA = {
            SALE_BAO_GIA: username,
            MA_KHACH_HANG: BG.MA_KHACH_HANG,
            TEN_CONG_TY: BG.TEN_CONG_TY,
            HOTLINE: BG.HOTLINE,
            EMAIL: BG.EMAIL,
            NGUOI_LIEN_HE: BG.NGUOI_LIEN_HE,
            HOTLINE_NGUOI_LH: BG.HOTLINE_NGUOI_LH,
            DIA_CHI_GIAO_HANG: BG.DIA_CHI_GIAO_HANG,
            NGAY_GIAO_HANG: $('#ngaygiaohang').val(),
            PHI_VAN_CHUYEN: BG.PHI_VAN_CHUYEN,
            DIEU_KHOAN_THANH_TOAN: BG.DIEU_KHOAN_THANH_TOAN,
            PHUONG_THUC_THANH_TOAN: BG.PHUONG_THUC_THANH_TOAN,
            GHI_CHU_BAO_GIA: BG.GHI_CHU_BAO_GIA,
            GHI_CHU_TONG: BG.GHI_CHU_TONG,
            HIEU_LUC_BAO_GIA: BG.HIEU_LUC_BAO_GIA,
            SO_NGAY_DUOC_NO: BG.SO_NGAY_DUOC_NO,
            THUE_GTGT: BG.THUE_GTGT,
            CM_DON_HANG: BG.CM_DON_HANG,
            IS_CM: BG.IS_CM,
            IS_HOI_HANG: BG.IS_HOI_HANG,
            IS_TINH_GIA: BG.IS_TINH_GIA,
            DA_TRUNG: BG.DA_TRUNG,
            DA_XUAT: BG.DA_XUAT,
            DA_HUY: BG.DA_HUY,
            TONG_TIEN_HANG: $scope.tong_tien_hang,
            TONG_TIEN_THUE_GTGT: $scope.thue_vat,
            TONG_THANH_TIEN: $scope.tong_tien_hang + $scope.thue_vat,
            TONG_GT_CHENH_LECH: $scope.gia_tri_chenh_lech,
            TONG_CHI_PHI_HOA_DON: $scope.tong_chi_phi_hoa_don,
            THUC_NHAN_CUA_KHACH: $scope.tong_khach_nhan,
            TONG_GT_DON_HANG_THEO_HOP_DONG: $scope.tong_gia_tri_theo_hop_dong,
            THU_PHI_DICH_VU_HOA_DON: $scope.tien_thu_dich_vu_vat,
            TONG_GIA_TRI_THU_CUA_KHACH: $scope.tong_gia_tri_thu_cua_khach,
            LIST_CT_BG: $scope.ListChiTietBG

        }

        $scope.SuccessSystem("Hệ thống đang lưu dữ liệu, bạn vui lòng chờ trong giây lát");
        $http.post(origin + "/api/Api_BaoGia/CreateNewBaoGia", $scope.NEW_BAO_GIA)
            .then(function successCallback(response) {
                if (response.status != 200) {
                    $scope.ErrorSystem(response.data)
                } else {
                    $scope.datareturn = response.data
                    $scope.SuccessSystem("Tạo thành công phiếu báo giá số " + $scope.datareturn.SO_BAO_GIA);

                }
            })
    }
    //===========================================================================================================================================================

});
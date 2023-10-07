var app = angular.module("myApp", []);

app.controller('DanhSachBaoGiaCtrl', function ($scope, $http, $sce) {
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
    $scope.tranghientai = 1;
    //--------------- Danh sách bao gia KD -----------------
    $scope.LoadListBaoGiaKD = function () {
        var data = {
            username: username,
            tukhoa1: '',
            tukhoa2: '',
            tukhoa3: '',
            tukhoa6: '',
            tukhoa4: '',
            sotrang:1
        }
        $http.post(origin + '/api/Api_BaoGia/GetListBaoGiaKD', data).then(function (response) {
            $scope.ListBaoGiaKD = response.data;
            console.log($scope.ListBaoGiaKD);
        });
    };
    $scope.LoadListBaoGiaKD();

    //-------------- Chi tiết báo giá --------------------

    $scope.Detail = {
        ListHangHoa: [],
        ListTaiKhoan: [],
        ListAdd: [],
        ListAddGop: [],
        SearchHang: [],
        ListTachPhieu: [],

    }
    $scope.Detail.ListAdd.push({

        MaHang: null,
        TenHang: null,
        TKKho: null,
        DonGia: null,
        SoLuong: null,
        DVT: null,
        TKNo: null,
        TKCo: null,
        DonGiaVon: null
    });

    $scope.SO_BAO_GIA_SELLECT = '';
    $scope.transfer = function (transfer) {
        $scope.SO_BAO_GIA_SELLECT = transfer.SO_BAO_GIA;
        $scope.listxoa = [];
        $scope.Detail.ListAdd = [];
        $scope.thongtinbaogia = transfer;
        var dataBG = {
            sochungtu: transfer.SO_BAO_GIA,
            username: username
        }
        $http.post(origin + '/api/Api_BaoGia/ThongTinPhieuBaoGia', dataBG).then(function (response) {
            $scope.thongtinbaogia = response.data.BHBaoGia;
            $scope.thongtinbaogia.SO_BAO_GIA = response.data.BHBaoGia.SO_BAO_GIA;
            $scope.thongtinbaogia.TEN_CONG_TY = response.data.BHBaoGia.TEN_CONG_TY;
            $scope.thongtinbaogia.NV_BAO_GIA = response.data.BHBaoGia.NV_BAO_GIA;
            $scope.thongtinbaogia.DIA_CHI_GIAO_HANG = response.data.BHBaoGia.DIA_CHI_GIAO_HANG;
            $scope.Detail.ListAdd = response.data.ChiTietBaoGia;
            $scope.thongtinchitiet = response.data.ChiTietBaoGia;
            var tien_thue_gtgt = 0;
            var thanh_tien = 0;
            var tongsoluong = 0;
            for (i = 0; i < $scope.Detail.ListAdd.length; i++) {
                thanh_tien = Math.round($scope.Detail.ListAdd[i].THANH_TIEN_HANG + thanh_tien)
                tien_thue_gtgt = Math.round(tien_thue_gtgt + $scope.Detail.ListAdd[i].TIEN_THUE_GTGT)
                tongsoluong = Math.round(tongsoluong + $scope.Detail.ListAdd[i].SO_LUONG)
            }
            console.log($scope.thongtinbaogia);
            console.log($scope.thongtinchitiet);
            //for (i = 0; i < $scope.Detail.ListAdd.length; i++) {
            //    if ($scope.Detail.ListAdd[i].SL_DA_TRA > 0) {
            //        $scope.thamchieutralai = true;
            //        break;
            //    }
            //    else {
            //        $scope.thamchieutralai = false;
            //        continue;
            //    }
            //}
            //$scope.TONG_SO_LUONG = tongsoluong;
            //$scope.THUE_GTGT = $scope.Detail.ListAdd[0].THUE_GTGT;
            //$scope.THANH_TIEN_HOA_DON = thanh_tien;
            //$scope.TIEN_THUE_HOA_DON = tien_thue_gtgt;
            //$scope.TONG_TIEN_HOA_DON = ($scope.THANH_TIEN_HOA_DON + $scope.TIEN_THUE_HOA_DON);
            //$scope.SO_TIEN_BANG_CHU = (docso(parseInt($scope.TONG_TIEN_HOA_DON)));
            //$scope.SO_TIEN_BANG_CHU = jsUcfirst($scope.SO_TIEN_BANG_CHU);
        })

        

    };

    //In chi tiết báo giá=======================================================================
    $scope.InBaoGia = function (sobaogia) {
        window.open(window.location.origin + '/BaoGia/InPXBaoGia/' + sobaogia, '_blank');
    }

    //------------------------
    function jsUcfirst(string) {
        return string.charAt(1).toUpperCase() + string.slice(2);
    }
    var mangso = [' không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín'];
    function dochangchuc(so, daydu) {
        var chuoi = "";
        chuc = Math.floor(so / 10);
        donvi = so % 10;
        if (chuc > 1) {
            chuoi = " " + mangso[chuc] + " mươi";
            if (donvi == 1) {
                chuoi += " mốt";
            }
        } else if (chuc == 1) {
            chuoi = " mười";
            if (donvi == 1) {
                chuoi += " một";
            }
        } else if (daydu && donvi > 0) {
            chuoi = " lẻ";
        }
        if (donvi == 5 && chuc >= 1) {
            chuoi += " lăm";
        } else if (donvi > 1 || (donvi == 1 && chuc == 0)) {
            chuoi += " " + mangso[donvi];
        }
        return chuoi;
    }
    function docblock(so, daydu) {
        var chuoi = "";
        tram = Math.floor(so / 100);
        so = so % 100;
        if (daydu || tram > 0) {
            chuoi = " " + mangso[tram] + " trăm";
            chuoi += dochangchuc(so, true);
        } else {
            chuoi = dochangchuc(so, false);
        }
        return chuoi;
    }
    function dochangtrieu(so, daydu) {
        var chuoi = "";
        trieu = Math.floor(so / 1000000);
        so = so % 1000000;
        if (trieu > 0) {
            chuoi = docblock(trieu, daydu) + " triệu";
            daydu = true;
        }
        nghin = Math.floor(so / 1000);
        so = so % 1000;
        if (nghin > 0) {
            chuoi += docblock(nghin, daydu) + " nghìn";
            daydu = true;
        }
        if (so > 0) {
            chuoi += docblock(so, daydu);
        }
        return chuoi;
    }
    function docso(so) {
        if (so == 0) return mangso[0];
        var chuoi = "", hauto = "";
        do {
            ty = so % 1000000000;
            so = Math.floor(so / 1000000000);
            if (so > 0) {
                chuoi = dochangtrieu(ty, true) + hauto + chuoi;
            } else {
                chuoi = dochangtrieu(ty, false) + hauto + chuoi;
            }
            hauto = " tỷ";
        } while (so > 0);
        return chuoi;
    }
});
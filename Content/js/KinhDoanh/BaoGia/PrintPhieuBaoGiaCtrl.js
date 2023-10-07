var app = angular.module("myApp", []);

app.factory('Excel', function ($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {
        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };
})
app.controller('PrintPhieuBaoGiaCtrl', function ($scope, $http) {
    $scope.d = new Date();

    function jsUcfirst(string) {
        return string.charAt(1).toUpperCase() + string.slice(2);
    }



    $scope.show = true;
    $scope.showghichuct = true;
    $scope.showghichucn = true;
    $scope.ghichucn = true;
    $scope.gialist = false;
    $scope.showSoTienCanThu = false;
    $scope.checkbox = function (check) {
        if (check == true) {
            $scope.showghichu = true;
        }
        else {
            $scope.showghichu = false;
        }
    }
    $scope.checkboxghichuct = function (ghichuct) {
        if (ghichuct == true) {
            $scope.showghichuct = false;
        }
        else {
            $scope.showghichuct = true;
        }
    }
    $scope.checkboxghichucn = function (ghichucn) {
        if (ghichucn == true) {
            $scope.showghichucn = true;
        }
        else {
            $scope.showghichucn = false;
        }
    }
    $scope.checkgialist = function (gialist) {
        if (gialist == true) {
            $scope.gialist = true;
        }
        else {
            $scope.gialist = false;
        }
    }
    $scope.thongtinchung = {
        NGUOI_LIEN_HE: null,
        TEN_CONG_TY: null,
        DIA_CHI_XUAT_HOA_DON: null,
        DIA_CHI_CONG_TY: null

    };
    //this gets the full url
    var macongty = $('#macongty').val();
    var username = $('#username').val();
    var hovaten = $('#hovaten').val();

    $scope.username = $('#username').val();

    $scope.hovaten = $('#hovaten').val();
    //$scope.maphongban = $('#maphongban').val();
    //$scope.mapb = $scope.maphongban.substring(0, 4);
    $scope.nguoilapphieu = hovaten;
    var url = document.location.href;
    //this removes the anchor at the end, if there is one
    url = url.substring(0, (url.indexOf("#") == -1) ? url.length : url.indexOf("#"));
    //this removes the query after the file name, if there is one
    url = url.substring(0, (url.indexOf("?") == -1) ? url.length : url.indexOf("?"));
    //this removes everything before the last slash in the path
    url = url.substring(url.lastIndexOf("/") + 1, url.length);
    //return
    $scope.nam = new Date().getFullYear()

    $scope.Date = new Date()

    //hàm tìm kiếm
    $scope.getdataphieuxuatkho = function (sochungtu) {
        $scope.TongTienChuaThue8 = 0;
        $scope.Tongthue8 = 0;
        $scope.TongTienChuaThue10 = 0;
        $scope.Tongthue10 = 0;
        $scope.TongTienChuaThue0 = 0;
        $scope.Tongthue0 = 0;
        $scope.TongTienChuaThue5 = 0;
        $scope.Tongthue5 = 0;
        $scope.TongTienHangHoaKhongChiuThue = 0;
        $scope.TongThanhTienTatCa = 0
        $scope.TongTienThueTatCa = 0
        var so_luong = 0;
        var tong_tien = 0;
        $scope.SO_LUONG_IN = 0
        $scope.ListMSTPrint = []
        $http.post(origin + '/api/Api_BaoGia/ThongTinPhieuBG/' + sochungtu + '/' + $scope.username)
            .then(function (response) {
                if (response.data) {
                    $scope.thongtinbaogia = response.data;
                    $scope.thongtinchung = $scope.thongtinbaogia.BHBaoGia;
                    $scope.thongtinchitiet = $scope.thongtinbaogia.ChiTietBaoGia;
                    $scope.ngay = $scope.thongtinchung.NGAY_CHUNG_TU.substring(0, 2);
                    $scope.thang = $scope.thongtinchung.NGAY_CHUNG_TU.substring(3, 5);
                    $scope.namh = $scope.thongtinchung.NGAY_CHUNG_TU.substring(6, 10);
                }
                for (i = 0; i < $scope.thongtinchitiet.length; i++) {
                    so_luong = so_luong + $scope.thongtinchitiet[i].SO_LUONG;
                    tong_tien = tong_tien + $scope.thongtinchitiet[i].THANH_TIEN_HANG;
                    tk_no = $scope.thongtinchitiet[i].TK_NO;
                    tk_co = $scope.thongtinchitiet[i].TK_CO;
                    $scope.thongtinchitiet[i].THUE_GTGT = $scope.thongtinchitiet[i].THUE_GTGT + '%';

                    if (parseInt($scope.thongtinchitiet.THUE_GTGT) === 8) {
                        $scope.TongTienChuaThue8 += parseInt($scope.thongtinchitiet[i].THANH_TIEN_HANG)
                        $scope.Tongthue8 += parseInt(($scope.thongtinchitiet[i].THANH_TIEN_HANG / 100) * $scope.thongtinchitiet.THUE_GTGT)
                    }
                    else if (parseInt($scope.thongtinchitiet.THUE_GTGT) === 10) {
                        $scope.TongTienChuaThue10 += parseInt($scope.thongtinchitiet[i].THANH_TIEN_HANG)
                        $scope.Tongthue10 += parseInt(($scope.thongtinchitiet[i].THANH_TIEN_HANG / 100) * $scope.thongtinchitiet.THUE_GTGT)
                    }
                    else if (parseInt($scope.thongtinchitiet.THUE_GTGT) === 5) {
                        $scope.TongTienChuaThue5 += parseInt($scope.thongtinchitiet[i].THANH_TIEN_HANG)
                        $scope.Tongthue5 += parseInt(($scope.thongtinchitiet[i].THANH_TIEN_HANG / 100) * $scope.thongtinchitiet.THUE_GTGT)
                    }
                    else if (parseInt($scope.thongtinchitiet.THUE_GTGT) === 0) {
                        $scope.TongTienChuaThue0 += parseInt($scope.thongtinchitiet[i].THANH_TIEN_HANG)
                        $scope.Tongthue0 += parseInt(($scope.thongtinchitiet[i].THANH_TIEN_HANG / 100) * $scope.thongtinchitiet.THUE_GTGT)
                    } else {
                        $scope.TongTienHangHoaKhongChiuThue += parseInt($scope.thongtinchitiet[i].THANH_TIEN_HANG)
                    }
                }
                $scope.tongsoluong = so_luong;
                $scope.tong_tien = tong_tien;
                if ($scope.thongtinchung.SO_TIEN_CAN_THU > 0) {
                    $scope.showSoTienCanThu = true;
                } else {
                    $scope.thongtinchung.SO_TIEN_CAN_THU = $scope.thongtinchung.TONG_TIEN
                }


                if ($scope.showSoTienCanThu == true) {
                    $scope.so_tien_bang_chu = (docso(parseInt($scope.thongtinchung.SO_TIEN_CAN_THU)));
                    $scope.so_tien_bang_chu = jsUcfirst($scope.so_tien_bang_chu);
                } else {
                    $scope.so_tien_bang_chu = (docso(parseInt($scope.thongtinchung.TONG_TIEN)));
                    $scope.so_tien_bang_chu = jsUcfirst($scope.so_tien_bang_chu);
                }



                $scope.TK_NO = tk_no;
                $scope.TK_CO = tk_co;

            }, function (error) {
                console.log(error);
            })
        console.log($scope.thongtinchung);
    }
    $scope.getdataphieuxuatkho(url);
    $scope.CurrentDate = new Date();


    $scope.thaydoisotien = function () {
        $scope.thongtinchung.TONG_TIEN = $scope.sotienmoi;
        $scope.so_tien_bang_chu = (docso(parseInt($scope.sotienmoi)));
        $scope.so_tien_bang_chu = jsUcfirst($scope.so_tien_bang_chu);
    }




    $scope.printToCart = function (printSectionId) {
        var elem = document.getElementById("myDate");
        var elem2 = document.getElementById("ghichu");
        var elem3 = document.getElementById("ghichucn");
        if (elem != null) {
            elem.parentElement.removeChild(elem);
        }
        if (elem2 != null) {
            elem2.parentElement.removeChild(elem2);
        }
        if (elem3 != null) {
            elem3.parentElement.removeChild(elem3);
        }

        var innerContents = document.getElementById(printSectionId).innerHTML;
        var popupWinindow = window.open('', '_blank', 'width=600,height=700,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
        popupWinindow.document.open();
        popupWinindow.document.write('<html><head><link rel="styl  esheet" type="text/css" href="style.css" /><link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css"></head><body onload="window.print()">' + innerContents + '</html>');
        popupWinindow.document.close();
        var data_in = {
            sochungtu: url
        }
        $http.post(origin + '/api/Api_XuatKho/XacNhanDaIn', data_in).then(function () {

        })
    }
    document.onkeydown = keydown;
    function keydown(evt) {

        if (!evt) evt = event;

        if (evt.ctrlKey && evt.keyCode == 80) {

            $scope.printToCart('printSectionId');
        }
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


    $scope.tableToExcel = (function () {
        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><style>td{font-size:18px !important}</style><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
        return function (table, name) {
            table = document.getElementById('printSectionId')
            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
            window.location.href = uri + base64(format(template, ctx))
        }
    })

    $scope.tablesToExcelMultiTabs = (function ($) {
        var uri = 'data:application/vnd.ms-excel;base64,'
            , html_start = `<html xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40">`
            , template_ExcelWorksheet = `<x:ExcelWorksheet><x:Name>{SheetName}</x:Name><x:WorksheetSource HRef="sheet{SheetIndex}.htm"/></x:ExcelWorksheet>`
            , template_ListWorksheet = `<o:File HRef="sheet{SheetIndex}.htm"/>`
            , template_HTMLWorksheet = `
------=_NextPart_dummy
Content-Location: sheet{SheetIndex}.htm
Content-Type: text/html; charset=windows-1252

` + html_start + `
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
  <link id="Main-File" rel="Main-File" href="../WorkBook.htm">
  <link rel="File-List" href="filelist.xml">
</head>
<body><table>{SheetContent}</table></body>
</html>`
            , template_WorkBook = `MIME-Version: 1.0
X-Document-Type: Workbook
Content-Type: multipart/related; boundary="----=_NextPart_dummy"

------=_NextPart_dummy
Content-Location: WorkBook.htm
Content-Type: text/html; charset=windows-1252

` + html_start + `
<head>
<meta name="Excel Workbook Frameset">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="File-List" href="filelist.xml">
<!--[if gte mso 9]><xml>
 <x:ExcelWorkbook>
  <x:ExcelWorksheets>{ExcelWorksheets}</x:ExcelWorksheets>
  <x:ActiveSheet>1</x:ActiveSheet>
 </x:ExcelWorkbook>
</xml><![endif]-->
</head>
<frameset>
  <frame src="sheet0.htm" name="frSheet">
  <noframes><body><p>This page uses frames, but your browser does not support them.</p></body></noframes>
</frameset>
</html>
{HTMLWorksheets}
Content-Location: filelist.xml
Content-Type: text/xml; charset="utf-8"

<xml xmlns:o="urn:schemas-microsoft-com:office:office">
  <o:MainFile HRef="../WorkBook.htm"/>
  {ListWorksheets}
  <o:File HRef="filelist.xml"/>
</xml>
------=_NextPart_dummy--
`
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
        return function (tables, filename) {
            var context_WorkBook = {
                ExcelWorksheets: ''
                , HTMLWorksheets: ''
                , ListWorksheets: ''
            };
            var tables = jQuery(tables);
            var dt = new Date();
            var day = dt.getDate();
            var month = dt.getMonth() + 1;
            var year = dt.getFullYear();
            var hour = dt.getHours();
            var mins = dt.getMinutes();
            var postfix = day + "." + month + "." + year + "_" + hour + "." + mins;
            $.each(tables, function (SheetIndex) {
                var $table = $('#' + tables[SheetIndex]);

                var SheetName = $table.attr('data-SheetName');
                if ($.trim(SheetName) === '') {
                    SheetName = 'Sheet' + SheetIndex;
                }
                context_WorkBook.ExcelWorksheets += format(template_ExcelWorksheet, {
                    SheetIndex: SheetIndex
                    , SheetName: SheetName
                });
                context_WorkBook.HTMLWorksheets += format(template_HTMLWorksheet, {
                    SheetIndex: SheetIndex
                    , SheetContent: $table.html()
                });
                context_WorkBook.ListWorksheets += format(template_ListWorksheet, {
                    SheetIndex: SheetIndex
                });
            });

            var link = document.createElement("A");
            link.href = uri + base64(format(template_WorkBook, context_WorkBook));
            link.download = "PrintXuatKhoPacking" + postfix + ".xls";
            link.target = '_blank';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
    })(jQuery);

    $scope.tablesToExcelPackingList = (function ($) {
        var uri = 'data:application/vnd.ms-excel;base64,'
            , html_start = `<html xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40">`
            , template_ExcelWorksheet = `<x:ExcelWorksheet><x:Name>{SheetName}</x:Name><x:WorksheetSource HRef="sheet{SheetIndex}.htm"/></x:ExcelWorksheet>`
            , template_ListWorksheet = `<o:File HRef="sheet{SheetIndex}.htm"/>`
            , template_HTMLWorksheet = `
------=_NextPart_dummy
Content-Location: sheet{SheetIndex}.htm
Content-Type: text/html; charset=windows-1252

` + html_start + `
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
  <link id="Main-File" rel="Main-File" href="../WorkBook.htm">
  <link rel="File-List" href="filelist.xml">
</head>
<body><table>{SheetContent}</table></body>
</html>`
            , template_WorkBook = `MIME-Version: 1.0
X-Document-Type: Workbook
Content-Type: multipart/related; boundary="----=_NextPart_dummy"

------=_NextPart_dummy
Content-Location: WorkBook.htm
Content-Type: text/html; charset=windows-1252

` + html_start + `
<head>
<meta name="Excel Workbook Frameset">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="File-List" href="filelist.xml">
<!--[if gte mso 9]><xml>
 <x:ExcelWorkbook>
  <x:ExcelWorksheets>{ExcelWorksheets}</x:ExcelWorksheets>
  <x:ActiveSheet>1</x:ActiveSheet>
 </x:ExcelWorkbook>
</xml><![endif]-->
</head>
<frameset>
  <frame src="sheet0.htm" name="frSheet">
  <noframes><body><p>This page uses frames, but your browser does not support them.</p></body></noframes>
</frameset>
</html>
{HTMLWorksheets}
Content-Location: filelist.xml
Content-Type: text/xml; charset="utf-8"

<xml xmlns:o="urn:schemas-microsoft-com:office:office">
  <o:MainFile HRef="../WorkBook.htm"/>
  {ListWorksheets}
  <o:File HRef="filelist.xml"/>
</xml>
------=_NextPart_dummy--
`
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
        return function (tables, filename) {
            var context_WorkBook = {
                ExcelWorksheets: ''
                , HTMLWorksheets: ''
                , ListWorksheets: ''
            };
            var tables = jQuery(tables);
            var dt = new Date();
            var day = dt.getDate();
            var month = dt.getMonth() + 1;
            var year = dt.getFullYear();
            var hour = dt.getHours();
            var mins = dt.getMinutes();
            var postfix = day + "." + month + "." + year + "_" + hour + "." + mins;
            $.each(tables, function (SheetIndex) {
                var $table = $('#' + tables[SheetIndex]);

                var SheetName = $table.attr('data-SheetName');
                if ($.trim(SheetName) === '') {
                    SheetName = 'Sheet' + SheetIndex;
                }
                context_WorkBook.ExcelWorksheets += format(template_ExcelWorksheet, {
                    SheetIndex: SheetIndex
                    , SheetName: SheetName
                });
                context_WorkBook.HTMLWorksheets += format(template_HTMLWorksheet, {
                    SheetIndex: SheetIndex
                    , SheetContent: $table.html()
                });
                context_WorkBook.ListWorksheets += format(template_ListWorksheet, {
                    SheetIndex: SheetIndex
                });
            });

            var link = document.createElement("A");
            link.href = uri + base64(format(template_WorkBook, context_WorkBook));
            link.download = "PrintXuatKhoPacking" + postfix + ".xls";
            link.target = '_blank';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
    })(jQuery);

});

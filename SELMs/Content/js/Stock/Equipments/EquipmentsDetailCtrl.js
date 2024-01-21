var app = angular.module("myApp", []);

app.controller('EquipmentsDetailCtrl', function ($scope, $http, $sce) {

    var username = $('#username').val();
    var fullname = $('#fullname').val();
    var isadmin = $('#isadmin').val();
    var role = $('#role').val();

    $scope.username = $('#username').val();
    $scope.ErrorSystem = function (errorMessage) {
        // This function handles errors and displays the error message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Error: ' + errorMessage;
        notificationElement.style.backgroundColor = '#f5aaaa';
        notificationElement.style.width = '1000px';
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
        notificationElement.style.width = '1000px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';
    };

    //this gets the full url
    var url = document.location.href;
    //this removes the anchor at the end, if there is one
    url = url.substring(0, (url.indexOf("#") == -1) ? url.length : url.indexOf("#"));
    //this removes the query after the file name, if there is one
    url = url.substring(0, (url.indexOf("?") == -1) ? url.length : url.indexOf("?"));
    //this removes everything before the last slash in the path
    url = url.substring(url.lastIndexOf("/") + 1, url.length);
    //return

    $scope.ListUnits = ['Cái', 'Chiếc', 'Bộ', 'Cặp', 'Hộp'];


    $scope.LoadCategoriesList = function () {

        $http.post(origin + '/api/v1/categories').then(function (response) {
            $scope.ListCategories = response.data;
        });
    }

    $scope.LoadCategoriesList();

    $scope.LoadLocationsList = function () {
        var data = {
            username: username,
            text: '',
            id: -1
        }
        $http.post(origin + '/api/v1/locations', data).then(function (response) {
            $scope.ListLocations = response.data;
            $scope.sumLocations = $scope.ListLocations.length;
        });
    }

    $scope.LoadLocationsList();

    $scope.text = fullname;
    $scope.LoadMembersList = function (text) {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            text: text
        }
        $http.post(origin + '/api/v1/members', data).then(function (response) {
            $scope.ListMembers = response.data;
        });
    }
    $scope.LoadMembersList();

    $scope.HandelMember = function (mem) {
        $scope.text = mem.fullname;
        $scope.NewEquip.responsibler = mem.username;
    }
    $scope.HandelComponentEquip = function (ListComponentEquips) {
        ListComponentEquips.push({
            system_equipment_code: '',
            standard_equipment_code: '',
            equipment_name: '',
            usage_status: ''
        });
    }
    $scope.LoadEquipmentsList = function (text) {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            text: '',
            text1: '',
            text2: text,
            text3: ''

        }
        $http.post(origin + '/api/v1/equipments', data).then(function (response) {
            $scope.ListEquips = response.data;
        });
    }
    $scope.DeleteComponentEquip = function (ListComponentEquips, index) {
        ListComponentEquips.splice(index, 1);
    }
    $scope.HandelEquip = function (eq, item) {
        item.system_equipment_code = eq.system_equipment_code;
        item.standard_equipment_code = eq.standard_equipment_code;
        item.equipment_name = eq.equipment_name;
        item.usage_status = eq.usage_status;

    }
    $scope.image_url = '';
    $scope.GetDetailEquip = function (system_equip_code) {
        new QRCode(document.getElementById('id_qrcode'), origin + '/Equipments/EquipmentDetails/' + system_equip_code);
        var partialUrl = origin + '/api/v1/equipments/detail/' + system_equip_code;
        $http.post(partialUrl)
            .then(function (response) {
                console.log(response.data);
                $scope.DetailEquip = response.data.equip;
                $scope.ListComponentEquips = response.data.ListComponentEquips;
                var listImage = response.data.ListImageEquips;
                var partialUrl = origin + '/api/v1/images/' + listImage[0].image_id;
                    //+ listImage[0].image_id;
                $http.get(partialUrl).then(function (res) {
                    console.log(res)
                    $scope.image_url = res.config.url;
                })
               

                
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.GetDetailEquip(url);
    // Assume raw data is a flat array of RGBA values
    // For example, [R1, G1, B1, A1, R2, G2, B2, A2, ...]

    $scope.ValidateDataInput = function (equip) {
        var regex1 = /\S/;
        var regex2 = /^\d+$/;
        console.log('standard code', equip.standard_equipment_code);
        console.log('equipment name', equip.equipment_name);
        console.log('price', equip.price);
        if (!regex1.test(equip.standard_equipment_code) || equip.standard_equipment_code == '') {
            $scope.ErrorSystem('Vui lòng nhập mã chuẩn thiết bị.');
            console.log('standard code error.');
        }
        else if (!regex1.test(equip.equipment_name) || equip.equipment_name == '') {
            $scope.ErrorSystem('Vui lòng nhập tên thiết bị.');
            console.log('equipment name error.');
        }
        else if (!regex2.test(equip.price)) {
            $scope.ErrorSystem('Vui lòng nhập giá thiết bị.');
            console.log('price error');
        }
        else if (equip.price < 1000) {
            $scope.ErrorSystem('Giá thiết bị không được nhỏ hơn 1000VND.');
            console.log('price error');
        }
        else {
            console.log('no validation error');
            $scope.UpdateEquip(equip);
        }
    }
    $scope.show = false;
  
    $scope.UpdateEquip = function (equip) {
        var data = {
            equip: {
                system_equipment_code: url,
                standard_equipment_code: equip.standard_equipment_code,
                equipment_name: equip.equipment_name,
                unit: equip.unit,
                serial_no: equip.serial_no,
                type_equipment: equip.type_equipment,
                category_code: equip.category_code,
                price: equip.price,
                create_date: equip.create_date,
                note: equip.note,
                responsibler: equip.responsibler,
                usage_status: equip.usage_status,
                specification: equip.specification,
                is_integration: equip.is_integration
            },
            location_id: equip.location_id,
            ListComponentEquips: $scope.ListComponentEquips
        }
        var partialUrl = origin + '/api/v1/equipments/update/' + equip.equipment_id ;
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Cập nhật thiết bị thành công!');
                $scope.UpdateImageEquip(response.data)
              
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.UpdateImageEquip = function (equip) {
        var ListFileAttach = document.getElementById('formFile').files;
        let formData = new FormData();

        for (let i = 0; i < ListFileAttach.length; i++) {
            formData.append('images[]', ListFileAttach[i]);
        }

        formData.append('equipment_id', equip.equipment_id);

        var partialUrl = origin + '/api/v1/equipments/images/add';

        $http.post(partialUrl, formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }  // Let AngularJS set the content type
        })
            .then(function (response) {
                $scope.SuccessSystem('Cập nhật ảnh thiết bị thành công!');
                $scope.GetDetailEquip(url);
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }

});
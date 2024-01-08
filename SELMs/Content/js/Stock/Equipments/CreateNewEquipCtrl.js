var app = angular.module("myApp", []);

app.controller('CreateNewEquipCtrl', function ($scope, $http, $sce) {

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

    $scope.ListUnits = ['Cái', 'Chiếc', 'Bộ', 'Cặp', 'Hộp'];
    var url = document.location.href;
    if (url.includes('?')) {
        url = url.substring(url.indexOf('?') + 1);
    } else {
        url = '';
    }

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

    $scope.text = '';
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
            usage_status:''
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
    $scope.ResetNewEquip = function () {
        $scope.NewEquip = {
            standard_equipment_code: '',
            equipment_name: '',
            unit: 'Cái',
            serial_no: '',
            type_equipment: 'private',
            category_code: '',
            location_id: url != '' ? parseInt(url):1,
            price: 0,
            create_date: new Date(),
            note: '',
            responsibler: username,
            usage_status: 'Thiết bị mới',
            specification: '',
            is_integration: false,
            img: []

        }
        $scope.ListComponentEquips = [];
        console.log(url);
        if (url != '') {
            var partialUrl = origin + '/api/v1/locations/detail/' + url;
            $http.post(partialUrl)
                .then(function (response) {
                    console.log(response.data);
                    $scope.ListLocations = response.data.ListSubLocation;
                }, function (error) {
                    $scope.ErrorSystem(error.data.Message);
                });
        }
    }
    $scope.ResetNewEquip();
   
    $scope.CreateNewEquip = function (equip) {
        console.log(equip);
        var data = {
            equip: {
                standard_equipment_code: equip.standard_equipment_code,
                equipment_name: equip.equipment_name,
                unit: equip.unit,
                serial_no: equip.serial_no,
                type_equipment: equip.type_equipment,
                category_code: equip.category_code,               
                price: equip.price,
                create_date: new Date(),
                note: equip.note,
                responsibler: equip.responsibler,
                usage_status: equip.usage_status,
                specification: equip.specification,
                is_integration: equip.is_integration
            },     
            location_id: equip.location_id,
            ListComponentEquips: $scope.ListComponentEquips
        }
        var partialUrl = origin + '/api/v1/equipments/new-equipment';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Thêm mới thiết bị thành công!');
                $scope.UpdateImageEquip(response.data)
                $scope.ResetNewEquip();

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }

    $scope.LoadEquipDetailsBySystemCode = function (system_equipment_code) {
        window.location.href = "/Equipments/EquipmentDetails/" + system_equipment_code;
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
                $scope.LoadEquipDetailsBySystemCode(equip.system_equipment_code);
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    };
});
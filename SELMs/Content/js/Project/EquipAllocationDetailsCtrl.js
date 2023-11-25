var app = angular.module("myApp", []);

app.controller('EquipAllocationDetailsCtrl', function ($scope, $http, $sce) {

    var username = $('#username').val();
    var fullname = $('#fullname').val();

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


    //===============Danh sách khách hàng=====================
    $scope.project_name = '';
    $scope.location_name = '';
    $scope.creater_name = '';
    $scope.application_code = '';
    $scope.LoadAvailableEquipmentList = function () {
        var data = {
            username: username,
            isadmin: isadmin,
            text: $scope.project_name,
            text1: $scope.location_name,
            text2: $scope.creater_name,
            text3: $scope.application_code
        }
        $http.post(origin + '/api/v1/equipment-allocation-application/avaiable-equipments', data).then(function (response) {
            $scope.ListAvailableEquipment = response.data.Result
            console.log($scope.ListAvailableEquipment);
        });
    }
    $scope.LoadAvailableEquipmentList();
    $scope.ResetHandoverForm = function () {
        $scope.ListDetailHandover = [];
        $scope.NewHandover = {
            handover_date: null,
            handover_name: '',
            handover: '',
            location_name: '',
            location_id: 0,
            project_name: '',
            project_id: '',
            receipter: '',
            receipter_name:''
            }
    }
    $scope.ResetHandoverForm();
    $scope.NewHandover.handover_date = new Date();
    $scope.NewHandover.handover_name = fullname
    $scope.NewHandover.handover = username
    $scope.SelectEquip = function (item) {
        $scope.NewHandover.location_name = item.location_desciption;
        $scope.NewHandover.location_id = item.location_id;
        $scope.NewHandover.project_name = item.project_name;
        $scope.NewHandover.project_id = item.project_id;
        $scope.ListDetailHandover.push({
            system_equipment_code: item.system_equipment_code,
            equipment_name: item.equipment_name,
            unit: item.unit,
            equipment_specification: item.specification,
            usage_status: item.usage_status
            })
    }
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
    $scope.HandelMemberLocation = function (mb, NewHandover) {
        NewHandover.receipter = mb.user_code;
        NewHandover.receipter_name = mb.fullname;

    }
    $scope.SaveNewHandover = function (NewHandover) {
        var data = {
            location_code: NewHandover.location_code,
            location_desciption: NewHandover.location_desciption,
            parent_location_id: NewHandover.parent_location_id,
            location_level: NewHandover.location_level,
            is_active: true
        }
        var partialUrl = origin + '/api/v1/locations/update/' + location.location_id;
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Cập nhật thông tin vị trí thành công!');

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });

    }
    //=========================Danh sách thiết bị cần nhập mới =================
    $scope.LoadNeedImportEquipmentList = function () {
        var data = {
            username: username,
            isadmin: isadmin,
            text: $scope.project_name,
            text1: $scope.location_name,
            text2: $scope.creater_name,
            text3: $scope.application_code
        }
        $http.post(origin + '/api/v1/equipment-allocation-application/need-import-equipments', data).then(function (response) {
            $scope.ListNeedImportEquipment = response.data.Result
            console.log($scope.ListNeedImportEquipment);
        });
    }
    $scope.LoadNeedImportEquipmentList();

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
        item.system_equipment_code_corresponding = eq.system_equipment_code;

    }
});
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
            receipter_name: ''
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
            standard_equipment_code: item.standard_equipment_code,
            equipment_name: item.equipment_name,
            unit: item.unit,
            equipment_specification: item.specification,
            usage_status: item.usage_status,
            application_detail_id: item.application_detail_id,
            status: 'Đã bàn giao'
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
            application_details: $scope.ListDetailHandover,
            application: {
                creater: username,
                handover_date: NewHandover.handover_date,
                handover_name: NewHandover.handover_name,
                handover: NewHandover.handover,
                description: NewHandover.description,
                location_name: NewHandover.location_name,
                location_id: NewHandover.location_id,
                project_name: NewHandover.project_name,
                project_id: NewHandover.project_id,
                receipter: NewHandover.receipter,
                receipter_name: NewHandover.receipter_name
            }
        }
        var partialUrl = origin + '/api/v1/equipment-handover-form/new-form';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Tạo đơn bàn giao thiết bị thành công!');
                $scope.LoadAvailableEquipmentList();
                $scope.ResetHandoverForm();
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


    $scope.checkAdd = false;
    $scope.AddNewEquip = function (ListDetailHandover) {
        $scope.checkAdd = true;
        ListDetailHandover.push({
            system_equipment_code: '',
            standard_equipment_code: '',
            equipment_name: '',
            equipment_specification: '',
            usage_status: '',
            note: '',
            status:'Đã bàn giao'
            })
    }
    $scope.HandelEquip = function (eq, item, ListDetailHandover) {
        // Check if there is an item with the same system_equipment_code in ListDetailHandover
        var existingItem = null;

        for (var i = 0; i < ListDetailHandover.length; i++) {
            if (ListDetailHandover[i].system_equipment_code === eq.system_equipment_code) {
                existingItem = ListDetailHandover[i];
                break;
            }
        }

        if (!existingItem) {
            item.system_equipment_code = eq.system_equipment_code;
            item.standard_equipment_code = eq.standard_equipment_code;
            item.equipment_name = eq.equipment_name;
            item.equipment_specification = eq.specification;
            item.usage_status = eq.usage_status;
            $scope.checkAdd = false;
        }
    }
    $scope.RemoveEquip = function (ListDetailHandover, index) {
        ListDetailHandover.splice(index, 1);
    }
});
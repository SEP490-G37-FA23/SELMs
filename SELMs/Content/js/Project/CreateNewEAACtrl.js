var app = angular.module("myApp", []);

app.controller('CreateNewEAACtrl', function ($scope, $http, $sce) {

    var username = $('#username').val();
    var isadmin = $('#isadmin').val();
    var role = $('#role').val();
    var fullname = $('#fullname').val();


    $scope.username = $('#username').val();

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
   
   
    $scope.Reset = function () {
        $scope.location_name = '';
        $scope.project_name = '';
        $scope.NewEAA = {
            creater: username,
            name_creater: fullname,
            application_date: $scope.NgayHomNay,
            location_id: 0,
            location_name:'',
            project_id: 0,
            desciption: '',
            notes: '',
            status:'Chờ xử lý'

        }
        $scope.ListEquipRequest = [];
        $scope.select_equip = false;

    }
    $scope.Reset();

    $scope.LoadProjectsList = function () {
        var data = {
            username: username,
            project_name: $scope.project_name
        }
        $http.post(origin + '/api/v1/projects/doing', data).then(function (response) {
            $scope.ListProjects = response.data;
        });
    }

    $scope.LoadProjectsList();
    $scope.HandelProject = function (pj, NewEAA) {
        NewEAA.location_id = pj.location_id;
        NewEAA.project_id = pj.project_id;
        NewEAA.location_desciption = pj.location_desciption;
        $scope.project_name = pj.project_name;
    }
    $scope.HandelNewEquipRequest = function (ListEquipRequest) {
        $scope.select_equip = false;
        ListEquipRequest.push({
            system_equipment_code: '',
            standard_equipment_code: '',
            equipment_name: '',
            specification:'',
            usage_status: '',
            unit:'',
            status: '',
            note:''
        });
    }
    $scope.HandelEquip = function (eq, item) {
        item.system_equipment_code = eq.system_equipment_code;
        item.standard_equipment_code = eq.standard_equipment_code;
        item.equipment_name = eq.equipment_name;
        item.equipment_specification = eq.specification;
        item.usage_status = eq.usage_status;
        item.unit = eq.unit;
        item.status = 'Có sẵn trong kho';
        $scope.select_equip = true;

    }
    $scope.DeleteEquipRequest = function (ListEquipRequest, index) {
        ListEquipRequest.splice(index, 1);
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

    $scope.ValidateDataInput = function (NewEAA) {
        console.log(NewEAA);
        if (NewEAA.project_id == '0') {
            console.log('pjid not valid');
            $scope.ErrorSystem('Vui lòng chọn dự án cần cấp phát thiết bị.');
        }
        else {
            console.log('no invalid data');
            $scope.SaveNewEAA(NewEAA);
        }
    }

    $scope.SaveNewEAA = function (NewEAA) {
        console.log(NewEAA);
        var data = {
            application: {
                total_equipments: $scope.ListEquipRequest.length,
                notes: NewEAA.notes,
                desciption: NewEAA.desciption,
                project_id: NewEAA.project_id,
                location_id: NewEAA.location_id,
                creater: username
            },           
            application_details: $scope.ListEquipRequest
        }
        var partialUrl = origin + '/api/v1/equipment-allocation-application/new-application';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Thêm mới yêu cầu cấp thiết bị thành công!');
                $scope.Reset();

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
});
var app = angular.module("myApp", []);

app.controller('CreateNewInventoryRequestCtrl', function ($scope, $http, $sce) {

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

    $scope.LoadLocationsList = function () {
        var data = {
            username: username,
            text: '',
            level: 1,
            id: 0
        }
        $http.post(origin + '/api/v1/locations', data).then(function (response) {
            $scope.ListLocations = response.data;
        });
    }
    $scope.LoadLocationsList();
    $scope.ResetNewInventory = function () {
        $scope.performerSearch = '';
        $scope.NewInventory = {
            requester: username,
            location_id: 0,
            performer: '',
            requesterSearch: fullname

        }
        $scope.ListNewEquipmentInInventory = [];
    }

    $scope.ResetNewInventory();

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
    $scope.HandelMemberPerformer = function (mb, NewInventory) {
        NewInventory.performer = mb.user_code;
        $scope.performerSearch = mb.fullname;
    }
    $scope.GetDetailLocation = function (location_id) {
        console.log(location_id)
        var partialUrl = origin + '/api/v1/locations/detail/' + parseInt(location_id);
        $http.post(partialUrl)
            .then(function (response) {
                console.log(response.data);
                $scope.DetailLocation = response.data.location_info;
                $scope.ListSubLocation = response.data.ListSubLocation;
                $scope.ListProjectInLocation = response.data.ListProjectInLocation;
                $scope.ListEquipmentInLocation = response.data.ListEquipmentInLocation;
                console.log($scope.ListEquipmentInLocation);
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
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
    $scope.HandelEquip = function (eq, item) {
        item.system_equipment_code = eq.system_equipment_code;
        item.standard_equipment_code = eq.standard_equipment_code;
        item.equipment_name = eq.equipment_name;

    }

    $scope.HandleNewEquipInInventory = function (ListNewEquipmentInInventory) {
        ListNewEquipmentInInventory.push({
            system_equipment_code: '',
            standard_equipment_code: '',
            equipment_name: ''

        })
    }
    $scope.DeleteNewEquipInInventory = function (ListNewEquipmentInInventory, index) {
        ListNewEquipmentInInventory.splice(index, 1);
    }

    $scope.AddAllEquipmentLocation = function () {
        $scope.ListNewEquipmentInInventory = [];
        for (var i = 0; i < $scope.ListEquipmentInLocation.length; i++) {
            var equipment = $scope.ListEquipmentInLocation[i];

            // Extract properties from the current item in ListEquipmentInLocation
            var newEquipment = {
                system_equipment_code: equipment.system_equipment_code,
                standard_equipment_code: equipment.standard_equipment_code,
                equipment_name: equipment.equipment_name
            };
            $scope.ListNewEquipmentInInventory.push(newEquipment);
        }
        console.log($scope.ListNewEquipmentInInventory);
    }

    $scope.SaveNewInventoryRequest = function (NewInventory) {
        var data = {
            application: {
                requester: NewInventory.requester,
                performer: NewInventory.performer,
                total_equipment: $scope.ListNewEquipmentInInventory.length,
                status: false,
                location_id: parseInt(NewInventory.location_id)
            },
            application_details:  $scope.ListNewEquipmentInInventory.map(function (item) {
                    return item.system_equipment_code;
                })
        }
        var partialUrl = origin + '/api/v1/inventory-request/new-application';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Thêm mới đơn kiểm kê thành công!');
                $scope.ResetNewInventory();

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
});
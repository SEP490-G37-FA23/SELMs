var app = angular.module("myApp", []);

app.controller('LocationDetailCtrl', function ($scope, $http, $sce) {

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


    $scope.GetDetailLocation = function (location_id) {
        new QRCode(document.getElementById('id_qrcode'), origin + '/Locations/LocationDetails/' + location_id);
        var partialUrl = origin + '/api/v1/locations/detail/' + location_id;
        $http.post(partialUrl)
            .then(function (response) {
                console.log(response.data);
                $scope.DetailLocation = response.data.location_info;
                $scope.ListSubLocation = response.data.ListSubLocation;
                $scope.ListProjectInLocation = response.data.ListProjectInLocation;
                $scope.ListEquipmentInLocation = response.data.ListEquipmentInLocation;
                $scope.ListMembersJoinLocation = response.data.ListMemberInLocation;
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.GetDetailLocation(url);
    $scope.showProject = false;
    $scope.showEquip = false;
    $scope.showOverview = true;
    $scope.showSubLocation = false;
    $scope.showMembers = false;


    $scope.GetOverview = function () {
        $scope.showProject = false;
        $scope.showEquip = false;
        $scope.showOverview = true;
        $scope.showSubLocation = false;
        $scope.showMembers = false;
    }
    $scope.GetEquipment = function () {
        $scope.showProject = false;
        $scope.showEquip = true;
        $scope.showOverview = false;
        $scope.showSubLocation = false;
        $scope.showMembers = false;
    }
    $scope.GetSubLocation = function (item) {
        $scope.showProject = false;
        $scope.showEquip = false;
        $scope.showOverview = false;
        $scope.showSubLocation = true;
        $scope.showMembers = false;
        document.getElementById('id_qrcode_sub').innerHTML = '';
        new QRCode(document.getElementById('id_qrcode_sub'), origin + '/Locations/LocationDetails/' + item.location_id);
        var partialUrl = origin + '/api/v1/locations/detail/' + item.location_id;
        $http.post(partialUrl)
            .then(function (response) {
                console.log(response.data);
                $scope.DetailSubLocation = response.data.location_info;
                $scope.ListSubLocation_L3 = response.data.ListSubLocation;

                $scope.ListEquipmentInSubLocation = response.data.ListEquipmentInLocation;
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.GetProject = function () {
        $scope.showProject = true;
        $scope.showEquip = false;
        $scope.showOverview = false;
        $scope.showSubLocation = false;
        $scope.showMembers = false;
    }
    $scope.GetMember = function () {
        $scope.showProject = false;
        $scope.showEquip = false;
        $scope.showOverview = false;
        $scope.showSubLocation = false;
        $scope.showMembers = true;
    }


    $scope.CreateNewProject = function () {
        window.location.href = origin + "/ProjectPortfolio/CreateNewProject/" + url;
    }
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
    $scope.ListMembersJoinLocation = [];
    $scope.HandleNewMember = function () {
        $scope.ListMembersJoinLocation.push({
            user_code: '',
            fullname: '',
            status: 'Đang tham gia',
            note: ''
        })
    }
    $scope.HandelMemberLocation = function (mb, item) {
        item.text = mb.user_code;
        item.user_code = mb.user_code;
        item.fullname = mb.fullname;

    }
    $scope.DeleteMemberLocation = function (list, index) {
        list.splice(index, 1);
    }


    $scope.CreateNewSubLocation_L2 = function () {
        $('#NewSubLocation_L2').show();
    }

    $scope.HandleNewSubLocation_L3 = function (ListSubLocation_L3,Location_L2) {
        ListSubLocation_L3.push({
            location_code: '',
            location_desciption: '',
            parent_location_id: Location_L2.location_id,
            location_level: 3,
            is_active: true
        })
    }
    $scope.DeleteMemberProject = function (ListSubLocation_L3, index) {
        ListSubLocation_L3.splice(index, 1);
    }

    $scope.UpdateLocation = function (location) {
        var data = {
            location_code: location.location_code,
            location_desciption: location.location_desciption,
            parent_location_id: location.parent_location_id,
            location_level: location.location_level,
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
    var today = new Date();
    $scope.ListNewEquipmentInLocation = [];

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

    $scope.LoadAllSubLocationList = function (text) {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            text: text,
            id: $scope.DetailLocation.location_id

        }
        $http.post(origin + '/api/v1/locations/all-sub-location', data).then(function (response) {
            $scope.ListAllSubLocation = response.data.Result;
        });
    }

    $scope.HandelSubLocation = function (lc, item) {
        item.location_id = lc.location_id;
        item.location_desciption = lc.location_desciption;
        item.text = lc.location_code;

    }

    $scope.HandleNewEquipInLocation = function (ListNewEquipmentInLocation) {
        ListNewEquipmentInLocation.push({
            system_equipment_code: '',
            standard_equipment_code: '',
            equipment_name: '',
            location_id: $scope.DetailLocation.location_id,
            location_desciption: $scope.DetailLocation.location_desciption,
            responsibler: username,
            from_date: today,
            note: ''
        })
    }
    $scope.DeleteNewEquipInLocation = function (ListNewEquipmentInLocation, index) {
        ListNewEquipmentInLocation.splice(index, 1);
    }

    $scope.SaveEquiInLocation = function () {
        var ListSave = [];
        $scope.ListNewEquipmentInLocation.forEach(item => {
            ListSave.push({
                system_equipment_code: item.system_equipment_code,
                location_id: item.location_id,
                note: item.note
                })
        })
        var data = {
            ListEquipLocationHistory: ListSave
        }
        var partialUrl = origin + '/api/v1/locations/equip-location-history';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Cập nhật thiết bị vào vị trí thành công!');

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.SaveMemberJoinLocation = function () {
        var ListSave = [];
        $scope.ListMembersJoinLocation.forEach(item => {
            ListSave.push({
                user_code: item.user_code,
                location_id: $scope.DetailLocation.location_id,
                note: item.note
            })
        })
        var data = {
            ListMembersJoinLocation: ListSave
        }
        console.log(data);
        var partialUrl = origin + '/api/v1/member-location-history/create';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Cập nhật thành viên sử dụng vị trí thành công!');

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.NewLocation = {
        location_code: '',
        location_desciption: '',
        parent_location_id: parseInt(url),
        location_level: 2,
        is_active: true
    }
    $scope.SaveNewLocation = function (newLocation) {
        var data = {
            Location: {
                location_code: newLocation.location_code,
                location_desciption: newLocation.location_desciption,
                parent_location_id: newLocation.parent_location_id,
                location_level: newLocation.location_level,
                is_active: true
            },
            SubLocations: []

        }
        var partialUrl = origin + '/api/v1/locations/new-location';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Thêm mới vị trí thành công!');

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
});